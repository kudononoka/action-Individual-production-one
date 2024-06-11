using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
/// <summary>�U���ΏۂɌ������ē���Node</summary>
public class MoveToTargetNode : BehaviorTreeBaseNode
{
    [Header("�ړ����x")]
    [SerializeField]
    float _moveSpeed;

    [Header("�ړ�����߂鎞��Target�Ƃ̋���")]
    [Tooltip("Target�Ƌ߂����ɂȂ�Ȃ��悤�ɂ���p")]
    [SerializeField]
    float _stopDistanceMin;

    [Header("�ړ�����߂鎞��Target�Ƃ̋���")]
    [Tooltip("Target���痣�ꂷ����Ɣ��s����߂�")]
    [SerializeField]
    float _stopDistanceMax;

    /// <summary>�����������I�u�W�F�N�g</summary>
    NavMeshAgent _agent = null;
    /// <summary>�ړI�n</summary>
    Transform _target = null;
    /// <summary>�����������I�u�W�F�N�g�̈ʒu</summary>
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
        _agent.speed = _moveSpeed;
    }

    public override Result Evaluate()
    {
        _agent.speed = _moveSpeed;

        _agent.SetDestination(_target.position);�@//Target�܂ňړ�

        _anim.SetBool("IsRun", true);    //�A�j���[�V�����ݒ�

        //Targe�ɒǂ������琬����Ԃ�
        if (Vector3.Distance(_target.position, _my.position) <= _stopDistanceMin)  
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsRun", false);
            return Result.Success;
        }

        //Target�Ƃ̋������͂Ȃ�Ă��܂����玸�s��Ԃ�
        else if (Vector3.Distance(_target.position, _my.position) >= _stopDistanceMax) 
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsRun", false);
            return Result.Failure;
        }

        return Result.Runnimg;
    }
}
