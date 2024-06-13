using UnityEditor.Experimental.GraphView;

/// <summary>GraphViewにNodeを出すための元データ</summary>
public class NodeView : Node
{
    private BehaviorTreeBaseNode node;
    public BehaviorTreeBaseNode Node => node;   
    public NodeView(BehaviorTreeBaseNode node)
    {
        this.node = node;
        title = node.NodeName;
    }
}
