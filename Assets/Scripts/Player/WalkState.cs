using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class WalkState : PlayerStateBase
{
    [Header("���͂ɂ���ĉ��𗧂�����")]
    [Tooltip("���͒l(�ړ�)�����߂�ꂽ�l�𒴂���Ɖ��𗧂�����悤�ɂ���")]
    [SerializeField, Range(0, 1)]
    float _isSoundMoving;

    /// <summary>���s���x</summary>
    float _walkSpeed;

    /// <summary>�����������Ă��邩�ǂ���</summary>
    bool _isWalkSlow = false;

    /// <summary>MainCamera</summary>
    Transform _mcTra;

    /// <summary>�����]�����̉�]���x</summary>
    float _rotateSpeed;

    /// <summary>���Ɍ����ׂ������̊i�[�p</summary>
    Quaternion targetRotation;

    /// <summary>���b�N�I������Target</summary>
    Transform _lockonTarget;

    /// <summary>���b�N�I�����ǂ̕���(�O�㍶�E)�ɓ����Ă��邩</summary>
    DirMovement _dirMovement = new();

    /// <summary>���b�N�I������Animator��Layer��Weight�؂�ւ�</summary>
    float _layerWeightValue = 0f;

    /// <summary>�O�t���[���̃��b�N�I�����</summary>
    bool _pastIsLockon = false;

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    MakeASound _makeASound;

    CharacterController _characterController;

    Animator _anim;

    Transform _playerTra;

    PlayerInputAction _inputAction;

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
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        _makeASound = playerController.MakeASound;
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
        //�ړ�
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
        float walkSpeed = _isWalkSlow ? _walkSpeed / 3 : _walkSpeed;
        _characterController.Move(moveDir * walkSpeed * Time.deltaTime);

        //�����������Ă���Ƃ�
        if (_inputAction.InputMove.magnitude >= _isSoundMoving)
        {
            _isWalkSlow = false;
            _anim.SetBool("IsWalkSlow",false);
            _makeASound.IsSoundChange(true);         //���𗧂Ă�
        }
        else
        {
            _isWalkSlow = true;
            _anim.SetBool("IsWalkSlow", true);
            _makeASound.IsSoundChange(false);       //���𗧂ĂȂ�
        }

        //���b�N�I���؂�ւ����͂��ꂽ�������������s��
        if (_inputAction.IsLockon != _pastIsLockon)     
        {
            _pastIsLockon = _inputAction.IsLockon;
            if(_inputAction.IsLockon)
            {
                SetLayerWeightChanging();
            }
        }

        //���b�N�I����
        if (_inputAction.IsLockon)
        {
            //���������ɂ����Animation��؂�ւ�
            DirMovement.MoveDir dir = _dirMovement.DirMovementJudge(_inputAction.InputMove);
            switch(dir)
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
                    _anim.SetLayerWeight(2, _layerWeightValue);
                    break;
                case DirMovement.MoveDir.Right:
                    _anim.SetFloat("AnimationSpeed", -1);
                    _anim.SetLayerWeight(2, _layerWeightValue);
                    break;
                default:
                    break;
            }

            //�^�[�Q�b�g�̕�������
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
            //�ړ���������Ɍ���
            _playerTra.rotation = Quaternion.RotateTowards(_playerTra.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        _anim.SetFloat("move", moveDir.magnitude);

        //�J�ڐ�
        if (_inputAction.IsAttackWeak && _playerHPSTController.CurrntStValue >= _playerParameter.AttackWeakSTCost)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);

        if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);

        if (_inputAction.IsGuard && _playerHPSTController.CurrntStValue >= _playerParameter.GuardHitSTCost)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Guard);

        if(_inputAction.IsEvade && _playerHPSTController.CurrntStValue >= _playerParameter.EvadeSTCost)
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Evade);

        if (moveDir.magnitude <= 0)     //�˂������Ă���
        {
            _anim.SetLayerWeight(2, 0);
            _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
        }
    }

    public override void OnEnd()
    {
        _anim.SetBool("IsWalkSlow", false);
        _isWalkSlow = false;
        _anim.SetLayerWeight(2, 0);
    }

    public void SetLayerWeightChanging()
    {
        DOTween.To(() => _layerWeightValue, x => _layerWeightValue = x, 1f, 1);
    }
}
