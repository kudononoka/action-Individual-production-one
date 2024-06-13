using System;
using UnityEngine;

[Serializable]
public class RootNode : BehaviorTreeBaseNode, IChildNodeSetting
{
    [SerializeField]
    BehaviorTreeBaseNode _childNode;
    public RootNode()
    {
        nodeName = "root";
        nodeData = new NodeData(NodeType.RootNode, typeof(RootNode).FullName);
    }

    public void ChildNodeSet(BehaviorTreeBaseNode chileNode)
    {
        _childNode = chileNode;
    }

    public void ChildNodeRemove(BehaviorTreeBaseNode chileNode)
    {
        _childNode = null;
    }

    public override void Init(GameObject target, GameObject my) { }

    public override Result Evaluate()
    {
        if (_childNode == null)
        {
            return Result.Failure;
        }
        _childNode.Evaluate();
        return Result.Success;
    }
}