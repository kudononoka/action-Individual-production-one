using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : BehaviorTreeBaseNode, IChildNodeSetting
{
    public RandomNode()
    {
        nodeName = "random";
        nodeData = new NodeData(NodeType.CompositeNode, typeof(RandomNode).FullName);
    }

    [SerializeField]
    private List<BehaviorTreeBaseNode> _childNodes = new List<BehaviorTreeBaseNode>();

    BehaviorTreeBaseNode _current = null;

    int _currentNodesIndex = 0;

    int _pastNodeIndex = 0;

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
        _current = null;
        _pastNodeIndex = 1;
    }

    public override Result Evaluate()
    {
        if (_current == null)
        {
            System.Random random = new System.Random();
            while (_currentNodesIndex == _pastNodeIndex)
            {
                _currentNodesIndex = random.Next(0, _childNodes.Count);
            }
            _current = _childNodes[_currentNodesIndex];
        }

        Result result = _current.Evaluate();

        if(result == Result.Success || result == Result.Failure)
        {
            _pastNodeIndex = _currentNodesIndex;
            _current = null;
        }

        return result;
    }


}
