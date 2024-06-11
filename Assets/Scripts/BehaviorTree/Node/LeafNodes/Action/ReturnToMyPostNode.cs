using UnityEngine;
using UnityEngine.AI;

/// <summary>�����̎�����ɖ߂�Node</summary>
public class ReturnToMyPostNode : BehaviorTreeBaseNode
{
    [Header("�ړ����x")]
    [SerializeField]
    float _moveSpeed;

    /// <summary>�����������I�u�W�F�N�g</summary>
    NavMeshAgent _agent = null;
    /// <summary>�����̎�����</summary>
    Vector3 _point = Vector3.zero;
    /// <summary>�����������I�u�W�F�N�g�̈ʒu</summary>
    Transform _my = null;

    Animator _anim;

    GameObject _target;

    AudibilityController _audibilityController;


    SightController _sightController;

    public ReturnToMyPostNode()
    {
        nodeName = "return to my post";
        nodeData = new NodeData(NodeType.ActionNode, typeof(ReturnToMyPostNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        //�e�I�u�W�F�N�g�Ɏ������ݒ肵�Ă��鎖���O��
        _point = my.transform.parent.position;
        _my = my.GetComponent<Transform>();
        _agent = my.GetComponent<NavMeshAgent>();
        _anim = my.GetComponent<Animator>();
        _agent.speed = _moveSpeed;
        _audibilityController = my.GetComponent<AudibilityController>();
        _sightController = my.GetComponent<SightController>();
        _target = target;
    }

    public override Result Evaluate()
    {
        _agent.speed = _moveSpeed;

        _agent.SetDestination(_point);�@//Target�܂ňړ�

        _anim.SetBool("IsWalk", true);    //�A�j���[�V�����ݒ�

        //�ړI�n�ɂ����琬����Ԃ�
        if (Vector3.Distance(_point, _my.position) <= 1)
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        //��������������߂�̂𒆒f
        if (_audibilityController.IsAudible(_target))
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Failure;
        }

        //�U���Ώۂ���������߂�̂𒆒f
        if (_sightController.isVisible(_target.transform.position))
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Failure;
        }

        return Result.Runnimg;
    }

}
