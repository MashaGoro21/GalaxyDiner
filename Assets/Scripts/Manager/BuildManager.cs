using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [SerializeField] GameObject tablePrefab;
    [SerializeField] Transform[] buildSpots;
    [SerializeField] int tableCost;

    private int currentSpotIndex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentSpotIndex = 0;
    }

    public void BuildTable()
    {
        if (CanBeBuilt() && CurrencySystem.Instance.SpendMoney(tableCost))
        {
            Transform spot = buildSpots[currentSpotIndex];
            SpawnTable(spot.position);
            currentSpotIndex++;

            QueueManager.Instance.TryServe();
        }
    }

    public void SpawnTable(Vector3 position)
    {
        TableSpot table = Instantiate(tablePrefab, position, Quaternion.identity).GetComponent<TableSpot>();
        TableManager.Instance.tables.Add(table);
    }

    public bool CanBeBuilt()
    {
        if (currentSpotIndex >= buildSpots.Length) return false;
        return true;
    }

    public int GetCurrentSpotIndex() => currentSpotIndex;

    public int SetCurrentSpotIndex(int amount) => currentSpotIndex = amount;
}
