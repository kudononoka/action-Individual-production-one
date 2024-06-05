using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
/// <summary>Traget�Ɍ������ē���Node</summary>
public class MoveToNode : BehaviorTreeBaseNode
{
    [Header("�ړ����x")]
    [SerializeField]
    float _moveSpeed;

    [Header("�ړ�����߂鎞��Target�Ƃ̋���")]
    [SerializeField]
    float _stopDistance;

    /// <summary>�����������I�u�W�F�N�g</summary>
    NavMeshAgent _agent = null;
    /// <summary>�ړI�n</summary>
    Transform _target = null;
    /// <summary>�����������I�u�W�F�N�g�̈ʒu</summary>
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
        _agent.SetDestination(_target.position);�@//Target�܂ňړ�

        _anim.SetBool("IsWalk", true);    //�A�j���[�V�����ݒ�
        if(Vector3.Distance(_target.position, _my.position) <= _stopDistance) //�ړI�n�ɂ�����
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
