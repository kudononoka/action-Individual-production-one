using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�A�j���[�V�����̋t�Đ���ꎞ��~���\�ɂ������</summary>
public class AnimMovingSpeedNode : BehaviorTreeBaseNode
{
    [Header("Animation�̍Đ��X�s�[�h")]
    [SerializeField, Range(-1, 1)]
    float _animSpeed;

    Animator _anim;
    public AnimMovingSpeedNode()
    {
        nodeName = "animation moving speed";
        nodeData = new NodeData(NodeType.ActionNode, typeof(AnimMovingSpeedNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        EnemyAI enemyAI = my.GetComponent<EnemyAI>();
        _anim = enemyAI.EnemyAnimator;
    }

    public override Result Evaluate()
    {
        _anim.SetFloat("MovingSpeed", _animSpeed);
        return Result.Success;
    }
}
