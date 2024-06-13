using UnityEngine;
using UniRx;
using System;

/// <summary>プレイヤーのパラメーター</summary>
[System.Serializable]
public class PlayerParameter
{
    [Header("HP最大値")]
    [SerializeField] int _hpMax;

    [Header("ST最大値")]
    [SerializeField] float _stMax;

    [Header("ST回復速度")]
    [SerializeField] float _stRecoverySpeed;

    [Header("歩行速度")]
    [SerializeField] float _walkSpeed;

    [Header("ガード時の歩行速度")]
    [SerializeField] float _guardWalkSpeed;

    [Header("弱攻撃力")]
    [SerializeField] int _attackWeakPower;

    [Header("強攻撃力")]
    [SerializeField] int _attackStrongPower;

    [Header("方向転換速度")]
    [SerializeField] float _rotateSpeed;

    [Header("弱攻撃ST消費値")]
    [SerializeField] float _attackWeakSTCost;

    [Header("強攻撃ST消費値")]
    [SerializeField] float _attackStrongSTCost;

    [Header("回避ST消費値")]
    [SerializeField] float _evadeSTCost;

    [Header("ガード時敵の攻撃がHitした時のST消費値")]
    [SerializeField] float _guardHitSTCost;

    /// <summary>歩行速度</summary>
    public float WalkSpeed => _walkSpeed;

    /// <summary>方向転換速度</summary>
    public float RotateSpeed => _rotateSpeed;

    /// <summary>ガード時歩行速度</summary>
    public float GuardWalkSpeed => _guardWalkSpeed;

    /// <summary>HP最大値</summary>
    public int HPMax => _hpMax;

    /// <summary>ST最大値</summary>
    public float STMax => _stMax;

    /// <summary>弱攻撃力</summary>
    public int AttackWeakPower => _attackWeakPower;

    /// <summary>強攻撃力</summary>
    public int AttackStrongPower => _attackStrongPower;

    /// <summary>弱攻撃にかかるST値</summary>
    public float AttackWeakSTCost => _attackWeakSTCost;

    /// <summary>強攻撃にかかるST値</summary>
    public float AttackStrongSTCost => _attackStrongSTCost;

    /// <summary>回避にかかるST値</summary>
    public float EvadeSTCost => _evadeSTCost;

    /// <summary>カード時敵の攻撃が当たった時にかかるST値</summary>
    public float GuardHitSTCost => _guardHitSTCost;

    /// <summary>STの回復スピード</summary>
    public float StRecoverySpeed => _stRecoverySpeed;

}