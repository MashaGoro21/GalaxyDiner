using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] int upgradeCost = 1;
    [SerializeField] float speedBonus = 0.8f;
    
    private float serviceTimeModifier = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void BuySpeedUpgrade()
    {
        if(CurrencySystem.Instance.SpendCrystals(upgradeCost))
        {
            serviceTimeModifier *= speedBonus;
        }
    }

    public float GetModifiedServiceTime(float baseTime)
    {
        Debug.Log(baseTime * serviceTimeModifier);
        return baseTime * serviceTimeModifier;
    }
}
