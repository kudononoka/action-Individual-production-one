using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

[Serializable]
/// <summary>攻撃Node</summary>
public class AttackNode : BehaviorTreeBaseNode
{
    float _coolTimer;

    [Header("これから攻撃する攻撃の種類")]
    [SerializeField]
    AttackType _attackType;

    [Header("攻撃にかかる時間")]
    [SerializeField]
    float _coolTime;

    Animator _anim;

    Weapon _weapon;

    EnemyParameter _enemyParameter;

    public AttackNode()
    {
        nodeName = "attack";
        nodeData = new NodeData(NodeType.ActionNode, typeof(AttackNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        EnemyAI enemyAI = my.GetComponent<EnemyAI>();
        _anim = enemyAI.EnemyAnimator;
        _weapon = enemyAI.Weapon;
        _enemyParameter = enemyAI.Parameter;
        _coolTimer = _coolTime;
    }

    public override Result Evaluate()
    {
        if(_coolTimer == _coolTime)　//Nodeに入った瞬間
        {
            _anim.SetInteger("AttackPattern", (int)_attackType);     //アニメーション設定
            _anim.SetTrigger("Attack");

            switch(_attackType)                  //与えるダメージを設定
            {
                case AttackType.HorizontalSwing:
                    _weapon.Damage = _enemyParameter.AttackHorizontalSwingPower;
                    break; 
                case AttackType.VerticalSwing:
                    _weapon.Damage = _enemyParameter.AttackVerticalSwingPower;
                    break;
                case AttackType.Special:
                    _weapon.Damage = _enemyParameter.AttackSpecialPower;
                    break;
                case AttackType.StepAttack:
                    _weapon.Damage = _enemyParameter.AttckStepPower;
                    break;
            }

            _weapon.DamageColliderEnabledSet(true);　　//武器の当たり判定ON
        }

        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0) //待ち時間経過
        {
            _coolTimer = _coolTime; 　　　　　　　//初期化
            _weapon.DamageColliderEnabledSet(false);　　//武器の当たり判定OFF
            return Result.Success;
        }

        return Result.Runnimg;
    }
}

/// <summary>攻撃タイプ</summary>
public enum AttackType
{
    /// <summary>横振り</summary>
    HorizontalSwing,
    /// <summary>縦振り</summary>
    VerticalSwing,
    /// <summary>攻撃タイプ</summary>
    Special,
    /// <summary>ステップからの攻撃</summary>
    StepAttack
}
