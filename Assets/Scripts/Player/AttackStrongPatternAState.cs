using System;
using UnityEngine;

[Serializable]
public class AttackStrongPatternAState : PlayerStateBase
{
    [Header("強攻撃にかかる時間")]
    [SerializeField]
    float _coolTime;

    [Header("次の攻撃をするかが確定される時間")]
    [SerializeField]
    float _nextAttackJudgeTime;

    [Header("次の攻撃につながるまでの時間")]
    [SerializeField]
    float _nextAttackTime;

    float _coolTimer;

    Animator _anim;

    PlayerInputAction _inputAction;

    Transform _playerTra;

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    Weapon _weapon;

    MakeASound _makeASound;
    public override void Init()
    {
        PlayerController　playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        _weapon = playerController.PlayerWeapon;
        _makeASound = playerController.MakeASound;
    }
    public override void OnEnter()
    {
        _playerHPSTController.STDown(_playerParameter.AttackStrongSTCost);
        _inputAction.IsAttackStrong = false;
        _coolTimer = _coolTime;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 1);
        _weapon.DamageColliderEnabledSet(true);
        _weapon.Damage = _playerParameter.AttackStrongPower;
        _makeASound.IsSoundChange(true);
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;
        if(_coolTimer < _nextAttackJudgeTime)
        {
            if (_coolTimer < _nextAttackTime)
            {
                if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
                {
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternB);
                }
            }
        }
        else
        {
            _inputAction.IsAttackStrong = false;
        }

        if (_coolTimer <= 0.95)
        {
            //移動かIdleに遷移
            if (_inputAction.InputMove.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }
    }
    public override void OnEnd()
    {
        _inputAction.IsAttackStrong = false;
        _weapon.DamageColliderEnabledSet(false);
        _makeASound.IsSoundChange(false);
    }
}
