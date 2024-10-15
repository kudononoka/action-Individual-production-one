using UnityEngine;
using UniRx;
using System;

/// <summary>プレイヤーのパラメーター</summary>
[System.Serializable]
public class PlayerParameter
{
    [Header("HP最大値")]
    [SerializeField] int _hpMax;

    [Header("歩行速度")]
    [SerializeField] float _walkSpeed;

    [Header("ガード時の歩行速度")]
    [SerializeField] float _guardWalkSpeed;

    [Header("弱攻撃力(コンボ攻撃１・２回目)")]
    [SerializeField] int _attackWeakPower;

    [Header("強攻撃力(コンボ攻撃３・４回目)")]
    [SerializeField] int _attackStrongPower;

    [Header("ため攻撃力")]
    [SerializeField] int _attackChargePower;

    [Header("方向転換速度")]
    [SerializeField] float _rotateSpeed;

    /// <summary>歩行速度</summary>
    public float WalkSpeed => _walkSpeed;

    /// <summary>方向転換速度</summary>
    public float RotateSpeed => _rotateSpeed;

    /// <summary>ガード時歩行速度</summary>
    public float GuardWalkSpeed => _guardWalkSpeed;

    /// <summary>HP最大値</summary>
    public int HPMax => _hpMax;

    /// <summary>弱攻撃力</summary>
    public int AttackWeakPower => _attackWeakPower;

    /// <summary>強攻撃力</summary>
    public int AttackStrongPower => _attackStrongPower;

    /// <summary>溜め攻撃力</summary>
    public int AttackChargePower => _attackChargePower;

}