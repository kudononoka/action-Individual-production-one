using UnityEngine;
/// <summary>ノードのタイプ　タイプによってWindow上に表示する形が変わってくる</summary>
public enum NodeType
{
    /// <summary>全体の開始点ノード</summary>
    RootNode,
    /// <summary>Sequence・Selector</summary>
    CompositeNode,
    /// <summary>条件付きで実行するノード</summary>
    DecoratorNode,
    /// <summary>ActionNode</summary>
    ActionNode,
    /// <summary>ConditionNode</summary>
    ConditionNode,
}

/// <summary>ノードの進行状況</summary>
public enum Result
{
    /// <summary>実行中</summary>
    Runnimg,
    /// <summary>成功</summary>
    Success,
    /// <summary>失敗</summary>
    Failure,
}

/// <summary>BehaviorTreeで使う専用基底クラスNode・Nodeクラスを継承</summary>
public abstract class BehaviorTreeBaseNode : ScriptableObject
{
    protected string nodeName;
    [SerializeField]
    protected NodeData nodeData;
    public string NodeName => nodeName;
    public NodeData NodeData => nodeData;

    /// <summary>Nodeの処理イベント</summary>
    /// <returns>Nodeの結果</returns>
    public abstract Result Evaluate();

    /// <summary>それぞれのNodeの初期化処理を行う</summary>
    /// <param name="target">敵となるTarget</param>
    /// <param name="my">実際に動かすGameObject</param>
    public abstract void Init(GameObject target, GameObject my);
}

