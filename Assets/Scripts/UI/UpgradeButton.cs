using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Button button;

    private void Awake()
    {
        button.interactable = false;
    }

    public void OnClick()
    {
        UpgradeManager.Instance.BuySpeedUpgrade();
    }

    public void UnlockUpgradeButton()
    {
        button.interactable = true;
    }
}
