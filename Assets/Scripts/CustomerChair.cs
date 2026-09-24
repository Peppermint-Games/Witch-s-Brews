using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerChair : MonoBehaviour
{
    public int chairID;
    public Transform customerPosition;
    public Customer heldCustomer;
    public bool isOccupied => heldCustomer != null;
    public bool SeatCustomer(Customer customer)
    {
        if (customer == null)
            return false;
        if (isOccupied)
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
        customer.chairID = 01;
        UpdateVisuals();
        return customer;
    }
    public void UpdateVisuals()
    {

    }
}