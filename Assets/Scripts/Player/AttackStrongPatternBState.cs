﻿using System;
using UnityEngine;

[Serializable]
public class AttackStrongPatternBState : PlayerStateBase
{
    [Header("強攻撃にかかる時間")]
    [SerializeField]
    float _coolTime;

    [Header("素振り音を鳴らすタイミング")]
    [SerializeField]
    float _soundTime;

    /// <summary>素振り音を鳴らしたかどうか</summary>
    bool _isSound = false;

    float _coolTimer;

    Animator _anim;

    PlayerInputAction _inputAction;

    Transform _playerTra;

    Weapon _weapon;

    PlayerParameter _playerParameter;

    MakeASound _makeASound;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _weapon = playerController.PlayerWeapon;
        _playerParameter = playerController.Parameter;
        _makeASound = playerController.MakeASound;
    }
    public override void OnEnter()
    {
        _inputAction.IsAttackStrong = false;
        _coolTimer = _coolTime;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 1);
        _weapon.DamageColliderEnabledSet(true);
        _weapon.Damage = _playerParameter.AttackStrongPower;
        _makeASound.IsSoundChange(true);
        _isSound = false;
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0)
        {
            //移動かIdleに遷移
            if (_inputAction.InputMove.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        if (!_isSound && _coolTimer <= _soundTime)
        {
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackStrongSwish);
            _isSound = true;
        }
    }
    public override void OnEnd()
    {
        _inputAction.IsAttackStrong = false;
        _weapon.DamageColliderEnabledSet(false);
        _makeASound.IsSoundChange(false);
    }
}
