using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SequenceNode : BehaviorTreeBaseNode, IChildNodeSetting
{
    public SequenceNode()
    {
        nodeName = "sequence";
        nodeData = new NodeData(NodeType.CompositeNode, typeof(SequenceNode).FullName);
    }

    [SerializeField]
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
        _childNodes = _childNodes.OrderBy(n => n.NodeData.Rect.position.y).ToList();
        _currentNodesIndex = 0;
        _current = _childNodes[_currentNodesIndex];
    }

    public override Result Evaluate()
    {
        //実行する子Nodeがなかったら
        if( _current == null )
        {
            _currentNodesIndex = 0;
            //０番目のNodeを入れる
            _current = _childNodes[_currentNodesIndex];
        }

        //子Node実行
        Result result = _current.Evaluate();

        //成功が返ってきたら
        if (result == Result.Success)
        {
            _currentNodesIndex++;
            //全ての子Nodeの実行が終わったら
            if (_currentNodesIndex == _childNodes.Count)
            {
                _current = null;
               　//成功を返す
                return Result.Success;
            }
            else
            {
                //次の子Nodeに移行
                _current = _childNodes[_currentNodesIndex];
            }
        }

        //失敗が返ってきたら
        if (result == Result.Failure)
        {
            _current = null;
            //失敗を返す
            return Result.Failure;
        }

        return Result.Runnimg;
    }
}
