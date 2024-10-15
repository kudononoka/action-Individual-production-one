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

    [SerializeField]
    private List<BehaviorTreeBaseNode> _childNodes = new List<BehaviorTreeBaseNode>();

    BehaviorTreeBaseNode _current = null;

    int _currentChildIndex = 0;

    public override void Init(GameObject target, GameObject my)
    {
        _childNodes = _childNodes.OrderBy(n => n.NodeData.Rect.position.y).ToList();
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
        //実行する子Nodeがなかったら
        if (_current == null)
        {
            _currentChildIndex = 0;
            //０番目のNodeを入れる
            _current = _childNodes[_currentChildIndex];
        }

        //子Node実行
        Result result = _current.Evaluate();

        //成功が返ってきたら
        if (result == Result.Success)  
        {
            _current = null;
            //成功を返す
            return Result.Success;
        }

        //失敗が返ってきたら
        if (result == Result.Failure)
        {
            _currentChildIndex++;
            //全ての子Nodeの実行が終わったら
            if (_currentChildIndex == _childNodes.Count)
            {
                _current = null;
                //失敗を返す
                return Result.Failure;
            }

            //次の子Nodeに移行
            _current = _childNodes[_currentChildIndex];
        }

        return Result.Runnimg;
    }

}