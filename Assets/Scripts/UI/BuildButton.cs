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

    public void OnClick()
    {
        BuildManager.Instance.BuildTable();

        if(!BuildManager.Instance.CanBeBuilt())
        {
            UnlockBuildButton(false);
        }
    }

    public void UnlockBuildButton(bool isUnlock)
    {
        button.interactable = isUnlock;
    }
}
