using UnityEngine;
using UnityEngine.UI;

public class DishesButton : MonoBehaviour
{
    [SerializeField] string name;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnClick()
    {
        if (CraftingSystem.Instance.Craft(name))
        {
            UIManager.Instance.ShowDishesPanel(false);
        }
    }

    private void Update()
    {
        if (!CraftingSystem.Instance.CanCraft(name)) button.interactable = false;
        else button.interactable = true;
    }
}
