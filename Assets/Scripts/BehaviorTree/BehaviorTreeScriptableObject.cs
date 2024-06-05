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

    /// <summary>Save���̕����̃m�[�h���������Ă���f�[�^</summary>
    public List<BehaviorTreeBaseNode> Nodes => _nodes;

    /// <summary>Save���̃��[�g�f�[�^</summary>
    public BehaviorTreeBaseNode RootNodeData => _root;

    /// <summary>Save���̃��[�g�f�[�^�̏㏑��</summary>
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

    /// <summary>Node��ID</summary>
    public int ID { get => _id; set => _id = value; }

    /// <summary>BehaviorTreeBaseNode���p�������N���X�̖��O</summary>
    public string ClassName => _className;

    /// <summary>BehaviorTree�pNode��Type</summary>
    public NodeType NodeType => _nodeType;

    /// <summary>Node��Position��Size</summary>
    public Rect Rect { get => _rect; set => _rect = value; }

    /// <summary>�qNode��ID</summary>
    public List<ChildData> ChildData => _childData;

    /// <summary>���������qNode�̓o�^</summary>
    public void ChildIDAdd(ChildData childid) => _childData.Add(childid);

    /// <summary>���������qNode�̉���</summary>
    public void ChildDataRemoveAt(int index) => _childData.RemoveAt(index);

    /// <summary>���������S�qNode���㏑��</summary>
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

    /// <summary>BehaviorTree�pNodeType</summary>
    public NodeType NodeType => _nodeType;

    /// <summary>ID</summary>
    public int ID { get => _id; set => _id = value; }
}