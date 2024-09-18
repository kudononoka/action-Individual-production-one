using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerStateBase
{
    PlayerInputAction _inputAction;

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _inputAction = playerController.InputAction;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
    }
    public override void OnUpdate()
    {
        //遷移
        if (_inputAction.InputMove.magnitude > 0)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        if(_inputAction.IsAttack && _playerHPSTController.CurrntStValue >= _playerParameter.AttackWeakSTCost)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackComboOne);
        }

        if(_inputAction.IsEvade && _playerHPSTController.CurrntStValue >= _playerParameter.EvadeSTCost)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);
        }
    }
}
