using UnityEngine;

public class IngredientButton : MonoBehaviour
{
    [SerializeField] string ingredientName;
    [SerializeField] int cost = 5;

    public void OnClick()
    {
        if(CurrencySystem.Instance.SpendMoney(cost))
        {
            IngredientInventory.Instance.AddIngredient(ingredientName, 1);
        }
    }
}
