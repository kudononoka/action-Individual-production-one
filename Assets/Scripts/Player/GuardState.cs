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
    DirMovement _dirMovement = new();
    Quaternion targetRotation;
    Transform _lockonTarget;
    PlayerHPSTController _playerHPSTController;
    PlayerParameter _playerParameter;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _walkSpeed = playerController.Parameter.GuardWalkSpeed;
        _rotateSpeed = playerController.Parameter.RotateSpeed;
        _characterController = playerController.CharacterController;
        _inputAction = playerController.InputAction;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _lockonTarget = playerController.CameraController.LockonTarget;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        _mcTra = Camera.main.transform;
    }
    public override void OnEnter()
    {
        targetRotation = _playerTra.rotation;
        _anim.SetBool("Guard", true);
    }
    public override void OnUpdate()
    {
        //移動
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _characterController.Move(moveDir * _walkSpeed * Time.deltaTime);

        //ロックオン中
        if (_inputAction.IsLockon)
        {
            //動く方向によってAnimationを切り替え
            DirMovement.MoveDir dir = _dirMovement.DirMovementJudge(_inputAction.InputMove);
            switch (dir)
            {
                case DirMovement.MoveDir.Forward:
                    _anim.SetBool("IsMoveForward", true);
                    _anim.SetLayerWeight(2, 0);
                    break;
                case DirMovement.MoveDir.Backward:
                    _anim.SetBool("IsMoveForward", false);
                    _anim.SetLayerWeight(2, 0);
                    break;
                case DirMovement.MoveDir.Left:
                    _anim.SetFloat("AnimationSpeed", 1);
                    _anim.SetLayerWeight(2, 1);
                    break;
                case DirMovement.MoveDir.Right:
                    _anim.SetFloat("AnimationSpeed", -1);
                    _anim.SetLayerWeight(2, 1);
                    break;
                default:
                    break;
            }

            //ターゲットの方を向く
            var direction = _lockonTarget.transform.position - _playerTra.transform.position;
            direction.y = 0;
            _playerTra.rotation = Quaternion.LookRotation(direction);

        }
        else
        {
            if (moveDir.magnitude > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            }
            _playerTra.rotation = Quaternion.RotateTowards(_playerTra.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        if (moveDir.magnitude >= 0.1)
        {
            _anim.SetLayerWeight(1, 1);
        }
        else
        {
            _anim.SetLayerWeight(1, 0);
            _anim.SetLayerWeight(2, 0);
        }

        _anim.SetFloat("move", moveDir.magnitude);

        if (!_inputAction.IsGuard)
        {
            if(moveDir.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int) PlayerStateMachine.StateType.Walk);
        }

        if(_inputAction.IsEvade && _playerHPSTController.CurrntStValue >= _playerParameter.EvadeSTCost)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);
    }

    public override void OnEnd()
    {
        _anim.SetBool("Guard", false);
        _anim.SetLayerWeight(1, 0);
    }
}
