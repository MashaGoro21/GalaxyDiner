using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    public void OnClick()
    {
        UpgradeManager.Instance.BuySpeedUpgrade();

        if(!UpgradeManager.Instance.CanBeUpgraded())
        {
            UnlockUpgradeButton(false);
        }

    }

    public void UnlockUpgradeButton(bool isUnlock)
    {
        button.interactable = isUnlock;
    }
}
