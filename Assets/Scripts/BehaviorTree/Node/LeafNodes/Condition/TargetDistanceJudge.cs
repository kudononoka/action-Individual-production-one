using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDistanceJudge : BehaviorTreeBaseNode
{
    public enum InequalitySign
    {
        Greater,
        Less
    }

    [Header("”»’è‚ÌŠî€‚Æ‚È‚éTarget‚Æ‚Ì‹——£")]
    [SerializeField] float _range = 10f;

    [Header("ğŒ‚ğ’è‚ß‚½‹——£‚æ‚è‰“‚¢‚©‹ß‚¢‚©")]
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
        float distance = Vector3.Distance(_target.position, _my.position);

        if(_inequalitySign == InequalitySign.Greater)
        {
            if (distance >= _range)
                return Result.Success;
            else
                return Result.Failure;
        }
        else
        {
            if (distance <= _range)
                return Result.Success;
            else
                return Result.Failure;
        }
    }
}
