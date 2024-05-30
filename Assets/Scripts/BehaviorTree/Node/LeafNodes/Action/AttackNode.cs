using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>çUåÇNode</summary>
public class AttackNode : BehaviorTreeBaseNode
{
    public AttackNode()
    {
        nodeName = "attack";
        nodeData = new NodeData(NodeType.ActionNode, typeof(AttackNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
    }
    public override Result Evaluate()
    {
        Debug.Log("Attack");
        return Result.Success;
    }

}
