using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager Instance;

    [SerializeField] Transform queueStartPoint;
    [SerializeField] float spacing;
    [SerializeField] int queueSize;
    
    public bool isServing;
    
    private List<Vector3> positionList;
    private List<Customer> customerQueue;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        isServing = false;

        positionList = new List<Vector3>();
        customerQueue = new List<Customer>();
        
        for(int i = 0; i < queueSize; i++)
        {
            positionList.Add(queueStartPoint.position + new Vector3(1, 0) * spacing * i);
        }
    }

    public bool CanAddCustomer()
    {
        return customerQueue.Count < positionList.Count;
    }

    public void AddCustomer(Customer customer)
    {
        customerQueue.Add(customer);
        StartCoroutine(customer.MoveToRoutine(positionList[customerQueue.IndexOf(customer)]));
    }

    private Customer GetFirstInQueue()
    {
        if(customerQueue.Count == 0) return null;

        Customer customer = customerQueue[0];
        customerQueue.RemoveAt(0);
        return customer;
    }

    public void TryServe()
    {
        if (customerQueue.Count == 0) return;

        if(TableManager.Instance.HasFreeTable() && !isServing)
        {
            Customer first = GetFirstInQueue();
            isServing = true;
            first.StartService();
        }
    }

    public void RelocateAllCustomers()
    {
        for(int i = 0; i < customerQueue.Count; i++)
        {
            StartCoroutine(customerQueue[i].MoveToRoutine(positionList[i]));
        }
    }
}

