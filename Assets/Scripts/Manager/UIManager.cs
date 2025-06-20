using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI crystalsText;

    [SerializeField] TextMeshProUGUI moonBreadText;
    [SerializeField] TextMeshProUGUI astroSauceText;
    [SerializeField] TextMeshProUGUI spaceMeatText;

    [SerializeField] BuildButton buildButton;
    [SerializeField] UpgradeButton upgradeButton;

    [SerializeField] GameObject dishesPanel;

    private bool isUnlockedButtons;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        isUnlockedButtons = false;
        dishesPanel.SetActive(false);
    }

    public void UpdateIngredientsUI(int[] values)
    {
        spaceMeatText.text = $"{values[0]}";
        moonBreadText.text = $"{values[1]}";
        astroSauceText.text = $"{values[2]}";
    }

    public void UpdateMoneyUI(int value)
    {
        moneyText.text = $"{value}";
    }

    public void UpdateCrystalsUI(int value)
    {
        crystalsText.text = $"{value}";
    }

    public void UnlockButtons()
    {
        if (isUnlockedButtons) return;
        buildButton.UnlockBuildButton();
        upgradeButton.UnlockUpgradeButton();
        isUnlockedButtons = true;
    }

    public void ShowDishesPanel(bool isShow)
    {
        dishesPanel.SetActive(isShow);
    }

    public bool IsShownDishesPanel()
    {
        return dishesPanel.activeInHierarchy;
    }

}
