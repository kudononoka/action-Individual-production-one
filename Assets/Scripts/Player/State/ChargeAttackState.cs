using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ChargeAttackState : PlayerStateBase
{
    [Header("攻撃できるまでの溜め時間")]
    [SerializeField] float _chargeTime = 3;

    [Header("攻撃時間")]
    [SerializeField] float _coolTime;

    [Header("溜めパーティクルを再生する時間")]
    [SerializeField] float _chargeParticleStartTime;

    [Header("溜め中のEffect")]
    [SerializeField] ParticleSystem _chargeParticle;

    [Header("溜め終わった時のEffect")]
    [SerializeField] ParticleSystem _chargeEndParticle;

    float _timer = 0f;

    Animator _playerAnim;

    PlayerInputAction _inputAction;

    PlayerParameter _playerParameter;

    Weapon _weapon;

    Transform _playerTra;

    CameraController _cameraController;

    Transform _mcTra;

    bool _isAttack = false;

    bool _isMadeSound = false;

    bool _isParticlePlay = false;
    public override void Init()
    {
        //Updateなどで使用するコンポーネントなどをここで保持しておく
        PlayerController playerController = _playerStateMachine.PlayerController;
        _playerAnim = playerController.PlayerAnim;
        _inputAction = playerController.InputAction;
        _playerParameter = playerController.Parameter;
        _weapon = playerController.PlayerWeapon;
        _playerTra = playerController.PlayerTra;
        _cameraController = playerController.CameraController;
        _mcTra = Camera.main.transform;
    }
    public override void OnEnter()
    {
        //アニメーション設定
        _playerAnim.SetInteger("AttackType", 1);
        _playerAnim.SetTrigger("Attack");

        //初期化
        _timer = 0f;

        //ダメージ設定
        _weapon.Damage = _playerParameter.AttackChargePower;

        _weapon.AttackState = PlayerStateMachine.StateType.ChargeAttack;

        //ロックオン中
        if (_inputAction.IsLockon)
        {
            //ロックオン対象の方を向く
            Vector3 targetPos = _cameraController.LockonTarget.position;
            targetPos.y = _playerTra.position.y;
            _playerTra.LookAt(targetPos);
        }
    }

    public override void OnUpdate()
    {
        //攻撃をする
        if (_isAttack)
        {
            _timer -= Time.deltaTime;

            //素振り音を一回鳴らす
            if (!_isMadeSound)
            {
                AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackStrongSwish);
                _isMadeSound = true;
            }

            //攻撃が終わったら
            if (_timer <= 0.1f)
            {
                //移動かIdleに遷移
                if (_inputAction.InputMove.magnitude <= 0)
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);

                else
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
            }

        }
        //攻撃前の溜め
        else
        {

            //ボタンを押している間
            if (_inputAction.IsAttack)
            {

                _timer += Time.deltaTime;

                //チャージできたら
                if (_timer >= _chargeTime)
                {
                    //チャージ完了のパーティクル再生
                    ParticleStop(_chargeParticle);
                    ParticlePlay(_chargeEndParticle);
                    //音再生
                    AudioManager.Instance.SEPlay(SE.PlayerChargeEnd);
                }

                //チャージ開始
                else if (_timer >= _chargeParticleStartTime)
                {
                    //パーティクル再生
                    ParticlePlay(_chargeParticle);
                    //音再生
                    AudioManager.Instance.SEPlay(SE.PlayerCharge);
                }

                //移動入力があったら
                if (!_inputAction.IsLockon && _inputAction.InputMove.magnitude > 0)
                {
                    //入力した方向を向く
                    var _forward = Quaternion.AngleAxis(_mcTra.eulerAngles.y, Vector3.up);
                    var moveDir = _forward * new Vector3(_inputAction.InputMove.x, 0, _inputAction.InputMove.y).normalized;
                    _playerTra.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
                }

            }

            //ボタンを離したら
            else
            {

                //チャージが完了していたら
                if (_timer >= _chargeTime)
                {
                    //パーティクル
                    ParticleStop(_chargeEndParticle);
                    //音
                    AudioManager.Instance.SEStop();
                    //アニメーション
                    _playerAnim.SetTrigger("Attack");
                    //攻撃開始
                    _isAttack = true;
                    //攻撃時間に設定
                    _timer = _coolTime;
                }

                //チャージが完了していなかったら
                else
                {
                    //パーティクル停止
                    ParticleStop(_chargeParticle);
                    //音停止
                    AudioManager.Instance.SEStop();
                    //通常攻撃へ遷移
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackComboOne);
                }
            }

        }

    }

    public void ParticlePlay(ParticleSystem particles)
    {
        if(particles.isPlaying)
            return;

        particles.Play();
    }

    public void ParticleStop(ParticleSystem particles)
    {
        particles.Stop();
    }
    public override void OnEnd()
    {
        //初期化
        _timer = 0;
        _isAttack = false;
        _isMadeSound = false;
        _playerAnim.SetInteger("AttackType", 0);
    }
}