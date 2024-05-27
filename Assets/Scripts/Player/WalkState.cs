using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerStateBase
{
    float _walkSpeed;
    PlayerInputAction _inputAction;
    CharacterController _characterController;
    Animator _anim;
    Transform _playerTra;
    Transform _mcTra;
    float _rotateSpeed;

    Quaternion targetRotation;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _walkSpeed = playerController.Parameter.WalkSpeed;
        _rotateSpeed = playerController.Parameter.RotateSpeed;
        _characterController = playerController.CharacterController;
        _inputAction = playerController.InputAction;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _mcTra = Camera.main.transform;
    }
    public override void OnEnter()
    {
        _anim.SetFloat("move", 1);
        targetRotation = _playerTra.rotation;
    }

    public override void OnUpdate()
    {
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        //ˆÚ“®
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _characterController.Move(moveDir * _walkSpeed * Time.deltaTime);

        if(moveDir.magnitude > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }

        _playerTra.rotation = Quaternion.RotateTowards(_playerTra.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

        _anim.SetFloat("move", moveDir.magnitude);

        if (_inputAction.IsAttackWeak)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);
        }

        if (_inputAction.IsAttackStrong)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);
        }

        if (_inputAction.IsGuard)
        {
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Guard);
        }
        //‘JˆÚæ
        //—§‚Á‚Ä‚¢‚éó‘Ô
        if (moveDir.magnitude <= 0)
        {
            
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
        }
    }

    public override void OnEnd()
    { 
    }
}
