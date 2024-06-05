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

    [Header("íçñ⁄ÇµÇΩÇ¢íl")]
    [SerializeField]Å@NumType _numType;

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

    [Header("Ç«ÇÃêîéöÇ…Ç¬Ç¢Çƒíçñ⁄Ç∑ÇÈÇ©")]
    [SerializeField] NumType _numType;

    [Header("íËÇﬂÇΩêîéöÇÊÇËà»â∫Ç…Ç»ÇÈÇ∆SuccessÇï‘Ç∑")]
    [SerializeField] int _numLess;

    public int NumLess { get => _numLess; set => _numLess = value; }
}

public enum NumType
{
    HP,
}
