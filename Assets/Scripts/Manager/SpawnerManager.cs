using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelay = 5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelay && QueueManager.Instance.CanAddCustomer())
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    void SpawnCustomer()
    {
        Customer customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Customer>();
        QueueManager.Instance.AddCustomer(customer);
    }
}


