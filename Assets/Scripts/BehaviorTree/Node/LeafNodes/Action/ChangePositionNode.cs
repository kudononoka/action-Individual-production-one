using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangePositionNode : BehaviorTreeBaseNode
{
    [Header("Playerとの距離")]
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
        //Targetと自分の対角線となるベクトルを求める
        var vec = _my.position - _target.position;
        //求めたベクトル上　かつ　Targetから決められた距離である位置を求める
        Vector3 pos = (vec.normalized * _distance) + _target.position;
        //位置変更
        _my.position = pos;

        return Result.Success;
    }
}
