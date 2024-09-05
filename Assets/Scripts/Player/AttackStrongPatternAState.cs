using System;
using UnityEngine;

[Serializable]
public class AttackStrongPatternAState : PlayerStateBase
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

    /// <summary>素振り音を鳴らしたかどうか</summary>
    bool _isSound = false;

    float _coolTimer;

    Animator _anim;

    PlayerInputAction _inputAction;

    Transform _playerTra;

    PlayerHPSTController _playerHPSTController;

    PlayerParameter _playerParameter;

    Weapon _weapon;

    MakeASound _makeASound;

    public override void Init()
    {
        PlayerController　playerController = _playerStateMachine.PlayerController;
        _anim = playerController.PlayerAnim;
        _playerTra = playerController.PlayerTra;
        _inputAction = playerController.InputAction;
        _playerHPSTController = playerController.PlayerHPSTController;
        _playerParameter = playerController.Parameter;
        _weapon = playerController.PlayerWeapon;
        _makeASound = playerController.MakeASound;
    }
    public override void OnEnter()
    {
        _playerHPSTController.STDown(_playerParameter.AttackStrongSTCost);

        _coolTimer = _coolTime;

        //アニメーション設定
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 1);

    　 //ダメージ設定
        _weapon.Damage = _playerParameter.AttackStrongPower;

        //音を立てる
        _makeASound.IsSoundChange(true);
        _isSound = false;

        //入力値初期化
        _inputAction.IsAttackStrong = false;
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;

        //次の攻撃をするかどうか
        if(_coolTimer < _nextAttackJudgeTime)
        {
            //次の攻撃に遷移可能
            if (_coolTimer < _nextAttackTime)
            {
                if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
                {
                    //強攻撃に遷移
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternB);
                }
            }
        }
        else
        {
            _inputAction.IsAttackStrong = false;
        }

        //素振りの音を鳴らす
        if(!_isSound && _coolTimer <= _soundTime)
        {
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackStrongSwish);
            _isSound = true;
        }

        if (_coolTimer <= 0.95)
        {
            //移動かIdleに遷移
            if (_inputAction.InputMove.magnitude <= 0)
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);
            else
                _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
        }
    }
    public override void OnEnd()
    {
        _inputAction.IsAttackStrong = false;
        _makeASound.IsSoundChange(false);
    }
}
