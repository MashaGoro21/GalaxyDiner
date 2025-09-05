using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public static CurrencySystem Instance;

    [SerializeField] int money;
    [SerializeField] int crystals;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        UIManager.Instance.UpdateMoneyUI(money);
        UIManager.Instance.UpdateCrystalsUI(crystals);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void SpendMoney(int amount)
    {    
        money -= amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void AddCrystals(int amount)
    {
        crystals += amount;
        UIManager.Instance.UpdateCrystalsUI(crystals);
    }

    public void SpendCrystals(int amount)
    {     
        crystals -= amount;
        UIManager.Instance.UpdateCrystalsUI(crystals);
    }

    public int GetMoney() => money;

    public int GetCrystals() => crystals;

    public void SetMoney(int amount) 
    { 
        money = amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void SetCrystals(int amount) 
    { 
        crystals = amount;
        UIManager.Instance.UpdateCrystalsUI(crystals);
    }
}
