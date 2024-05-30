using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class SelectorNode : BehaviorTreeBaseNode, IChildNodeSetting
{
    public SelectorNode()
    {
        nodeName = "selector";
        nodeData = new NodeData(NodeType.CompositeNode, typeof(SelectorNode).FullName);
    }

    private List<BehaviorTreeBaseNode> _childNodes = new List<BehaviorTreeBaseNode>();

    BehaviorTreeBaseNode _current = null;

    int _currentChildIndex = 0;

    public override void Init(GameObject target, GameObject my)
    {
        _childNodes.OrderBy(n => n.NodeData.Rect.position.y);
        _currentChildIndex = 0;
        _current = _childNodes[_currentChildIndex];
    }

    public void ChildNodeSet(BehaviorTreeBaseNode chileNode)
    {
        _childNodes.Add(chileNode);
    }

    public void ChildNodeRemove(BehaviorTreeBaseNode chileNode)
    {
        _childNodes.Remove(chileNode);
    }

    public override Result Evaluate()
    {
        Result result = _current.Evaluate();

        if (result == Result.Success)  
        {
            _current = null;
            _currentChildIndex = 0;
            return Result.Success;
        }

        if (result == Result.Failure)
        {
            _currentChildIndex++;
            if (_currentChildIndex >= _childNodes.Count)
            {
                return Result.Failure;
            }
            _current = _childNodes[_currentChildIndex];
        }

        return Result.Runnimg;
    }

}