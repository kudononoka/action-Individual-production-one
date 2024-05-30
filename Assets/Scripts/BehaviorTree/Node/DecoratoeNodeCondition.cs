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

    /// <summary>複数の条件ノード</summary>
    private List<BehaviorTreeBaseNode> _conditionsNodes = new List<BehaviorTreeBaseNode>();

    /// <summary>条件が全てそろったらおこすアクション</summary>
    BehaviorTreeBaseNode _action = null;

    public void ChildNodeSet(BehaviorTreeBaseNode chileNode)
    {
        switch (chileNode.NodeData.NodeType)
        {
            case NodeType.ActionNode:
                _action = chileNode;
                break;
            case NodeType.ConditionNode:
                _conditionsNodes.Add(chileNode);
                break;
        }
    }

    public void ChildNodeRemove(BehaviorTreeBaseNode chileNode)
    {
        switch (chileNode.NodeData.NodeType)
        {
            case NodeType.ActionNode:
                _action = null;
                break;
            case NodeType.ConditionNode:
                _conditionsNodes.Remove(chileNode);
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

        return _action.Evaluate();         //条件がすべてそろったら
    }

}
