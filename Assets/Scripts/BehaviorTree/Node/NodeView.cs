using UnityEditor.Experimental.GraphView;

/// <summary>GraphView��Node���o�����߂̌��f�[�^</summary>
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
