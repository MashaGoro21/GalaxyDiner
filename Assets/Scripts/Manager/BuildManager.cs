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
        if (!CanBeBuilt()) return;

        CurrencySystem.Instance.SpendMoney(tableCost);
        Transform spot = buildSpots[currentSpotIndex];
        SpawnTable(spot.position, spot.rotation);
        Destroy(spot.gameObject);
        currentSpotIndex++;

        QueueManager.Instance.TryServe();
    }

    public void SpawnTable(Vector3 pos, Quaternion rot)
    {
        TableSpot table = Instantiate(tablePrefab, pos, rot).GetComponent<TableSpot>();
        TableManager.Instance.tables.Add(table);
    }

    public bool CanBeBuilt()
    {
        if (currentSpotIndex >= buildSpots.Length || tableCost > CurrencySystem.Instance.GetMoney()) return false;
        return true;
    }

    public int GetCurrentSpotIndex() => currentSpotIndex;

    public int SetCurrentSpotIndex(int amount) => currentSpotIndex = amount;
}
