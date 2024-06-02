using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerStateBase
{
    PlayerInputAction _inputAction;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _inputAction = playerController.InputAction;
    }
    public override void OnUpdate()
    {
        //ˆÚ“®Ø‚è‘Ö‚¦
        if (_inputAction.InputMove.magnitude > 0)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        if(_inputAction.IsAttackWeak)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);
        }

        if (_inputAction.IsAttackStrong)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);
        }

        if(_inputAction.IsGuard)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Guard);
        }

        if(_inputAction.IsEvade)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);
        }
    }
}
