using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>決められたHP以下だったら成功を返すNode</summary>
public class HpJudge : BehaviorTreeBaseNode
{
    EnemyHPController _hPController;

    [Header("HP値")]
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
        //現在のHPが一定の値以下だったら
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
