using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager I;
    private void Awake()
    {
        I = this;
    }
    public List<string> customerNames = new List<string>();
    public List<OrderKeyword> keywords = new List<OrderKeyword>();
    public Customer currentCustomer;
    public Customer GenerateCustomer()
    {
        Customer customer = new Customer();
        customer.customerName = customerNames[Random.Range(0, customerNames.Count)];
        customer.order = GenerateOrder();
        currentCustomer = customer;
        return customer;
    }
    CustomerOrder GenerateOrder()
    {
        CustomerOrder order = new CustomerOrder();
        int requirementCount = Random.Range(1, 3);
        List<OrderKeyword> available = new List<OrderKeyword>(keywords);
        List<string> sentences = new List<string>();
        for(int i = 0; i < requirementCount; i++)
        {
            if (available.Count == 0)
                break;
            int index = Random.Range(0, available.Count);
            OrderKeyword keyword = available[index];
            available.RemoveAt(index);
            sentences.Add(keyword.text);
            order.requirements.Add(new VibeRequirement { targetvibe = keyword.targetVibe, weight = keyword.weight });
        }
        order.requestText = string.Join(" ", sentences);
        order.basePay = 5;
        return order;
    }
}