using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishesButton : MonoBehaviour
{
    [SerializeField] string name;

    public void OnClick()
    {
        if (CraftingSystem.Instance.Craft(name))
        {
            UIManager.Instance.ShowDishesPanel(false);
        }
    }
}
