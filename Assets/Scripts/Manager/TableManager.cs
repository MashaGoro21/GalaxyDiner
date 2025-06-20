using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager Instance;

    public List<TableSpot> tables = new List<TableSpot>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public TableSpot GetFreeTable()
    {
        foreach(var table in tables)
        {
            if (!table.isOccupied) return table;
        }
        return null;
    }

    public bool HasFreeTable()
    {
        return GetFreeTable() != null;
    }

    public List<TableSpot> GetAll() => new(tables);

    public void SetAll(List<Vector3> tablePositions)
    {
        foreach(var tablePosition in tablePositions)
        {
            BuildManager.Instance.SpawnTable(tablePosition);
        }
    }

    public void ClearAllTables()
    {
        foreach (var t in tables)
        {
            Destroy(t.gameObject);
        }
        tables.Clear();
    }
}
