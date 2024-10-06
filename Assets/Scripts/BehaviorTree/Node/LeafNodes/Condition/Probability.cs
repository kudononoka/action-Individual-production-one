using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Probability : BehaviorTreeBaseNode
{
    [Header("パーセント")]
    [SerializeField, Range(0, 100)] int _percent;

    public Probability()
    {
        nodeName = "probability";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(Probability).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
    }

    public override Result Evaluate()
    {
        int randomNum = Random.Range(0, 101);

        Debug.Log(randomNum);

        if (randomNum <= _percent)
        {
            return Result.Success;
        }
        
        return Result.Failure;
    }
}
