using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>アニメーションの逆再生や一時停止を可能にするもの</summary>
public class AnimMovingSpeedNode : BehaviorTreeBaseNode
{
    [Header("Animationの再生スピード")]
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
