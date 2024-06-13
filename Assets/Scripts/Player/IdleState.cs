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
        //移動切り替え
        if (_inputAction.InputMove.magnitude > 0)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        if(_inputAction.IsAttackWeak && _playerHPSTController.CurrntStValue >= _playerParameter.AttackWeakSTCost)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);
        }

        if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);
        }

        if(_inputAction.IsGuard && _playerHPSTController.CurrntStValue >= _playerParameter.GuardHitSTCost)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Guard);
        }

        if(_inputAction.IsEvade && _playerHPSTController.CurrntStValue >= _playerParameter.EvadeSTCost)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);
        }
    }
}
