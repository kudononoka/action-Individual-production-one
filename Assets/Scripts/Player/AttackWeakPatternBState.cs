using System;
using UnityEngine;

[Serializable]
public class AttackWeakPatternBState : PlayerStateBase
{
    [Header("弱攻撃にかかる時間")]
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
        PlayerController playerController = _playerStateMachine.PlayerController;
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
        _playerHPSTController.STDown(_playerParameter.AttackWeakSTCost);
        _inputAction.IsAttackWeak = false;
        _coolTimer = _coolTime;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 0);
        _weapon.DamageColliderEnabledSet(true);
        _weapon.Damage = _playerParameter.AttackWeakPower;
        _makeASound.IsSoundChange(true);
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;
        if(_coolTimer < _nextAttackJudgeTime)
        {
            if (_coolTimer < _nextAttackTime)
            {
                if (_inputAction.IsAttackWeak && _playerHPSTController.CurrntStValue >= _playerParameter.AttackWeakSTCost)
                {
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);
                }
                else if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
                {
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);
                }
            }
        }
        else
        {
            _inputAction.IsAttackWeak = false;
        }

        if (_coolTimer <= 0.2)
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
        _inputAction.IsAttackWeak = false;
        _weapon.DamageColliderEnabledSet(false);
        _makeASound.IsSoundChange(false);
    }
}
