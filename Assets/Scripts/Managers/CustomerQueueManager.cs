using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueManager : MonoBehaviour
{
    public static CustomerQueueManager I;

    [Header("Settings")]
    public int automaticQueueSize = 3;
    public int maximumQueueSize = 5;

    [Header("Scene")]
    public Transform spawnPoint;
    public Transform customerVisualLayer;

    [Header("Queue Slots")]
    public Transform[] queueAPositions =
        new Transform[5];

    public Transform[] queueBPositions =
        new Transform[5];

    [Header("Prefab")]
    public GameObject customerPrefab;

    public List<Customer> queueA =
        new List<Customer>();

    public List<Customer> queueB =
        new List<Customer>();

    private Dictionary<Customer, CustomerUI>
        customerVisuals =
            new Dictionary<Customer, CustomerUI>();

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        FillAutomaticQueues();
    }

    private void OnDestroy()
    {
        if (I == this)
            I = null;
    }

    // =========================================================
    // GENERATION
    // =========================================================

    public void FillAutomaticQueues()
    {
        FillAutomaticQueue(CustomerQueue.A);
        FillAutomaticQueue(CustomerQueue.B);

        RepositionQueues();
    }

    void FillAutomaticQueue(CustomerQueue queue)
    {
        List<Customer> customers =
            GetQueue(queue);

        while (customers.Count <
               automaticQueueSize)
        {
            Customer customer =
                CustomerManager.I
                    .GenerateCustomer();

            if (customer == null)
                break;

            customers.Add(customer);

            CreateCustomerVisual(
                customer,
                queue
            );
        }
    }

    void CreateCustomerVisual(
        Customer customer,
        CustomerQueue queue)
    {
        if (customerPrefab == null ||
            spawnPoint == null)
            return;

        GameObject obj =
            Instantiate(
                customerPrefab,
                customerVisualLayer
            );

        obj.transform.position =
            spawnPoint.position;

        CustomerUI ui =
            obj.GetComponent<CustomerUI>();

        if (ui == null)
        {
            Destroy(obj);
            return;
        }

        ui.Setup(customer, queue);

        customerVisuals.Add(
            customer,
            ui
        );
    }

    // =========================================================
    // POSITIONING
    // =========================================================

    public void RepositionQueues()
    {
        RepositionQueue(
            queueA,
            CustomerQueue.A
        );

        RepositionQueue(
            queueB,
            CustomerQueue.B
        );
    }

    void RepositionQueue(
        List<Customer> customers,
        CustomerQueue queue)
    {
        for (int i = 0;
             i < customers.Count;
             i++)
        {
            CustomerUI ui =
                GetCustomerUI(
                    customers[i]
                );

            if (ui == null)
                continue;

            ui.MoveToQueue(
                queue,
                i
            );
        }
    }
    public void RegisterSeatedCustomer(
   Customer customer,
   CustomerChair chair)
    {
        if (customer == null ||
            chair == null ||
            customerPrefab == null ||
            customerVisualLayer == null)
            return;

        // Don't create it twice.
        if (customerVisuals.ContainsKey(customer))
            return;

        GameObject obj =
            Instantiate(
                customerPrefab,
                customerVisualLayer
            );

        CustomerUI ui =
            obj.GetComponent<CustomerUI>();

        if (ui == null)
        {
            Destroy(obj);
            return;
        }

        // Setup requires a queue, but the customer is immediately
        // switched to their seated/waiting state below.
        ui.Setup(
            customer,
            CustomerQueue.A
        );

        customerVisuals.Add(
            customer,
            ui
        );

        // Put them straight at their chair.
        if (chair.customerPosition != null)
        {
            obj.transform.position =
                chair.customerPosition.position;
        }

        ui.SetWaiting();
    }
    public Transform GetQueuePosition(
        CustomerQueue queue,
        int index)
    {
        Transform[] positions =
            queue == CustomerQueue.A
            ? queueAPositions
            : queueBPositions;

        if (index < 0 ||
            index >= positions.Length)
            return null;

        return positions[index];
    }

    // =========================================================
    // CHAIRS
    // =========================================================

    public bool SendToChair(Customer customer)
    {
        if (customer == null)
            return false;

        CustomerChair chair =
            SeatingManager.I.GetFreeSeat();

        if (chair == null)
            return false;

        if (!SeatingManager.I
            .SeatCustomer(
                customer,
                chair
            ))
            return false;

        queueA.Remove(customer);
        queueB.Remove(customer);

        CustomerUI ui =
            GetCustomerUI(customer);

        if (ui != null)
            ui.MoveToChair(chair);

        FillAutomaticQueues();
        RepositionQueues();

        return true;
    }

    public bool ReturnFromChair(
        Customer customer)
    {
        if (customer == null)
            return false;

        CustomerQueue target =
            GetShortestQueueWithSpace();

        List<Customer> queue =
            GetQueue(target);

        if (queue.Count >= maximumQueueSize)
            return false;

        queue.Add(customer);

        CustomerUI ui =
            GetCustomerUI(customer);

        if (ui != null)
        {
            ui.MoveToQueue(
                target,
                queue.Count - 1
            );
        }

        RepositionQueues();

        return true;
    }

    public bool HasSpaceForChairCustomer()
    {
        return
            queueA.Count < maximumQueueSize ||
            queueB.Count < maximumQueueSize;
    }

    // =========================================================
    // DISMISS
    // =========================================================

    public void DismissCustomer(
        Customer customer)
    {
        if (customer == null)
            return;

        // If they're in a queue.
        queueA.Remove(customer);
        queueB.Remove(customer);

        // If they're sitting.
        if (customer.chairID >= 0)
        {
            SeatingManager.I
                .RemoveCustomerForExit(
                    customer.chairID
                );
        }

        customer.state =
            CustomerState.Leaving;

        CustomerUI ui =
            GetCustomerUI(customer);

        if (ui != null)
        {
            customerVisuals.Remove(
                customer
            );

            ui.MoveToExit();
        }

        FillAutomaticQueues();
        RepositionQueues();
    }

    // =========================================================
    // SERVED
    // =========================================================

    public void CustomerServed(
        Customer customer)
    {
        if (customer == null)
            return;

        queueA.Remove(customer);
        queueB.Remove(customer);

        if (customer.chairID >= 0)
        {
            SeatingManager.I
                .RemoveCustomerForExit(
                    customer.chairID
                );
        }

        customer.state =
            CustomerState.Leaving;

        CustomerUI ui =
            GetCustomerUI(customer);

        if (ui != null)
        {
            customerVisuals.Remove(
                customer
            );

            ui.MoveToExit();
        }

        FillAutomaticQueues();
        RepositionQueues();
    }

    // =========================================================
    // LOOKUPS
    // =========================================================

    public CustomerUI GetCustomerUI(
        Customer customer)
    {
        CustomerUI ui;

        if (customerVisuals.TryGetValue(
            customer,
            out ui))
        {
            return ui;
        }

        return null;
    }

    public List<Customer> GetQueue(
        CustomerQueue queue)
    {
        return queue == CustomerQueue.A
            ? queueA
            : queueB;
    }

    public CustomerQueue GetShortestQueue()
    {
        return queueA.Count <= queueB.Count
            ? CustomerQueue.A
            : CustomerQueue.B;
    }

    CustomerQueue GetShortestQueueWithSpace()
    {
        if (queueA.Count <
            maximumQueueSize &&
            queueA.Count <= queueB.Count)
        {
            return CustomerQueue.A;
        }

        return CustomerQueue.B;
    }
}