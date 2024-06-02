using System;
using UnityEngine;

[Serializable]
public class AttackStrongPatternAState : PlayerStateBase
{
    [Header("‹­UŒ‚‚É‚©‚©‚éŠÔ")]
    [SerializeField]
    float _coolTime;

    [Header("Ÿ‚ÌUŒ‚‚ğ‚·‚é‚©‚ªŠm’è‚³‚ê‚éŠÔ")]
    [SerializeField]
    float _nextAttackJudgeTime;

    [Header("Ÿ‚ÌUŒ‚‚É‚Â‚È‚ª‚é‚Ü‚Å‚ÌŠÔ")]
    [SerializeField]
    float _nextAttackTime;

    float _coolTimer;
    Animator _anim;
    PlayerInputAction _inputAction;
    Transform _playerTra;
    PlayerHPSTController _playerHPSTController;
    PlayerParameter _playerParameter;
    public override void Init()
    {
        PlayerController@playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        
    }
    public override void OnEnter()
    {
        _playerHPSTController.STDown(_playerParameter.AttackStrongSTCost);
        _inputAction.IsAttackStrong = false;
        _coolTimer = _coolTime;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 1);
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
            //ˆÚ“®‚©Idle‚É‘JˆÚ
            if (_inputAction.InputMove.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }
    }
    public override void OnEnd()
    {
        _inputAction.IsAttackStrong = false;
    }
}
