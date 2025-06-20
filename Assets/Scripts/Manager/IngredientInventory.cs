using System.Collections.Generic;
using UnityEngine;

public class IngredientInventory : MonoBehaviour
{
    public static IngredientInventory Instance;

    private Dictionary<string, int> ingredients;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ingredients = new Dictionary<string, int>();

        ingredients["SpaceMeat"] = 0;
        ingredients["MoonBread"] = 0;
        ingredients["AstroSauce"] = 0;
    }

    private void Start()
    {
        UIManager.Instance.UpdateIngredientsUI(GetIngredientsAmount());
    }

    public void AddIngredient(string name, int amount)
    {
        if (!ingredients.ContainsKey(name)) return;
        
        ingredients[name] += amount;
        UIManager.Instance.UpdateIngredientsUI(GetIngredientsAmount());
    }

    public bool HasIngredients(Dictionary<string, int> required)
    {
        foreach (var entry in required)
        {
            if (!ingredients.ContainsKey(entry.Key) || ingredients[entry.Key] < entry.Value)
                return false;
        }
        return true;
    }

    public void UseIngredients(Dictionary<string, int> required)
    {
        foreach (var entry in required)
        {
            ingredients[entry.Key] -= entry.Value;
        }
    }

    public int[] GetIngredientsAmount()
    {
        return new int[]{ ingredients["SpaceMeat"], ingredients["MoonBread"], ingredients["AstroSauce"]};
    }

    public Dictionary<string, int> GetAll() => new(ingredients);

    public void SetAll(Dictionary<string, int> saved)
    {
        ingredients = new(saved);
    }
}
