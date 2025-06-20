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

    public bool SpendMoney(int amount)
    {
        if (money < amount) return false;
        
        money -= amount;
        UIManager.Instance.UpdateMoneyUI(money);
        return true;
    }

    public void AddCrystals(int amount)
    {
        crystals += amount;
        UIManager.Instance.UpdateCrystalsUI(crystals);
    }

    public bool SpendCrystals(int amount)
    {
        if (crystals < amount) return false;
        
        crystals -= amount;
        UIManager.Instance.UpdateCrystalsUI(crystals);
        return true;
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
