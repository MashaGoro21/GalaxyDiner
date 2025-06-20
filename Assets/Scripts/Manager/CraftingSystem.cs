using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;

    private Dictionary<string, Dictionary<string, int>> recipes;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        recipes = new Dictionary<string, Dictionary<string, int>>();

        recipes["GalaxyBurger"] = new Dictionary<string, int>
        {
            {"SpaceMeat", 1},
            {"MoonBread", 1}
        };

        recipes["AstroHotdog"] = new Dictionary<string, int>
        {
            {"SpaceMeat", 1},
            {"AstroSauce", 1}
        };
    }

    public bool CanCraft(string dish)
    {
        return recipes.ContainsKey(dish) && IngredientInventory.Instance.HasIngredients(recipes[dish]);
    }

    public bool Craft(string dish)
    {
        if (!CanCraft(dish)) return false;

        IngredientInventory.Instance.UseIngredients(recipes[dish]);
        UIManager.Instance.UpdateIngredientsUI(IngredientInventory.Instance.GetIngredientsAmount());
        return true;
    }
}
