using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientButton : MonoBehaviour
{
    public string ingredientName;
    public int cost = 5;

    public void OnClick()
    {
        if(CurrencySystem.Instance.SpendMoney(cost))
        {
            IngredientInventory.Instance.AddIngredient(ingredientName, 1);
        }
    }
}
