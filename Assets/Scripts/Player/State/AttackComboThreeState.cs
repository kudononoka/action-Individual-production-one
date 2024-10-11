using System;
using UnityEngine;

[Serializable]
public class AttackComboThreeState : PlayerStateBase
{
    [Header("強攻撃にかかる時間")]
    [SerializeField]
    float _coolTime;

    [Header("次の攻撃をするかが確定される時間")]
    [SerializeField]
    float _nextAttackJudgeTime;

    [Header("次の攻撃につながるまでの時間")]
    [SerializeField]
    float _nextAttackTime;

    [Header("素振り音を鳴らすタイミング")]
    [SerializeField]
    float _soundTime;

    [Header("移動開始時間")]
    [SerializeField]
    float _moveStartTime;

    [Header("移動速度")]
    [SerializeField]
    float _moveSpeed;

    [Header("移動距離差分")]
    [SerializeField]
    float _movingDifference;

    /// <summary>素振り音を鳴らしたかどうか</summary>
    bool _isMadeSound = false;

    float _coolTimer;

    Animator _anim;

    PlayerInputAction _inputAction;

    Transform _playerTra;

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    Weapon _weapon;

    CharacterController _characterController;

    CameraController _cameraController;

    Transform _mcTra;

    /// <summary>攻撃中移動する前のPlayerのPosition</summary>
    Vector3 _beforeMovingPos;

    public override void Init()
    {
        //Updateなどで使用するコンポーネントなどをここで保持しておく
        PlayerController playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        _weapon = playerController.PlayerWeapon;
        _characterController = playerController.CharacterController;
        _cameraController = playerController.CameraController;
        _mcTra = Camera.main.transform;
    }
    public override void OnEnter()
    {
        _playerHPSTController.STDown(_playerParameter.AttackStrongSTCost);

        _coolTimer = _coolTime;

        _anim.SetTrigger("Attack");

        //ダメージ設定
        _weapon.Damage = _playerParameter.AttackStrongPower;

        _isMadeSound = false;

        //入力値初期化
        _inputAction.IsAttack = false;

        //現在のPlayerの位置を記憶しておく
        _beforeMovingPos = _playerTra.position;

        //ロックオン中
        if (_inputAction.IsLockon)
        {
            //ロックオン対象の方を向く
            Vector3 targetPos = _cameraController.LockonTarget.position;
            targetPos.y = _playerTra.position.y;
            _playerTra.LookAt(targetPos);
        }
        //通常時
        else
        {
            //移動入力があったら
            if (_inputAction.InputMove.magnitude > 0)
            {
                //入力した方向を向く
                var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
                var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
                _playerTra.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            }
        }
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;

        //攻撃の入力を受け付ける時間になったら
        if(_coolTimer < _nextAttackJudgeTime)
        {
            //次の攻撃に遷移可能な時間になったら
            if (_coolTimer < _nextAttackTime)
            {
                //攻撃の入力をされていたら
                if (_inputAction.IsAttack && _anim.GetBool("NextAttack"))
                {
                    
                    //強攻撃に遷移
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackComboFour);
                }
            }
        }
        //それ以外の時間は
        else
        {
            //入力されても取り消しにする
            _inputAction.IsAttack　= false;
        }

        //アニメーションと合わせて素振り音を鳴らす
        if (!_isMadeSound && _coolTimer <= _soundTime)
        {
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackStrongSwish);
            _isMadeSound = true;
        }

        if (_coolTimer <= 0.2)
        {
            _anim.SetBool("NextAttack", false);
            //移動かIdleに遷移
            if (_inputAction.InputMove.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        //移動する時間になったら
        if (_coolTimer <= _moveStartTime)
        {

            //攻撃始めの位置から一定の距離離れたら
            if (Vector3.Distance(_beforeMovingPos, _playerTra.position) >= _movingDifference)
            {
                //移動停止
                _characterController.Move(Vector3.zero);
            }
            else
            {
                //移動
                _characterController.Move(_playerTra.forward * _moveSpeed);
            }

        }
    }
    public override void OnEnd()
    {
        //入力を取り消し
        _inputAction.IsAttack = false;
        //移動停止
        _characterController.Move(Vector3.zero);
    }
}
