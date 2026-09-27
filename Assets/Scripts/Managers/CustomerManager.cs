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
        keywords = KeywordDatabase.BuildDatabase();
        customerNames = CustomerNameDatabase.BuildDatabase();
    }
    private void OnDestroy() => I = null;
    List<string> customerNames = new List<string>();
    List<OrderKeyword> keywords = new List<OrderKeyword>();
    public Customer GenerateCustomer()
    {
        List<RegularData> regulars = GameManager.I.save.data.tea.regulars;
        Customer customer;
        if (regulars.Count > 0 && Random.value < regularChance)
            customer = GenerateRegular();
        else
            customer = GenerateNewCustomer();
        customer.state = CustomerState.Ordering;
        return customer;
    }
    public Customer GenerateNewCustomer()
    {
        Customer customer = new Customer();
        customer.customerName = customerNames[Random.Range(0, customerNames.Count)];
        customer.order = GenerateOrder();
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
            order = GenerateVibeOrder();
        return order;
    }
    CustomerOrder GenerateRegularOrder(RegularData regular)
    {
        CustomerOrder order = new CustomerOrder();
        if (regular.preferredTeaIDs.Count > 0 && Random.value < .6f)
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
    CustomerOrder GenerateVibeOrder()
    {
        CustomerOrder order =
            new CustomerOrder();

        order.wantsSpecificTea = false;
        order.requestedTeaID = -1;
        order.basePay = 5;

        if (keywords == null ||
            keywords.Count == 0)
        {
            order.requestText =
                "Surprise me.";

            return order;
        }

        List<string> requestParts =
            new List<string>();

        // Pick our primary request.
        OrderKeyword primary =
            keywords[
                Random.Range(
                    0,
                    keywords.Count
                )
            ];

        AddRequirement(
            order,
            primary
        );

        requestParts.Add(
            primary.text
        );

        // 50% chance of having a second requirement.
        if (Random.value < 0.5f)
        {
            List<OrderKeyword> secondaryOptions =
                keywords.FindAll(
                    x =>
                        x != null &&
                        x != primary
                );

            if (secondaryOptions.Count > 0)
            {
                OrderKeyword secondary =
                    secondaryOptions[
                        Random.Range(
                            0,
                            secondaryOptions.Count
                        )
                    ];

                AddRequirement(
                    order,
                    secondary
                );

                requestParts.Add(
                    secondary.text
                );
            }
        }

        order.requestText =
            string.Join(
                " ",
                requestParts
            );

        return order;
    }

    void AddRequirement(
        CustomerOrder order,
        OrderKeyword keyword)
    {
        if (order == null ||
            keyword == null)
            return;

        VibeRequirement existing =
            order.requirements.Find(
                x =>
                    x.targetvibe ==
                    keyword.targetVibe
            );

        if (existing != null)
        {
            existing.weight +=
                keyword.weight;
        }
        else
        {
            order.requirements.Add(
                new VibeRequirement
                {
                    targetvibe =
                        keyword.targetVibe,

                    weight =
                        keyword.weight
                }
            );
        }
    }
    RegularData GetRegular(int id)
    {
        if (GameManager.I == null || GameManager.I.save == null || GameManager.I.save.data == null || GameManager.I.save.data.tea == null || GameManager.I.save.data.tea.regulars == null)
            return null;
        return GameManager.I.save.data.tea.regulars.Find(x => x.id == id);
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
    public int ProcessSuccessfulVisit(
   Customer customer,
   Teabag servedTea,
   float score)
    {
        if (customer == null ||
            servedTea == null)
            return 0;

        // Existing regular.
        if (customer.isRegular)
        {
            RegularData regular =
                GetRegular(
                    customer.regularID
                );

            if (regular == null)
                return 0;

            // Treat 75+ as a successful visit.
            if (score >= 75f)
            {
                regular.successfulVisits++;

                if (!regular.preferredTeaIDs
                    .Contains(servedTea.id))
                {
                    regular.preferredTeaIDs
                        .Add(servedTea.id);
                }

                if (!regular.preferredVibes
                    .Contains(servedTea.teaVibe))
                {
                    regular.preferredVibes
                        .Add(servedTea.teaVibe);
                }
            }

            return CalculateTip(
                customer,
                score
            );
        }

        // New customers have a chance to become regulars.
        TryCreateRegular(
            customer,
            servedTea,
            score
        );

        return 0;
    }
}