using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    [SerializeField] Button button;

    private void Awake()
    {
        button.interactable = false;
    }

    public void OnClick()
    {
        BuildManager.Instance.BuildTable();
    }

    public void UnlockBuildButton()
    {
        button.interactable = true;
    }
}
