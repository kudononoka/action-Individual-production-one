using UnityEngine;

/// <summary>ターゲットの方を向く</summary>
public class LookAt : BehaviorTreeBaseNode
{
    Transform _target;
    Transform _my;
    public LookAt()
    {
        nodeName = "look at";
        nodeData = new NodeData(NodeType.ActionNode, typeof(LookAt).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
    }

    public override Result Evaluate()
    {
        _my.LookAt(_target, Vector3.up);　
        return Result.Success;
    }
}
