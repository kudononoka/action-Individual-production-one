using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpJudge : BehaviorTreeBaseNode
{
    EnemyHPController _hPController;

    [Header("HP’l")]
    [SerializeField] int _value;

    public HpJudge()
    {
        nodeName = "hp judge";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(HpJudge).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _hPController = my.GetComponent<EnemyAI>().HPController;
    }

    public override Result Evaluate()
    {
        if(_hPController.CurrentHPValue <= _value)
        {
            return Result.Success;
        }
        else
        {
            return Result.Failure;
        }
    }
}
