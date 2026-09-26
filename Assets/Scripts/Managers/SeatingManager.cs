using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeatingManager : MonoBehaviour
{
    public static SeatingManager I;
    private void Awake()
    {
        I = this;
    }
    public List<CustomerChair> chairs = new List<CustomerChair>();
    private void Start()
    {
        BuildChairs();
        LoadSeatedCustomers();
    }
    private void OnDestroy() => I = null;
    void LoadSeatedCustomers()
    {
        List<SeatedCustomerData> saved = GameManager.I.save.data.tea.seatedCustomers;
        foreach (var item in saved)
        {
            CustomerChair chair = chairs.FirstOrDefault(x => x.chairID == item.chairID);
            if (chair == null)
                continue;
            Customer customer = new Customer
            {
                customerName = item.customerName,
                isRegular = item.isRegular,
                regularID = item.regularID,
                chairID = item.chairID,
                state = CustomerState.Waiting,
                order = new CustomerOrder
                {
                    wantsSpecificTea = item.wantsSpecificTea,
                    requestedTeaID = item.requestedTeaID,
                    requestText = item.requestText,
                    basePay = item.basePay,
                    requirements = new List<VibeRequirement>(item.requirements)
                }
            };
            chair.heldCustomer = customer;
            chair.UpdateVisuals();
        }
    }
    void BuildChairs()
    {
        teaData tea = GameManager.I.save.data.tea;
        foreach (var item in chairs)
        {
            if (item == null)
                continue;
            ChairData saved = tea.chairs.FirstOrDefault(x => x.chairID == item.chairID);
            if(saved == null)
            {
                saved = new ChairData { chairID = item.chairID, isUnlocked = false };
                tea.chairs.Add(saved);
            }
            item.data = saved;
            item.UpdateVisuals();
        }
    }
    public bool HasFreeSeat()
    {
        return chairs.Any(x => !x.isOccupied && x.isUnlocked);
    }
    public CustomerChair GetFreeSeat()
    {
        return chairs.FirstOrDefault(x => !x.isOccupied && x.isUnlocked);
    }
    public bool SeatCustomer(Customer customer)
    {
        CustomerChair chair = GetFreeSeat();
        if (chair == null)
            return false;
        bool success = chair.SeatCustomer(customer);
        if (!success)
            return false;
        SaveSeatedCustomer(customer, chair.chairID);
        return true;
    }
    public Customer PullCustomer(int chairID)
    {
        CustomerChair chair = chairs.FirstOrDefault(x => x.chairID == chairID);
        if (chair == null)
            return null;
        Customer customer = chair.RemoveCustomer();
        if (customer == null)
            return null;
        RemoveSavedCustomer(chairID);
        customer.state = CustomerState.ReadyToServe;
        GameManager.I.Save();
        return customer;
    }
    public void DismissCustomer(int chairID)
    {
        Customer customer = PullCustomer(chairID);
        if (customer == null)
            return;
        customer.state = CustomerState.Leaving;
    }
    void SaveSeatedCustomer(Customer customer, int chairID)
    {
        SeatedCustomerData data = new SeatedCustomerData
        {
            chairID = chairID,
            customerName = customer.customerName,
            wantsSpecificTea = customer.order.wantsSpecificTea,
            requestedTeaID = customer.order.requestedTeaID,
            requirements = new List<VibeRequirement>(customer.order.requirements),
            requestText = customer.order.requestText,
            basePay = customer.order.basePay,
            isRegular = customer.isRegular,
            regularID = customer.regularID
        };
        GameManager.I.save.data.tea.seatedCustomers.Add(data);
        GameManager.I.Save();
    }
    void RemoveSavedCustomer(int chairID)
    {
        GameManager.I.save.data.tea.seatedCustomers.RemoveAll(x => x.chairID == chairID);
    }
    public void MakeCustomerWait()
    {
        Customer customer = CustomerManager.I.currentCustomer;
        if (customer == null)
            return;
        bool seated = SeatingManager.I.SeatCustomer(customer);
        if (!seated)
            return;
        CustomerManager.I.currentCustomer = null;
        CustomerManager.I.GenerateCustomer();
    }
    public void DismissCurrentCustomer()
    {
        Customer customer = CustomerManager.I.currentCustomer;
        if (customer == null)
            return;
        customer.state = CustomerState.Leaving;
        CustomerManager.I.currentCustomer = null;
        CustomerManager.I.GenerateCustomer();
    }
    public void CallCustomerFromSeat(int chairID)
    {
        if (CustomerManager.I.currentCustomer != null)
            return;
        Customer customer = SeatingManager.I.PullCustomer(chairID);
        if (customer == null)
            return;
        CustomerManager.I.currentCustomer = customer;
        customer.state = CustomerState.ReadyToServe;
        CustomerManager.I.SetupCustomerUI();
    }
}