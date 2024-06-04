using System;
using UnityEngine;

[Serializable]
public class AttackStrongPatternBState : PlayerStateBase
{
    [Header("ã≠çUåÇÇ…Ç©Ç©ÇÈéûä‘")]
    [SerializeField]
    float _coolTime;

    float _coolTimer;
    Animator _anim;
    PlayerInputAction _inputAction;
    Transform _playerTra;
    Weapon _weapon;
    PlayerParameter _playerParameter;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _weapon = playerController.PlayerWeapon;
        _playerParameter = playerController.Parameter;
    }
    public override void OnEnter()
    {
        _inputAction.IsAttackStrong = false;
        _coolTimer = _coolTime;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 1);
        _weapon.DamageColliderEnabledSet(true);
        _weapon.Damage = _playerParameter.AttackStrongPower;
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0)
        {
            //à⁄ìÆÇ©IdleÇ…ëJà⁄
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
    }
}
