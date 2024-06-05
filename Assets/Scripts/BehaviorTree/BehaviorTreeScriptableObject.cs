using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
[CreateAssetMenu]
public class BehaviorTreeScriptableObject : ScriptableObject
{
    [SerializeField]
    BehaviorTreeBaseNode _root;
    [SerializeField]
    List<BehaviorTreeBaseNode> _nodes = new List<BehaviorTreeBaseNode>();

    /// <summary>Save元の複数のノードを所持しているデータ</summary>
    public List<BehaviorTreeBaseNode> Nodes => _nodes;

    /// <summary>Save元のルートデータ</summary>
    public BehaviorTreeBaseNode RootNodeData => _root;

    /// <summary>Save元のルートデータの上書き</summary>
    public void RootNodeDataOverwrite(BehaviorTreeBaseNode data) => _root = data;

    public Result Evaluate()
    {
        if (_root == null)
        {
            return Result.Failure;
        }
        return _root.Evaluate();
    }
}

[Serializable]
public class NodeData
{
    public NodeData(NodeType nodeType, string className)
    {
        _nodeType = nodeType;
        _className = className;
    }
    [SerializeField]
    private NodeType _nodeType;
    [SerializeField]
    private string _className;
    [SerializeField]
    private int _id;
    [SerializeField]
    private Rect _rect;
    [SerializeField]
    private List<ChildData> _childData = new List<ChildData>();

    /// <summary>NodeのID</summary>
    public int ID { get => _id; set => _id = value; }

    /// <summary>BehaviorTreeBaseNodeを継承したクラスの名前</summary>
    public string ClassName => _className;

    /// <summary>BehaviorTree用NodeのType</summary>
    public NodeType NodeType => _nodeType;

    /// <summary>NodeのPositionとSize</summary>
    public Rect Rect { get => _rect; set => _rect = value; }

    /// <summary>子NodeのID</summary>
    public List<ChildData> ChildData => _childData;

    /// <summary>自分が持つ子Nodeの登録</summary>
    public void ChildIDAdd(ChildData childid) => _childData.Add(childid);

    /// <summary>自分が持つ子Nodeの解除</summary>
    public void ChildDataRemoveAt(int index) => _childData.RemoveAt(index);

    /// <summary>自分が持つ全子Nodeを上書き</summary>
    public void ChildIDListOverwrite(List<ChildData> children) => _childData = children;
}

[Serializable]
public class ChildData
{
    public ChildData(int id, NodeType nodeType)
    {
        _id = id;
        _nodeType = nodeType;
    }
    [SerializeField]
    private int _id;
    [SerializeField]
    private NodeType _nodeType;

    /// <summary>BehaviorTree用NodeType</summary>
    public NodeType NodeType => _nodeType;

    /// <summary>ID</summary>
    public int ID { get => _id; set => _id = value; }
}