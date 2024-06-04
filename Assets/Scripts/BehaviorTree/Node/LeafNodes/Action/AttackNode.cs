using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

[Serializable]
/// <summary>�U��Node</summary>
public class AttackNode : BehaviorTreeBaseNode
{
    float _coolTimer;

    [Header("���ꂩ��U������U���̎��")]
    [SerializeField]
    AttackType _attackType;

    [Header("�U���ɂ����鎞��")]
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
        if(_coolTimer == _coolTime)�@//Node�ɓ������u��
        {
            _anim.SetInteger("AttackPattern", (int)_attackType);     //�A�j���[�V�����ݒ�
            _anim.SetTrigger("Attack");

            switch(_attackType)                  //�^����_���[�W��ݒ�
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

            _weapon.DamageColliderEnabledSet(true);�@�@//����̓����蔻��ON
        }

        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0) //�҂����Ԍo��
        {
            _coolTimer = _coolTime; �@�@�@�@�@�@�@//������
            _weapon.DamageColliderEnabledSet(false);�@�@//����̓����蔻��OFF
            return Result.Success;
        }

        return Result.Runnimg;
    }
}

/// <summary>�U���^�C�v</summary>
public enum AttackType
{
    /// <summary>���U��</summary>
    HorizontalSwing,
    /// <summary>�c�U��</summary>
    VerticalSwing,
    /// <summary>�U���^�C�v</summary>
    Special,
    /// <summary>�X�e�b�v����̍U��</summary>
    StepAttack
}
