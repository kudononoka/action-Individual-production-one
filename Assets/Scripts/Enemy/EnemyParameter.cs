using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyParameter
{
    [Header("HP�ő�l")]
    [SerializeField] int _hpMax;

    public int HPMax => _hpMax;
}
