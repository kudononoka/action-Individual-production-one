﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>対象が見えているかどうか</summary>
public class IsVisible : BehaviorTreeBaseNode
{
    Transform _target;

    SightController _sightController;

    public IsVisible()
    {
        nodeName = "is visible";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(IsVisible).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _sightController = my.GetComponent<SightController>();
        _target = target.GetComponent<Transform>();
    }

    public override Result Evaluate()
    {
        if (_sightController.isVisible(_target.position))
        {
            return Result.Success;
        }
        return Result.Failure;
    }
}
