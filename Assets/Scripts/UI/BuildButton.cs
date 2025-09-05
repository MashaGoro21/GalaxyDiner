using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    private void Update()
    {
        if (!BuildManager.Instance.CanBeBuilt()) UnlockBuildButton(false);
        else UnlockBuildButton(true);
    }

    public void OnClick()
    {
        BuildManager.Instance.BuildTable();
    }

    public void UnlockBuildButton(bool isUnlock)
    {
        button.interactable = isUnlock;
    }
}
