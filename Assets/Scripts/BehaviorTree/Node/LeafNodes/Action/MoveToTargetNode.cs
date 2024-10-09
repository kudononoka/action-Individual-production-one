using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
/// <summary>攻撃対象に向かって動くNode</summary>
public class MoveToTargetNode : BehaviorTreeBaseNode
{
    [Header("移動速度")]
    [SerializeField]
    float _moveSpeed;

    [Header("移動をやめる時のTargetとの距離")]
    [Tooltip("Targetと近すぎにならないようにする用")]
    [SerializeField]
    float _stopDistanceMin;

    //[Header("移動をやめる時のTargetとの距離")]
    //[Tooltip("Targetから離れすぎると尾行をやめる")]
    //[SerializeField]
    //float _stopDistanceMax;

    /// <summary>動かしたいオブジェクト</summary>
    NavMeshAgent _agent = null;
    /// <summary>目的地</summary>
    Transform _target = null;
    /// <summary>動かしたいオブジェクトの位置</summary>
    Transform _my = null;

    Animator _anim;
    public MoveToTargetNode()
    {
        nodeName = "move to target";
        nodeData = new NodeData(NodeType.ActionNode, typeof(MoveToTargetNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
        _agent = my.GetComponent<NavMeshAgent>();
        _anim = my.GetComponent<Animator>();
    }

    public override Result Evaluate()
    {
        _agent.speed = _moveSpeed;

        _agent.isStopped = false;

        _agent.SetDestination(_target.position);　//Targetまで移動

        _anim.SetBool("IsWalk", true);

        //Targeに追いついたら成功を返す
        if (Vector3.Distance(_target.position, _my.position) <= _stopDistanceMin)  
        {
            _agent.isStopped = true;
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        ////Targetとの距離がはなれてしまったら失敗を返す
        //else if (Vector3.Distance(_target.position, _my.position) >= _stopDistanceMax) 
        //{
        //    _agent.isStopped = true;
        //    _anim.SetBool("IsWalk", false);
        //    return Result.Failure;
        //}

        return Result.Runnimg;
    }
}
