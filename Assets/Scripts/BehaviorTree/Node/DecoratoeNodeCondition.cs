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

    /// <summary>�����̏����m�[�h</summary>
    private List<BehaviorTreeBaseNode> _conditionsNodes = new List<BehaviorTreeBaseNode>();

    /// <summary>�������S�Ă�������炨�����A�N�V����</summary>
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
        for (int i = 0; i < _conditionsNodes.Count; i++)        //�����ƍ����Ă��邩�m�F
        {
            Result result = _conditionsNodes[i].Evaluate();
            if (result == Result.Success)
            {
                continue;
            }
            
            return Result.Failure;         //��������ł�����Ȃ�������
        }

        return _action.Evaluate();         //���������ׂĂ��������
    }

}
