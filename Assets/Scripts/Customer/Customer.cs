using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] float serviceTime = 2f;
    [SerializeField] float eatingTime = 4f;
    [SerializeField] int profit = 50;
    [SerializeField] float moveSpeed = 2f;

    private static int customersServed = 0;

    private TableSpot assignedTable;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = 180f;
        agent.acceleration = 8;
        agent.stoppingDistance = 0.01f;
        agent.updateRotation = true;
    }

    public void MoveTo(Vector3 targetPos)
    {
        StartCoroutine(MoveToRoutine(targetPos));
    }

    IEnumerator MoveToRoutine(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        QueueManager.Instance.TryServe();
    }

    public void StartService()
    {
        assignedTable = TableManager.Instance.GetFreeTable();
        if (assignedTable != null)
        {
            assignedTable.Reserve();

            UIManager.Instance.ShowDishesPanel(true);
            StartCoroutine(ChooseDish());
        }
    }

    IEnumerator ChooseDish()
    {
        while(UIManager.Instance.IsShownDishesPanel())
        {
            yield return null;
        }

        StartCoroutine(ServiceRoutine());
    }

    IEnumerator ServiceRoutine()
    {
        yield return new WaitForSeconds(UpgradeManager.Instance.GetModifiedServiceTime(serviceTime));
        QueueManager.Instance.isServing = false;
        QueueManager.Instance.RelocateAllCustomers();
        Vector3 targetPos = assignedTable.transform.position;
        StartCoroutine(MoveToTableRoutine(targetPos));
    }

    IEnumerator MoveToTableRoutine(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        StartCoroutine(EatAndLeave());
    }

    IEnumerator EatAndLeave()
    {
        yield return new WaitForSeconds(eatingTime);
        assignedTable.Free();
        PayForEating();
        UIManager.Instance.UnlockButtons();
        Destroy(gameObject);
        QueueManager.Instance.TryServe();
    }

    public void PayForEating()
    {
        customersServed++;
        if(customersServed == 5)
        {
            CurrencySystem.Instance.AddCrystals(1);
            customersServed = 0;
        }
        else
        {
            CurrencySystem.Instance.AddMoney(profit);
        }
        
    }
}


