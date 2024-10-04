using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratoeNodeCondition : BehaviorTreeBaseNode, IChildNodeSetting
{
    public DecoratoeNodeCondition()
    {
        nodeName = "decorator";
        nodeData = new NodeData(NodeType.DecoratorNode, typeof(DecoratoeNodeCondition).FullName);
    }

    [SerializeField]
    /// <summary>複数の条件ノード</summary>
    private List<BehaviorTreeBaseNode> _conditionsNodes = new List<BehaviorTreeBaseNode>();

    [SerializeField]
    /// <summary>条件が全てそろったらおこすアクション</summary>
    BehaviorTreeBaseNode _action = null;

    public void ChildNodeSet(BehaviorTreeBaseNode chileNode)
    {
        switch (chileNode.NodeData.NodeParameter.NodeType)
        {
            case NodeType.ConditionNode:
                _conditionsNodes.Add(chileNode);
                break;
            default:
                _action = chileNode;
                break;
        }
    }

    public void ChildNodeRemove(BehaviorTreeBaseNode chileNode)
    {
        switch (chileNode.NodeData.NodeParameter.NodeType)
        {
            case NodeType.ConditionNode:
                _conditionsNodes.Remove(chileNode);
                break;
            default:
                _action = null;
                break;
        }
    }

    public override void Init(GameObject target, GameObject my)
    {
    }

    public override Result Evaluate()
    {
        for (int i = 0; i < _conditionsNodes.Count; i++)        //条件と合っているか確認
        {
            Result result = _conditionsNodes[i].Evaluate();
            if (result == Result.Success)
            {
                continue;
            }
            
            return Result.Failure;         //条件が一つでも合わなかったら
        }

        Result resultAction = _action.Evaluate();         //条件がすべてそろったら

        if(resultAction == Result.Runnimg)
        {
            return Result.Runnimg;
        }

        return Result.Success;
    }

}
