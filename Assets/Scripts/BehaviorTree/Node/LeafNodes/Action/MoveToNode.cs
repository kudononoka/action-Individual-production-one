using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>Tragetに向かって動くNode</summary>
public class MoveToNode : BehaviorTreeBaseNode
{
    /// <summary>動かしたいオブジェクト</summary>
    NavMeshAgent _agent = null;
    /// <summary>目的地</summary>
    Transform _target = null;
    /// <summary>動かしたいオブジェクトの位置</summary>
    Transform _my = null;

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
    }

    public override Result Evaluate()
    {
        _agent.SetDestination(_target.position);　//Targetまで移動

        if(Vector3.Distance(_target.position, _my.position) <= _agent.stoppingDistance) //目的地についたら
        {
            _agent.SetDestination(_my.position);　
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
