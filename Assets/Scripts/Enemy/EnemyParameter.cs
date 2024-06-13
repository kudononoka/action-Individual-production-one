using System;
using UnityEngine;

[Serializable]
public class EnemyParameter
{
    [Header("HP最大値")]
    [SerializeField] int _hpMax;

    [Header("縦振り攻撃力")]
    [SerializeField] int _attackVerticalSwingPower;

    [Header("横振り攻撃力")]
    [SerializeField] int _attackHorizontalSwingPower;

    [Header("特殊攻撃力")]
    [SerializeField] int _attackSpecialPower;

    [Header("ステップからの攻撃")]
    [SerializeField] int _attackStepPower;

    public int HPMax => _hpMax;

    /// <summary>縦振り攻撃力</summary>
    public int AttackVerticalSwingPower => _attackVerticalSwingPower;

    /// <summary>縦振り攻撃力</summary>
    public int AttackHorizontalSwingPower => _attackHorizontalSwingPower;

    /// <summary>縦振り攻撃力</summary>
    public int AttackSpecialPower => _attackSpecialPower;

    /// <summary>縦振り攻撃力</summary>
    public int AttckStepPower => _attackStepPower;  
}
