using UnityEngine;
/// <summary>�m�[�h�̃^�C�v�@�^�C�v�ɂ����Window��ɕ\������`���ς���Ă���</summary>
public enum NodeType
{
    /// <summary>�S�̂̊J�n�_�m�[�h</summary>
    RootNode,
    /// <summary>Sequence�ESelector</summary>
    CompositeNode,
    /// <summary>�����t���Ŏ��s����m�[�h</summary>
    DecoratorNode,
    /// <summary>ActionNode</summary>
    ActionNode,
    /// <summary>ConditionNode</summary>
    ConditionNode,
}

/// <summary>�m�[�h�̐i�s��</summary>
public enum Result
{
    /// <summary>���s��</summary>
    Runnimg,
    /// <summary>����</summary>
    Success,
    /// <summary>���s</summary>
    Failure,
}

/// <summary>BehaviorTree�Ŏg����p���N���XNode�ENode�N���X���p��</summary>
public abstract class BehaviorTreeBaseNode : ScriptableObject
{
    protected string nodeName;
    [SerializeField]
    protected NodeData nodeData;
    public string NodeName => nodeName;
    public NodeData NodeData => nodeData;

    /// <summary>Node�̏����C�x���g</summary>
    /// <returns>Node�̌���</returns>
    public abstract Result Evaluate();

    /// <summary>���ꂼ���Node�̏������������s��</summary>
    /// <param name="target">�G�ƂȂ�Target</param>
    /// <param name="my">���ۂɓ�����GameObject</param>
    public abstract void Init(GameObject target, GameObject my);
}

