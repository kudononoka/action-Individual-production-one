using System;
using UnityEngine;

[Serializable]
public class AttackWeakPatternBState : PlayerStateBase
{
    [Header("弱攻撃にかかる時間")]
    [SerializeField]
    float _coolTime;

    [Header("次の攻撃をするかが確定される時間")]
    [SerializeField]
    float _nextAttackJudgeTime;

    [Header("次の攻撃につながるまでの時間")]
    [SerializeField]
    float _nextAttackTime;

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
        PlayerController playerController = _playerStateMachine.PlayerController;
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
        _playerHPSTController.STDown(_playerParameter.AttackWeakSTCost);

        //初期化
        _coolTimer = _coolTime;

        //アニメーション設定
        _anim.SetTrigger("Attack");
        _anim.SetInteger("AttackType", 0);

        //ダメージ設定
        _weapon.Damage = _playerParameter.AttackWeakPower;
        _makeASound.IsSoundChange(true);

        //素振り音
        AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackWeakSwish);
        _inputAction.IsAttackWeak = false;
    }

    public override void OnUpdate()
    {
        _coolTimer -= Time.deltaTime;
        //次の攻撃をするかどうかがきまる
        if(_coolTimer < _nextAttackJudgeTime)
        {
            //次の攻撃遷移可能
            if (_coolTimer < _nextAttackTime)
            {
                //弱攻撃
                if (_inputAction.IsAttackWeak && _playerHPSTController.CurrntStValue >= _playerParameter.AttackWeakSTCost)
                {
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackWeakPatternA);
                }
                //強攻撃
                else if (_inputAction.IsAttackStrong && _playerHPSTController.CurrntStValue >= _playerParameter.AttackStrongSTCost)
                {
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackStrongPatternA);
                }
            }
        }
        else
        {
            _inputAction.IsAttackWeak = false;
        }

        if (_coolTimer <= 0.2)
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
        _inputAction.IsAttackWeak = false;
        _makeASound.IsSoundChange(false);
    }
}
