using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : PlayerStateBase
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
        _walkSpeed = playerController.Parameter.GuardWalkSpeed;
        _rotateSpeed = playerController.Parameter.RotateSpeed;
        _characterController = playerController.CharacterController;
        _inputAction = playerController.InputAction;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _mcTra = Camera.main.transform;
    }
    public override void OnEnter()
    {
        targetRotation = _playerTra.rotation;
        _anim.SetBool("Guard", true);
    }
    public override void OnUpdate()
    {
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        //ˆÚ“®
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _characterController.Move(moveDir * _walkSpeed * Time.deltaTime);

        if (moveDir.magnitude > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            _anim.SetLayerWeight(1, 1);
        }
        else
        {
            _anim.SetLayerWeight(1, 0);
        }

        _playerTra.rotation = Quaternion.RotateTowards(_playerTra.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

        _anim.SetFloat("move", moveDir.magnitude);

        if (!_inputAction.IsGuard)
        {
            if(moveDir.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int) PlayerStateMachine.StateType.Walk);
        }
    }

    public override void OnEnd()
    {
        _anim.SetBool("Guard", false);
        _anim.SetLayerWeight(1, 0);
    }
}
