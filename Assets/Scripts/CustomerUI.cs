using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    public Customer customer;

    [Header("Sprites")]
    public Sprite enteringSprite;
    public Sprite liningSprite;
    public Sprite waitingSprite;

    [Header("Movement")]
    public float moveSpeed = 400f;

    private Image thisImage;
    private Coroutine moveRoutine;

    private CustomerQueue queue;
    private int queueIndex = -1;

    public CustomerQueue Queue => queue;
    public int QueueIndex => queueIndex;

    private void Awake()
    {
        thisImage = GetComponent<Image>();
    }

    // =========================================================
    // SETUP
    // =========================================================

    public void Setup(
        Customer newCustomer,
        CustomerQueue newQueue)
    {
        customer = newCustomer;
        queue = newQueue;

        SetEntering();
    }

    // =========================================================
    // CLICK
    // =========================================================

    public void Interact()
    {
        if (customer == null)
            return;

        UIManager.I.CallContextUI(this);
    }

    // =========================================================
    // STATES
    // =========================================================

    public void SetEntering()
    {
        if (customer != null)
            customer.state = CustomerState.Arriving;

        SetSprite(enteringSprite);
    }

    public void SetLining()
    {
        if (customer != null)
            customer.state = CustomerState.Waiting;

        SetSprite(liningSprite);
    }

    public void SetWaiting()
    {
        if (customer != null)
            customer.state = CustomerState.Waiting;

        SetSprite(waitingSprite);
    }

    void SetSprite(Sprite sprite)
    {
        if (thisImage == null)
            thisImage = GetComponent<Image>();

        if (thisImage != null)
            thisImage.sprite = sprite;
    }

    // =========================================================
    // MOVEMENT
    // =========================================================

    public void MoveToQueue(
        CustomerQueue newQueue,
        int newIndex)
    {
        queue = newQueue;
        queueIndex = newIndex;

        Transform target =
            CustomerQueueManager.I.GetQueuePosition(
                queue,
                queueIndex
            );

        if (target == null)
            return;

        MoveTo(
            target,
            SetLining
        );
    }

    public void MoveToChair(CustomerChair chair)
    {
        if (chair == null ||
            chair.customerPosition == null)
            return;

        queueIndex = -1;

        MoveTo(
            chair.customerPosition,
            SetWaiting
        );
    }

    public void MoveToExit()
    {
        SetLining();

        Transform target =
            CustomerQueueManager.I.spawnPoint;

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        MoveTo(
            target,
            () => Destroy(gameObject)
        );
    }

    void MoveTo(
        Transform target,
        System.Action onComplete)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine =
            StartCoroutine(
                MoveRoutine(
                    target,
                    onComplete
                )
            );
    }

    IEnumerator MoveRoutine(
        Transform target,
        System.Action onComplete)
    {
        while (target != null &&
               Vector3.Distance(
                   transform.position,
                   target.position
               ) > 1f)
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    moveSpeed *
                    Time.deltaTime
                );

            yield return null;
        }

        if (target != null)
            transform.position = target.position;

        moveRoutine = null;

        if (onComplete != null)
            onComplete();
    }
}