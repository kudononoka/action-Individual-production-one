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

    [Header("ジャスト回避後のスロー秒数")]
    [SerializeField]
    float _slowTime;

    float _slowTimer;

    Animator _anim;

    Transform _playerTra;

    PlayerInputAction _inputAction;

    Transform _lockonTarget;

    Transform _mcTra;

    CharacterController _characterController;

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    CapsuleCollider _capsuleCollider;

    MakeASound _makeASound;

    CameraController _cameraController;

    TimeManager _timeManager;

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
        _timeManager = playerController.TimeManager;
    }
    public override void OnEnter()
    {
        //現在のポジションを記憶
        _pos = _playerTra.position;

        //回避方向
        var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
        _moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;

        if(_moveDir == Vector3.zero)
        {
            _moveDir = _playerTra.forward;
        }

        //アニメーション設定
        _anim.SetTrigger("Evade");

        //当たり判定なし
        _capsuleCollider.enabled = false;

        //回避パーティクル再生
        _particle.Play();

        //入力取り消し
        _inputAction.IsEvade = false;

        //ジャスト回避判定
        JustAvoidanceJudgment justAvoidanceJudgment = new JustAvoidanceJudgment();

        bool isJust = false;
        foreach (var target in _cameraController.LockonRange.EnemiesInRange)
        {
            isJust = justAvoidanceJudgment.OnJudge(target.gameObject);
        }
        //ジャスト回避できていたら
        if (isJust)
        {
            //スロー
            _timeManager.SlowSystem.OnOffSlow(true);
        }

        //音再生
        AudioManager.Instance.SEPlayOneShot(SE.PlayerStep);

        //スロー時間初期化
        _slowTimer = _slowTime;
    }

    public override void OnUpdate()
    {
        _slowTimer -= Time.deltaTime;
       
        //移動
        _characterController.Move(_moveDir * _moveSpeed * Time.deltaTime);
        //移動方向を向く
        _playerTra.rotation = Quaternion.LookRotation(_moveDir, Vector3.up);
        
        //一定の距離を移動できたら
        if (Vector3.Distance(_pos, _playerTra.position) >= _evadeDistance)
        {
            //条件によって遷移
            //攻撃
            if (_inputAction.IsAttack)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackComboOne);
            //移動
            else if (_inputAction.InputMove.magnitude > 0.1f)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
            //直立
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
        }
        
        //スロー解除
        if(_slowTimer >= _slowTime && _timeManager.SlowSystem.IsSlowing)
        {
            _timeManager.SlowSystem.OnOffSlow(false);
        }
    }

    public override void OnEnd()
    {
        _inputAction.IsEvade = false;
        _capsuleCollider.enabled = true;
    }
}
