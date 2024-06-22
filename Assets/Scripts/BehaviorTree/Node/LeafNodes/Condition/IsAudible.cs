using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>音が聞き取ったかどうか判定</summary>
public class IsAudible : BehaviorTreeBaseNode
{
    GameObject _target;

    AudibilityController _audibilityController;

    public IsAudible()
    {
        nodeName = "is audible";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(IsAudible).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _audibilityController = my.GetComponent<AudibilityController>();
        _target = target;   
    }

    public override Result Evaluate()
    {
        if(_audibilityController.IsAudible(_target))
        {
            return Result.Success;
        }

        return Result.Failure;
    }
}
