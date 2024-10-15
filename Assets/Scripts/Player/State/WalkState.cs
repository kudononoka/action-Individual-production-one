using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class WalkState : PlayerStateBase
{
    [Header("入力によって音を立たせる")]
    [Tooltip("入力値(移動)が決められた値を超えると音を立たせるようにする")]
    [SerializeField, Range(0, 1)]
    float _isSoundMoving;

    /// <summary>歩行速度</summary>
    float _walkSpeed;

    /// <summary>ゆっくり歩いているかどうか</summary>
    bool _isWalkSlow = false;

    /// <summary>MainCamera</summary>
    Transform _mcTra;

    /// <summary>方向転換時の回転速度</summary>
    float _rotateSpeed;

    /// <summary>次に向くべき方向の格納用</summary>
    Quaternion targetRotation;

    /// <summary>ロックオンするTarget</summary>
    Transform _lockonTarget;

    /// <summary>ロックオン時のAnimatorのLayerのWeight切り替え</summary>
    float _layerWeightValue = 0f;

    /// <summary>前フレームのロックオン状態</summary>
    bool _pastIsLockon = false;

    PlayerParameter _playerParameter;

    MakeASound _makeASound;

    CharacterController _characterController;

    Animator _anim;

    Transform _playerTra;

    PlayerInputAction _inputAction;

    CameraController _cameraController;

    public override void Init()
    {
        PlayerController playerController = _playerStateMachine.PlayerController;
        _walkSpeed = playerController.Parameter.WalkSpeed;
        _rotateSpeed = playerController.Parameter.RotateSpeed;
        _characterController = playerController.CharacterController;
        _inputAction = playerController.InputAction;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _cameraController = playerController.CameraController;
        _playerParameter = playerController.Parameter;
        _mcTra = Camera.main.transform;
    }

    public override void OnEnter()
    {
        _anim.SetFloat("move", 1);
        targetRotation = _playerTra.rotation;
        _layerWeightValue = 0;
        if (_inputAction.IsLockon)
            SetLayerWeightChanging();
    }

    public override void OnUpdate()
    {
        //移動
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        float walkSpeed = _isWalkSlow ? _walkSpeed / 3 : _walkSpeed;
        _characterController.Move(moveDir * walkSpeed * Time.deltaTime);

        //ゆっくり歩いているとき
        if (_inputAction.InputMove.magnitude >= _isSoundMoving)
        {
            _isWalkSlow = false;
            _anim.SetBool("IsWalkSlow",false);
            AudioManager.Instance.SEPlay(SE.PlayerFootsteps);
        }
        else
        {
            _isWalkSlow = true;
            _anim.SetBool("IsWalkSlow", true);
            AudioManager.Instance.SEStop();
        }


        _anim.SetBool("IsMoveForward", true);
        _anim.SetLayerWeight(2, 0);
        if (moveDir.magnitude > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }
        //移動する方向に向く
        _playerTra.rotation = Quaternion.RotateTowards(_playerTra.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        

        _anim.SetFloat("move", moveDir.magnitude);

        //遷移先
        if (_inputAction.IsAttack)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.ChargeAttack);

        if(_inputAction.IsEvade)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);

        if (moveDir.magnitude <= 0)     //突っ立ってる状態
        {
            _anim.SetLayerWeight(2, 0);
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
        }
    }

    public override void OnEnd()
    {
        _anim.SetBool("IsWalkSlow", false);
        _anim.SetFloat("move", 0);
        _isWalkSlow = false;
        _anim.SetLayerWeight(2, 0);
        AudioManager.Instance.SEStop();
        _characterController.Move(Vector3.zero);
    }

    public void SetLayerWeightChanging()
    {
        DOTween.To(() => _layerWeightValue, x => _layerWeightValue = x, 1f, 1);
    }
}
