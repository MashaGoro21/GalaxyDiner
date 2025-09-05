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

    public List<TableSpot> GetTables() => new(tables);

    public void SetTables(Vector3[] positions, Quaternion[] rotations)
    {
        for(int i = 0; i < positions.Length; i++)
        {
            BuildManager.Instance.SpawnTable(positions[i], rotations[i]);
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
