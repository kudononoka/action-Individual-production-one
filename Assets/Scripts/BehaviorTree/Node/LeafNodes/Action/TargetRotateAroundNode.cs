using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotateAroundNode : BehaviorTreeBaseNode
{
    [Header("‰ñ‚Á‚Ä‚¢‚éŽžŠÔ")]
    [SerializeField] float _rotateAroundTime;

    [Header("‰ñ‚é‘¬“x")]
    [SerializeField] float _aroundSpeed;

    Transform _target;

    Transform _my;

    float _timer;

    public TargetRotateAroundNode()
    {
        nodeName = "rotate around";
        nodeData = new NodeData(NodeType.ActionNode, typeof(TargetRotateAroundNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
        _timer = 0;
    }

    public override Result Evaluate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _rotateAroundTime)
        {
            _timer = 0;
            return Result.Success;
        }

        _my.LookAt(_target.position);
        _my.RotateAround(_target.position, Vector3.down, _aroundSpeed);

        return Result.Runnimg;
    }
}
