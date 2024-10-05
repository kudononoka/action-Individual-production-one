using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangePositionNode : BehaviorTreeBaseNode
{
    [Header("Player�Ƃ̋���")]
    [SerializeField] float _distance = 3f;

    Transform _my;

    Transform _target;

    public ChangePositionNode()
    {
        nodeName = "change pos";
        nodeData = new NodeData(NodeType.ActionNode, typeof(ChangePositionNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
    }

    public override Result Evaluate()
    {
        //Target�Ǝ����̑Ίp���ƂȂ�x�N�g�������߂�
        var vec = _my.position - _target.position;
        //���߂��x�N�g����@���@Target���猈�߂�ꂽ�����ł���ʒu�����߂�
        Vector3 pos = (vec.normalized * _distance) + _target.position;
        //�ʒu�ύX
        _my.position = pos;

        return Result.Success;
    }
}
