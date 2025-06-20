using UnityEngine;

public class TableSpot : MonoBehaviour
{
    public bool isOccupied;

    public void Reserve()
    {
        isOccupied = true;
    }

    public void Free()
    {
        isOccupied = false;
    }
}
