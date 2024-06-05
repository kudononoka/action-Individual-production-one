using UnityEngine;

/// <summary>�������猩��Taget������ɂ��邩�ǂ���</summary>
public class TargetIsBehind : BehaviorTreeBaseNode
{
    Transform _targetTra;
    Transform _myTra;

    public�@TargetIsBehind()
    {
        nodeName = "target is behind";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(TargetIsBehind).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _targetTra = target.GetComponent<Transform>();
        _myTra = my.GetComponent<Transform>();
    }

    public override Result Evaluate()
    {
        Vector3 vec = (_targetTra.position - _myTra.position).normalized;�@//��������Target�֌������x�N�g�����擾
        float angle = Vector3.Angle(_myTra.forward, vec);�@//�����̐��ʃx�N�g���Ƃ̊p�x�����߂�

        if (angle >=  90)�@                      //�����̌����Target������
        {
            return Result.Success;
        }

        return Result.Failure;
    }
}
