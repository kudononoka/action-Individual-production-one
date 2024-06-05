using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
/// <summary>Tragetに向かって動くNode</summary>
public class MoveToNode : BehaviorTreeBaseNode
{
    [Header("移動速度")]
    [SerializeField]
    float _moveSpeed;

    [Header("移動をやめる時のTargetとの距離")]
    [SerializeField]
    float _stopDistance;

    /// <summary>動かしたいオブジェクト</summary>
    NavMeshAgent _agent = null;
    /// <summary>目的地</summary>
    Transform _target = null;
    /// <summary>動かしたいオブジェクトの位置</summary>
    Transform _my = null;

    Animator _anim; 
    public MoveToNode()
    {
        nodeName = "move to";
        nodeData = new NodeData(NodeType.ActionNode, typeof(MoveToNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
        _agent = my.GetComponent<NavMeshAgent>();
        _anim = my.GetComponent<EnemyAI>().EnemyAnimator;
        _agent.speed = _moveSpeed;
    }

    public override Result Evaluate()
    {
        _agent.SetDestination(_target.position);　//Targetまで移動

        _anim.SetBool("IsWalk", true);    //アニメーション設定
        if(Vector3.Distance(_target.position, _my.position) <= _stopDistance) //目的地についたら
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
