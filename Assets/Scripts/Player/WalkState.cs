using System;
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
    Transform _lockonTarget;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _walkSpeed = playerController.Parameter.WalkSpeed;
        _rotateSpeed = playerController.Parameter.RotateSpeed;
        _characterController = playerController.CharacterController;
        _inputAction = playerController.InputAction;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _lockonTarget = playerController.CameraController.LockonTarget;
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
        //移動
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _characterController.Move(moveDir * _walkSpeed * Time.deltaTime);

        //ロックオン中
        if (_inputAction.IsLockon)
        {
            //動く方向によってAnimationを切り替え
            MoveDir dir = DirMovementJudge(_inputAction.InputMove);
            switch(dir)
            {
                case MoveDir.Forward:
                    _anim.SetBool("IsMoveForward", true);
                    _anim.SetLayerWeight(2, 0);
                    break;
                case MoveDir.Backward:
                    _anim.SetBool("IsMoveForward", false);
                    _anim.SetLayerWeight(2, 0);
                    break;
                case MoveDir.Left:
                    _anim.SetFloat("AnimationSpeed", 1);
                    _anim.SetLayerWeight(2, 1);
                    break;
                case MoveDir.Right:
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
            _anim.SetBool("IsMoveForward", true);
            _anim.SetLayerWeight(2, 0);
            if (moveDir.magnitude > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            }
            //移動する方向に向く
            _playerTra.rotation = Quaternion.RotateTowards(_playerTra.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        _anim.SetFloat("move", moveDir.magnitude);

        //遷移先
        if (_inputAction.IsAttackWeak)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);

        if (_inputAction.IsAttackStrong)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);

        if (_inputAction.IsGuard)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Guard);

        if (moveDir.magnitude <= 0)     //突っ立ってる状態
        {
            _anim.SetLayerWeight(2, 0);
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
        }
    }

    /// <summary>動いている方向</summary>
    enum MoveDir
    {
        Forward,
        Backward,
        Left,
        Right,
        NotMove,
    }
    /// <summary>前後右左どの方向に動いているのか計算</summary>
    private MoveDir DirMovementJudge(Vector2 moveDir)
    {
        Vector3 vec = moveDir.normalized;
        if (vec.magnitude == 0)
        {
            return MoveDir.NotMove;
        }

        if (vec.x >= -0.5 && vec.x <= 0.5)
        {
            if (vec.y >= 0)
            {
                return MoveDir.Forward;
            }
            else
            {
                return MoveDir.Backward;
            }
        }
        else
        {
            if (vec.x >= 0)
            {
                return MoveDir.Right;
            }
            else
            {
                return MoveDir.Left;
            }
        }
    }

    public override void OnEnd() { }
}
