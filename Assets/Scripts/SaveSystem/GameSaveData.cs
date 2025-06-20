using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public int money;
    public int crystals;
    
    public int currentSpotIndex;
    public List<Vector3> tablePosition = new();

    public List<IngredientEntry> ingredients = new();

    public int upgradedTimes;
    public float serviceTimeModifier;
}

[Serializable]
public class IngredientEntry
{
    public string name;
    public int amount;
}
