using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WaitNode : BehaviorTreeBaseNode
{ 
    float _timer;

    [Header("待ち時間")]
    [SerializeField] float _waitTime;

    public WaitNode()
    {
        nodeName = "wait";
        nodeData = new NodeData(NodeType.ActionNode, typeof(WaitNode).FullName);
    }
    public override void Init(GameObject target, GameObject my)
    {
        _timer = 0;
    }
    public override Result Evaluate()
    {
        _timer += Time.deltaTime;

        if(_timer >= _waitTime) //待ち時間経過
        {
            _timer = 0; //初期化
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
