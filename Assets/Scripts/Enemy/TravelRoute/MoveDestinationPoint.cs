using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveDestinationPoint : MonoBehaviour
{
    /// <summary>–Ú“I’n</summary>
    public DestinationPoint[] Point = new DestinationPoint[1];
    
}

[System.Serializable]
public class DestinationPoint
{
    public DestinationPoint(Vector3 point)
    {
        _point = point;
    }
    public DestinationPoint() { }
    public Vector3 _point;
}
