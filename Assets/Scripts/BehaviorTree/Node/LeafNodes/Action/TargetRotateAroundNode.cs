using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotateAroundNode : BehaviorTreeBaseNode
{
    [Header("回っている時間")]
    [SerializeField] float _rotateAroundTime;

    [Header("回る速度")]
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

        //ターゲットの方を向く
        _my.LookAt(_target.position);

        //ターゲットを中心にまわる
        _my.RotateAround(_target.position, Vector3.down, _aroundSpeed);

        return Result.Runnimg;
    }
}
