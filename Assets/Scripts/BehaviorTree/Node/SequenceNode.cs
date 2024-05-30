using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceNode : BehaviorTreeBaseNode, IChildNodeSetting
{
    public SequenceNode()
    {
        nodeName = "sequence";
        nodeData = new NodeData(NodeType.CompositeNode, typeof(SequenceNode).FullName);
    }

    private List<BehaviorTreeBaseNode> _childNodes = new List<BehaviorTreeBaseNode>();

    BehaviorTreeBaseNode _current = null;

    int _currentNodesIndex = 0;

    public void ChildNodeSet(BehaviorTreeBaseNode chileNode)
    {
        _childNodes.Add(chileNode);
    }

    public void ChildNodeRemove(BehaviorTreeBaseNode chileNode)
    {
        _childNodes.Remove(chileNode);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _childNodes.OrderBy(n => n.NodeData.Rect.position.y);
        _currentNodesIndex = 0;
        _current = _childNodes[_currentNodesIndex];
    }

    public override Result Evaluate()
    {
        if( _current == null )
        {
            _current = _childNodes[_currentNodesIndex];
        }

        Result result = _current.Evaluate();

        if (result == Result.Success)
        {
            _currentNodesIndex++;
            if (_currentNodesIndex == _childNodes.Count)
            {
                _current = null;
                _currentNodesIndex = 0;
                return Result.Success;
            }
            else
            {
                _current = _childNodes[_currentNodesIndex];
            }
        }

        if (result == Result.Failure)
        {
            return Result.Failure;
        }

        return Result.Runnimg;
    }
}
