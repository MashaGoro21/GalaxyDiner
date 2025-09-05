using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] float serviceTime;
    [SerializeField] float eatingTime;
    [SerializeField] int profit;
    [SerializeField] float moveSpeed;
    [SerializeField] Color[] possibleCustomerColors;

    private static int customersServed = 0;
    private TableSpot assignedTable;
    private NavMeshAgent agent;
    private Animator animator;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private const string IS_WALKING_STRING = "IsWalking";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = 45f;
        agent.acceleration = 8;
        agent.stoppingDistance = 0.1f;
        agent.updateRotation = true;

        animator = GetComponent<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        if (possibleCustomerColors.Length > 0)
        {
            int index = Random.Range(0, possibleCustomerColors.Length);
            skinnedMeshRenderer.materials[0].color = possibleCustomerColors[index];
        }
        else
        {
            Debug.LogWarning("No colors set for client");
        }
    }

    public IEnumerator MoveToRoutine(Vector3 targetPos)
    {
        animator.SetBool(IS_WALKING_STRING, true);
        agent.SetDestination(targetPos);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        animator.SetBool(IS_WALKING_STRING, false);
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
        LoadingBar.Instance.StartLoading(UpgradeManager.Instance.GetModifiedServiceTime(serviceTime));

        yield return new WaitForSeconds(UpgradeManager.Instance.GetModifiedServiceTime(serviceTime));
        
        QueueManager.Instance.isServing = false;
        QueueManager.Instance.RelocateAllCustomers();

        Vector3 targetPos = assignedTable.transform.position;
        StartCoroutine(MoveToTableRoutine(targetPos));
    }

    IEnumerator MoveToTableRoutine(Vector3 targetPos)
    {
        animator.SetBool(IS_WALKING_STRING, true);
        agent.SetDestination(targetPos);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        animator.SetBool(IS_WALKING_STRING, false);
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
        if (customersServed == 5)
        {
            CurrencySystem.Instance.AddCrystals(1);
            assignedTable.PlayPayingCrystalAnimation();
            customersServed = 0;
        }
        else
        {
            CurrencySystem.Instance.AddMoney(profit);
            assignedTable.PlayPayingMoneyAnimation(profit);
        }
    }
}