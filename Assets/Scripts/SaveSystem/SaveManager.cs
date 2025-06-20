using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public static bool ShouldLoadGame = false;

    private string savePath;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    public void SaveGame()
    {
        GameSaveData data = new GameSaveData();

        data.money = CurrencySystem.Instance.GetMoney();
        data.crystals = CurrencySystem.Instance.GetCrystals();
        data.currentSpotIndex = BuildManager.Instance.GetCurrentSpotIndex();

        data.upgradedTimes = UpgradeManager.Instance.GetUpgradedTimes();
        data.serviceTimeModifier = UpgradeManager.Instance.GetServiceTimeModifier();

        foreach(var table in TableManager.Instance.GetAll())
        {
            data.tablePosition.Add(table.transform.position);
        }

        foreach (var ingredient in IngredientInventory.Instance.GetAll())
        {
            data.ingredients.Add(new IngredientEntry { name = ingredient.Key, amount = ingredient.Value });
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

        CurrencySystem.Instance.SetMoney(data.money);
        CurrencySystem.Instance.SetCrystals(data.crystals);

        BuildManager.Instance.SetCurrentSpotIndex(data.currentSpotIndex);

        Dictionary<string, int> restored = new();
        foreach (var entry in data.ingredients)
        {
            restored[entry.name] = entry.amount;
        }
        IngredientInventory.Instance.SetAll(restored);

        TableManager.Instance.ClearAllTables();
        TableManager.Instance.SetAll(data.tablePosition);

        UpgradeManager.Instance.SetUpgradedTimes(data.upgradedTimes);
        UpgradeManager.Instance.SetServiceTimeModifier(data.serviceTimeModifier);
    }
}
