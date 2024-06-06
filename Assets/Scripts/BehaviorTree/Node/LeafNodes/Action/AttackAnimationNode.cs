using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationNode : BehaviorTreeBaseNode
{
    [Header("���ꂩ��U������U���̎��")]
    [SerializeField]
    AttackState _attackState;

    Animator _anim;

    public AttackAnimationNode()
    {
        nodeName = "attack animation";
        nodeData = new NodeData(NodeType.ActionNode, typeof(AttackAnimationNode).FullName);
    }
    public override void Init(GameObject target, GameObject my)
    {
        EnemyAI enemyAI = my.GetComponent<EnemyAI>();
        _anim = enemyAI.EnemyAnimator;
    }
    public override Result Evaluate()
    {
        _anim.SetInteger("AttackPattern", (int)_attackState);     //�A�j���[�V�����ݒ�
        _anim.SetTrigger("Attack");

        return Result.Success;
    }
}

public enum AttackState
{
    SwingDown,
    ThreeSwingDown,
    SwingAround,
    SwingDownAndSwingAround,
    SwingAroundAndSwingDown,
    FourSwingAround,
    JumpToSwingDown,
}