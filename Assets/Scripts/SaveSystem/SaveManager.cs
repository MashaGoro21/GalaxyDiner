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

        foreach (var table in TableManager.Instance.GetTables())
        {
            Table t = new Table
            {
                posX = table.transform.position.x,
                posY = table.transform.position.y,
                posZ = table.transform.position.z,
                rotX = table.transform.rotation.x,
                rotY = table.transform.rotation.y,
                rotZ = table.transform.rotation.z,
                rotW = table.transform.rotation.w
            };
            data.tablePosition.Add(t);
        }

        foreach (var ingredient in IngredientInventory.Instance.GetIngredients())
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
        IngredientInventory.Instance.SetIngredients(restored);

        foreach (var table in data.tablePosition)
        {
            Debug.Log(table);
        }

        TableManager.Instance.ClearAllTables();

        Vector3[] positions = new Vector3[data.tablePosition.Count];
        Quaternion[] rotations = new Quaternion[data.tablePosition.Count];

        for(int i = 0; i < data.tablePosition.Count; i++)
        {
            Vector3 pos = new Vector3(data.tablePosition[i].posX, data.tablePosition[i].posY, data.tablePosition[i].posZ);
            Quaternion rot = new Quaternion(data.tablePosition[i].rotX, data.tablePosition[i].rotY, data.tablePosition[i].rotZ, data.tablePosition[i].rotW);

            positions[i] = pos;
            rotations[i] = rot;
        }

        TableManager.Instance.SetTables(positions, rotations);

        UpgradeManager.Instance.SetUpgradedTimes(data.upgradedTimes);
        UpgradeManager.Instance.SetServiceTimeModifier(data.serviceTimeModifier);
    }
}
