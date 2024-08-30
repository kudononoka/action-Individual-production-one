using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    SlowSystem _slowSystem = new();

    public SlowSystem SlowSystem => _slowSystem;
}
