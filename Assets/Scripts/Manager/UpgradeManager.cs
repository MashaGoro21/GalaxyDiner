using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] int upgradeCost;
    [SerializeField] float speedBonus;
    [SerializeField] int upgradingNumber;

    private int upgradedTimes;
    private float serviceTimeModifier;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        upgradedTimes = 0;
        serviceTimeModifier = 1f;
    }

    public void BuySpeedUpgrade()
    {
        if (CurrencySystem.Instance.SpendCrystals(upgradeCost) && CanBeUpgraded())
        {
            serviceTimeModifier *= speedBonus;
            upgradedTimes++;
        }
    }

    public float GetModifiedServiceTime(float baseTime)
    {
        return baseTime * serviceTimeModifier;
    }

    public bool CanBeUpgraded()
    {
        if (upgradedTimes >= upgradingNumber) return false;
        return true;
    }

    public int GetUpgradedTimes() => upgradedTimes;
    public float GetServiceTimeModifier() => serviceTimeModifier;
    public void SetUpgradedTimes(int amount) => upgradedTimes = amount;
    public float SetServiceTimeModifier(float amount) => serviceTimeModifier = amount;
}
