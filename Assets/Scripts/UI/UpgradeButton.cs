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

    private void Update()
    {
        Debug.Log(UpgradeManager.Instance.CanBeUpgraded());
        if (!UpgradeManager.Instance.CanBeUpgraded()) UnlockUpgradeButton(false);
        else UnlockUpgradeButton(true);
    }

    public void OnClick()
    {
        UpgradeManager.Instance.BuySpeedUpgrade();
    }

    public void UnlockUpgradeButton(bool isUnlock)
    {
        button.interactable = isUnlock;
    }
}
