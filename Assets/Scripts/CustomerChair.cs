using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerChair : MonoBehaviour
{
    public ChairData data;
    public int chairID;
    public Transform customerPosition;
    [System.NonSerialized]
    public Customer heldCustomer;
    public bool isUnlocked { get { return data != null && data.isUnlocked; } }
    public bool isOccupied { get { return heldCustomer != null; } }
    private void Awake()
    {
        customerPosition = this.transform;
        heldCustomer = null;
    }
    public bool SeatCustomer(Customer customer)
    {
        if (customer == null || data == null || isOccupied || !isUnlocked)
            return false;
        heldCustomer = customer;
        customer.chairID = chairID;
        customer.state = CustomerState.Waiting;
        UpdateVisuals();
        return true;
    }
    public Customer RemoveCustomer()
    {
        if (!isOccupied)
            return null;
        Customer customer = heldCustomer;
        heldCustomer = null;
        customer.chairID = -1;
        UpdateVisuals();
        return customer;
    }
    public void CallCustomerUp()
    {
        if (!isOccupied)
            return;
        SeatingManager.I.CallCustomerFromSeat(chairID);
    }
    public void DismissCustomer()
    {
        if (!isOccupied)
            return;
        SeatingManager.I.DismissCustomer(chairID);
    }
    public void UpdateVisuals()
    {

    }
    public void UnlockChair()
    {
        if (data == null || data.isUnlocked)
            return;
        data.isUnlocked = true;
        UpdateVisuals();
        GameManager.I.Save();
    }
}