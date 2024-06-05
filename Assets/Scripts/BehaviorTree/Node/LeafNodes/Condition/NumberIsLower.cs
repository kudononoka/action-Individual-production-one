using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOrLess : BehaviorTreeBaseNode
{
    [SerializeField]
    NumData[] _numDatas =
    {
        new NumData(NumType.HP)
    };

    [Header("注目したい値")]
    [SerializeField]　NumType _numType;

    EnemyHPController _enemyHPSTController;

    public NumberOrLess()
    {
        nodeName = "number or less";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(NumberOrLess).FullName);
    }
    public override void Init(GameObject target, GameObject my)
    {
        _enemyHPSTController = my.GetComponent<EnemyAI>().EnemyHPSTController;
        _numDatas[0].NumLess = _enemyHPSTController.HpMax / 2;
    }

    public override Result Evaluate()
    {
        if (_enemyHPSTController.CurrentHPValue <= _numDatas[(int)_numType].NumLess)
        {
            return Result.Success;
        }
        return Result.Failure;
    }
}

public class NumData
{
    public NumData(NumType numType)
    {
        _numType = numType;
    }

    [Header("どの数字について注目するか")]
    [SerializeField] NumType _numType;

    [Header("定めた数字より以下になるとSuccessを返す")]
    [SerializeField] int _numLess;

    public int NumLess { get => _numLess; set => _numLess = value; }
}

public enum NumType
{
    HP,
}
