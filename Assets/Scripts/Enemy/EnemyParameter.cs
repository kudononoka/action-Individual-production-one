using System;
using UnityEngine;

[Serializable]
public class EnemyParameter
{
    [Header("HPÅål")]
    [SerializeField] int _hpMax;

    [Header("cUèUÍ")]
    [SerializeField] int _attackVerticalSwingPower;

    [Header("¡UèUÍ")]
    [SerializeField] int _attackHorizontalSwingPower;

    [Header("ÁêUÍ")]
    [SerializeField] int _attackSpecialPower;

    [Header("Xebv©çÌU")]
    [SerializeField] int _attackStepPower;

    public int HPMax => _hpMax;

    /// <summary>cUèUÍ</summary>
    public int AttackVerticalSwingPower => _attackVerticalSwingPower;

    /// <summary>cUèUÍ</summary>
    public int AttackHorizontalSwingPower => _attackHorizontalSwingPower;

    /// <summary>cUèUÍ</summary>
    public int AttackSpecialPower => _attackSpecialPower;

    /// <summary>cUèUÍ</summary>
    public int AttckStepPower => _attackStepPower;  
}
