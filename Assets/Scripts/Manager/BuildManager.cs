using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [SerializeField] GameObject tablePrefab;
    [SerializeField] Transform[] buildSpots;
    [SerializeField] int tableCost = 200;

    private int currentSpotIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void BuildTable()
    {
        if (currentSpotIndex < buildSpots.Length && CurrencySystem.Instance.SpendMoney(tableCost))
        {
            Transform spot = buildSpots[currentSpotIndex];
            TableSpot table = Instantiate(tablePrefab, spot.position, Quaternion.identity).GetComponent<TableSpot>().GetComponent<TableSpot>();
            TableManager.Instance.tables.Add(table);
            QueueManager.Instance.TryServe();
            currentSpotIndex++;
        }
    }
}
