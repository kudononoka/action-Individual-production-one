using System;
using UnityEngine;

[Serializable]
public class AttackStrongPatternBState : PlayerStateBase
{
    [Header("強攻撃にかかる時間")]
    [SerializeField]
    float _coolTime;

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

    Weapon _weapon;

    PlayerParameter _playerParameter;

    CharacterController _characterController;

    /// <summary>攻撃中移動する前のPlayerのPosition</summary>
    Vector3 _beforeMovingPos;

    public override void Init()
    {
        //Updateなどで使用するコンポーネントなどをここで保持しておく
        PlayerController playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _weapon = playerController.PlayerWeapon;
        _playerParameter = playerController.Parameter;
        _characterController = playerController.CharacterController;
    }
    public override void OnEnter()
    {
        //初期化
        _coolTimer = _coolTime;

        //アニメーション設定
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 1);

        //ダメージ設定
        _weapon.Damage = _playerParameter.AttackStrongPower;

        //最初は音を立てない
        _isMadeSound = false;

        //入力をなかったことに
        _inputAction.IsAttackStrong = false;

        //現在のPlayerの位置を記憶しておく
        _beforeMovingPos = _playerTra.position;
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0)
        {
            //移動かIdleに遷移
            if (_inputAction.InputMove.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }

        //アニメーションと合わせて素振り音を鳴らす
        if (!_isMadeSound && _coolTimer <= _soundTime)
        {
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackStrongSwish);
            _isMadeSound = true;
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
        _inputAction.IsAttackStrong = false;
        //移動停止
        _characterController.Move(Vector3.zero);
    }
}
