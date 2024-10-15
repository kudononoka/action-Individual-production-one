using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerStateBase
{
    PlayerInputAction _inputAction;

    PlayerParameter _playerParameter;

    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _inputAction = playerController.InputAction;
        _playerParameter = playerController.Parameter;
    }
    public override void OnUpdate()
    {
        //遷移
        if (_inputAction.InputMove.magnitude > 0)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        if(_inputAction.IsAttack)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.ChargeAttack);
        }

        if(_inputAction.IsEvade)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);
        }
    }
}
