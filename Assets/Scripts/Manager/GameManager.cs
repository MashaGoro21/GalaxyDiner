using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] int money = 1000;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public bool SpendMoney(int amount)
    {
        if(money >= amount)
        {
            money -= amount;
            UIManager.Instance.UpdateMoneyUI(money);
            return true;
        }
        return false;
    }
}
