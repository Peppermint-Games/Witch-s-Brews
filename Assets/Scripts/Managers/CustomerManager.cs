using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager I;
    [Range(0, 1)]
    public float specificRecipeChance = .25f, regularChance = .2f;
    private void Awake()
    {
        I = this;
    }
    public List<string> customerNames = new List<string>();
    public List<OrderKeyword> keywords = new List<OrderKeyword>();
    public Customer currentCustomer;
    public Customer GenerateCustomer()
    {
        List<RegularData> regulars = GameManager.I.save.data.tea.regulars;
        if (regulars.Count > 0 && Random.value < regularChance)
            return GenerateRegular();
        return GenerateNewCustomer();
    }
    public Customer GenerateNewCustomer()
    {
        Customer customer = new Customer();
        customer.customerName = customerNames[Random.Range(0, customerNames.Count)];
        customer.order = GenerateOrder();
        currentCustomer = customer;
        return customer;
    }
    Customer GenerateRegular()
    {
        List<RegularData> regulars = GameManager.I.save.data.tea.regulars;
        RegularData data = regulars[Random.Range(0, regulars.Count)];
        Customer customer = new Customer();
        customer.customerName = data.customerName;
        customer.isRegular = true;
        customer.regularID = data.id;
        customer.order = GenerateRegularOrder(data);
        data.visits++;
        return customer;
    }
    CustomerOrder GenerateOrder()
    {
        CustomerOrder order = new CustomerOrder();
        bool canRequestRecipe = GameManager.I.save.data.tea.RBook.myRecipes.Count > 0;
        if (canRequestRecipe && Random.value < specificRecipeChance)
            GenerateSpecificTeaOrder(order);
        else
            GenerateVibeOrder(order);
        return order;
    }
    CustomerOrder GenerateRegularOrder(RegularData regular)
    {
        CustomerOrder order = new CustomerOrder();
        if(regular.preferredTeaIDs.Count > 0 && Random.value < .6f)
        {
            int teaID = regular.preferredTeaIDs[Random.Range(0, regular.preferredTeaIDs.Count)];
            order.wantsSpecificTea = true;
            order.requestedTeaID = teaID;
            order.requestText = "The usual, please";
            order.basePay = 5;
            return order;
        }
        return GenerateVibeOrder();
    }
    int CalculateTip(Customer customer, float score)
    {
        if (!customer.isRegular)
            return 0;
        RegularData regular = GetRegular(customer.regularID);
        if (regular == null)
            return 0;
        float performance = Mathf.Clamp01(score / 100f);
        float tip = customer.order.basePay * regular.tipBonus * performance;
        return Mathf.RoundToInt(tip);
    }
    void GenerateSpecificTeaOrder(CustomerOrder order)
    {
        List<SavedTeaRecipe> recipes = GameManager.I.save.data.tea.RBook.myRecipes;
        SavedTeaRecipe recipe = recipes[Random.Range(0, recipes.Count)];
        order.wantsSpecificTea = true;
        order.requestedTeaID = recipe.id;
        order.requestText = "Could I get a " + recipe.customName + "?";
        order.basePay = 5;
    }
    public void TryCreateRegular(Customer customer, Teabag servedTea, float score)
    {
        if (customer.isRegular)
            return;
        if (score < 75f)
            return;
        float chance = Mathf.Lerp(0.05f, .20f, score / 100f);
        if (Random.value > chance)
            return;
        teaData data = GameManager.I.save.data.tea;
        RegularData regular = new RegularData();
        regular.id = data.nextRegularID++;
        regular.customerName = customer.customerName;
        regular.visits = 1;
        regular.successfulVisits = 1;
        regular.tipBonus = .25f;
        regular.preferredTeaIDs.Add(servedTea.id);
        regular.preferredVibes.Add(servedTea.teaVibe);
        data.regulars.Add(regular);
        customer.isRegular = true;
        customer.regularID = regular.id;
        GameManager.I.Save();
    }
}