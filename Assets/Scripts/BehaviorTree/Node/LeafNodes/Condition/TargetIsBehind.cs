using UnityEngine;

/// <summary>自分から見てTagetが後方にいるかどうか</summary>
public class TargetIsBehind : BehaviorTreeBaseNode
{
    Transform _targetTra;
    Transform _myTra;

    public　TargetIsBehind()
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
        Vector3 vec = (_targetTra.position - _myTra.position).normalized;　//自分からTargetへ向いたベクトルを取得
        float angle = Vector3.Angle(_myTra.forward, vec);　//自分の正面ベクトルとの角度を求める

        if (angle >=  90)　                      //自分の後方にTargetがいる
        {
            return Result.Success;
        }

        return Result.Failure;
    }
}
