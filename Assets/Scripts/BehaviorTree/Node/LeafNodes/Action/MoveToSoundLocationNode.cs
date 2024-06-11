using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>�������������ꏊ�Ɍ�������p�m�[�h</summary>
[SerializeField]
public class MoveToSoundLocationNode : BehaviorTreeBaseNode
{
    [Header("�ړ����x")]
    [SerializeField]
    float _moveSpeed;

    [Header("�ړ�����߂鎞�̖ړI�n�Ƃ̋���")]
    [SerializeField]
    float _stopDistance;

    /// <summary>�����������I�u�W�F�N�g</summary>
    NavMeshAgent _agent = null;

    /// <summary>���������ꏊ</summary>
    Vector3 _soundLocation = Vector3.zero;

    /// <summary>�����������I�u�W�F�N�g�̈ʒu</summary>
    Transform _my = null;

    Animator _anim;

    AudibilityController _audibilityController;

    Transform _target;

    SightController _sightController;

    public MoveToSoundLocationNode()
    {
        nodeName = "move to sound location";
        nodeData = new NodeData(NodeType.ActionNode, typeof(MoveToSoundLocationNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _audibilityController = my.GetComponent<AudibilityController>();
        _my = my.GetComponent<Transform>();
        _agent = my.GetComponent<NavMeshAgent>();
        _anim = my.GetComponent<Animator>();
        _sightController = my.GetComponent<SightController>();
        _target = target.GetComponent<Transform>();
    }
    public override Result Evaluate()
    {
        if (_soundLocation == Vector3.zero)
        {
            _soundLocation = _audibilityController.SoundLocation;
        }
        _agent.speed = _moveSpeed;

        _agent.SetDestination(_soundLocation);�@//���������ꏊ�܂ňړ�

        _anim.SetBool("IsWalk", true);    //�A�j���[�V�����ݒ�

        //���������ꏊ�ɒǂ������琬����Ԃ�
        if (Vector3.Distance(_soundLocation, _my.position) <= _stopDistance)
        {
            _soundLocation = Vector3.zero;
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        //�U���Ώۂ����E�ɓ������玸�s��Ԃ�,�������������ꏊ�ɍs���̂𒆒f����
        if (_sightController.isVisible(_target.position))
        {
            _soundLocation = Vector3.zero;
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Failure;
        }
        
        return Result.Runnimg;
    }
}
