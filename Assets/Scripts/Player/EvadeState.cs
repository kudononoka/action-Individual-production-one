using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class EvadeState : PlayerStateBase
{
    [Header("回避にかかる時間")]
    [SerializeField]
    float _coolTime;

    [Header("回避スピード")]
    [SerializeField]
    float _moveSpeed;

    Animator _anim;
    Transform _playerTra;
    PlayerInputAction _inputAction;
    Transform _lockonTarget;
    Transform _mcTra;
    CharacterController _characterController;
    DirMovement _dirMovement = new();
    float _coolTimer;
    PlayerHPSTController _playerHPSTController;
    PlayerParameter _playerParameter;
    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _inputAction = playerController.InputAction;
        _playerTra = playerController.PlayerTra;
        _lockonTarget = playerController.CameraController.LockonTarget;
        _characterController = playerController.CharacterController;
        _mcTra = Camera.main.transform;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
    }
    public override void OnEnter()
    {
        _playerHPSTController.STDown(_playerParameter.EvadeSTCost);

        _inputAction.IsEvade = false;

        _coolTimer = _coolTime;

        //回避方向
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;

        if (_inputAction.IsLockon)
        {
            //動く方向によってAnimationを切り替え
            DirMovement.MoveDir dir = _dirMovement.DirMovementJudge(_inputAction.InputMove);
            switch (dir)
            {
                case DirMovement.MoveDir.Forward:
                    _anim.SetInteger("EvadeType", 0);
                    break;
                case DirMovement.MoveDir.Backward:
                    _anim.SetInteger("EvadeType", 1);
                    break;
                case DirMovement.MoveDir.Left:
                    _anim.SetInteger("EvadeType", 2);
                    break;
                case DirMovement.MoveDir.Right:
                    _anim.SetInteger("EvadeType", 3);
                    break;
                default:
                    break;
            }
        }
        else
        {
            _anim.SetInteger("EvadeType", 0);
        }

        _anim.SetTrigger("Evade");
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _characterController.Move(moveDir * _moveSpeed * Time.deltaTime);

        if(_inputAction.IsLockon)
        {
            var direction = _lockonTarget.transform.position - _playerTra.transform.position;
            direction.y = 0;
            _playerTra.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            _playerTra.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }

        if (_coolTimer <= 0)
        {
            if (_inputAction.IsAttackWeak && _playerHPSTController.CurrntStValue >= _playerParameter.AttackWeakSTCost)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);

            else if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);

            else if (_inputAction.InputMove.magnitude > 0.1f)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
        }
    }

    public override void OnEnd()
    {
        _inputAction.IsEvade = false;
    }
}
