using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>Traget�Ɍ������ē���Node</summary>
public class MoveToNode : BehaviorTreeBaseNode
{
    /// <summary>�����������I�u�W�F�N�g</summary>
    NavMeshAgent _agent = null;
    /// <summary>�ړI�n</summary>
    Transform _target = null;
    /// <summary>�����������I�u�W�F�N�g�̈ʒu</summary>
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
        _agent.SetDestination(_target.position);�@//Target�܂ňړ�

        if(Vector3.Distance(_target.position, _my.position) <= _agent.stoppingDistance) //�ړI�n�ɂ�����
        {
            _agent.SetDestination(_my.position);�@
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
