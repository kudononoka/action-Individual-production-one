using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class EvadeState : PlayerStateBase
{

    [Header("回避スピード")]
    [SerializeField]
    float _moveSpeed;

    [Header("回避距離")]
    [SerializeField]
    float _evadeDistance;

    [Header("回避エフェクト")]
    [SerializeField]
    ParticleSystem _particle;

    float _coolTimer;

    Animator _anim;

    Transform _playerTra;

    PlayerInputAction _inputAction;

    Transform _lockonTarget;

    Transform _mcTra;

    CharacterController _characterController;

    DirMovement _dirMovement = new();

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    CapsuleCollider _capsuleCollider;

    MakeASound _makeASound;

    CameraController _cameraController;

    Vector3 _moveDir;

    Vector3 _pos;

    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _inputAction = playerController.InputAction;
        _playerTra = playerController.PlayerTra;
        _cameraController = playerController.CameraController;
        _characterController = playerController.CharacterController;
        _mcTra = Camera.main.transform;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        _capsuleCollider = playerController.CapsuleCollider;
        _makeASound = playerController.MakeASound;
    }
    public override void OnEnter()
    {
        _playerHPSTController.STDown(_playerParameter.EvadeSTCost);

        _inputAction.IsEvade = false;

        _pos = _playerTra.position;

        //回避方向
        //var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        //_moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _moveDir = _playerTra.forward;

        _anim.SetInteger("EvadeType", 0);

        _anim.SetTrigger("Evade");
        _capsuleCollider.enabled = false;

        _makeASound.IsSoundChange(true);
        _particle.Play();
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;
        //var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        //var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        _characterController.Move(_moveDir * _moveSpeed * Time.deltaTime);

        if(_inputAction.IsLockon)
        {
            _lockonTarget = _cameraController.LockonTarget;
            var direction = _lockonTarget.transform.position - _playerTra.transform.position;
            direction.y = 0;
            _playerTra.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            _playerTra.rotation = Quaternion.LookRotation(_moveDir, Vector3.up);
        }

        if (Vector3.Distance(_pos, _playerTra.position) >= _evadeDistance)
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
        _capsuleCollider.enabled = true;
        _makeASound.IsSoundChange(false);
    }
}
