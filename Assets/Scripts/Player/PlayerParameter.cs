using UnityEngine;
using UniRx;
using System;

/// <summary>�v���C���[�̃p�����[�^�[</summary>
[System.Serializable]
public class PlayerParameter
{
    [Header("HP�ő�l")]
    [SerializeField] int _hpMax;

    [Header("ST�ő�l")]
    [SerializeField] float _stMax;

    [Header("ST�񕜑��x")]
    [SerializeField] float _stRecoverySpeed;

    [Header("���s���x")]
    [SerializeField] float _walkSpeed;

    [Header("�K�[�h���̕��s���x")]
    [SerializeField] float _guardWalkSpeed;

    [Header("�����]�����x")]
    [SerializeField] float _rotateSpeed;

    [Header("��U��ST����l")]
    [SerializeField] float _attackWeakSTCost;

    [Header("���U��ST����l")]
    [SerializeField] float _attackStrongSTCost;

    [Header("���ST����l")]
    [SerializeField] float _evadeSTCost;

    [Header("�K�[�h���G�̍U����Hit��������ST����l")]
    [SerializeField] float _guardHitSTCost;

    /// <summary>���s���x</summary>
    public float WalkSpeed => _walkSpeed;

    /// <summary>�����]�����x</summary>
    public float RotateSpeed => _rotateSpeed;

    /// <summary>�K�[�h�����s���x</summary>
    public float GuardWalkSpeed => _guardWalkSpeed;

    /// <summary>HP�ő�l</summary>
    public int HPMax => _hpMax;

    /// <summary>ST�ő�l</summary>
    public float STMax => _stMax;

    /// <summary>��U���ɂ�����ST�l</summary>
    public float AttackWeakSTCost => _attackWeakSTCost;

    /// <summary>���U���ɂ�����ST�l</summary>
    public float AttackStrongSTCost => _attackStrongSTCost;

    /// <summary>����ɂ�����ST�l</summary>
    public float EvadeSTCost => _evadeSTCost;

    /// <summary>�J�[�h���G�̍U���������������ɂ�����ST�l</summary>
    public float GuardHitSTCost => _guardHitSTCost;

    public float StRecoverySpeed => _stRecoverySpeed;

}