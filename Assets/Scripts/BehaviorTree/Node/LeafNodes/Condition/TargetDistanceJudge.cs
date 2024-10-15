using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ターゲットとの距離で成功失敗を判断するNode </summary>
public class TargetDistanceJudge : BehaviorTreeBaseNode
{
    public enum InequalitySign
    {
        Greater,
        Less
    }

    [Header("判定の基準となるTargetとの距離")]
    [SerializeField] float _range = 10f;

    [Header("条件を定めた距離より遠いか近いか")]
    [SerializeField] InequalitySign _inequalitySign = InequalitySign.Greater;

    Transform _target;

    Transform _my;

    public TargetDistanceJudge()
    {
        nodeName = "target distance";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(TargetDistanceJudge).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
    }

    public override Result Evaluate()
    {
        //ターゲットと自分の距離取得
        float distance = Vector3.Distance(_target.position, _my.position);

        //不等号がより大きいだったら
        if(_inequalitySign == InequalitySign.Greater)
        {
            //距離が範囲外だったら
            if (distance >= _range)
                //成功を返す
                return Result.Success;

            else
                return Result.Failure;

        }
        //不等号がより小さいだったら
        else
        {
            //距離が範囲内だったら
            if (distance <= _range)
                //成功を返す
                return Result.Success;
            else
                return Result.Failure;
        }
    }
}
