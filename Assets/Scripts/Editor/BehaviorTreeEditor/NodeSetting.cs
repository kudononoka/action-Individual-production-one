using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>Nodeの設定専用クラス</summary>
public class NodeSetting
{
    BehaviorTreeGraphView _graphView;

    public NodeSetting(BehaviorTreeGraphView graphView)
    {
        _graphView = graphView;
    }

    /// <summary>Nodeの設定</summary>
    public void Setting(BehaviorTreeBaseNode nodeData, Rect rect, Node node)
    {
        SettingPortNode(nodeData,node, node.name, rect, nodeData.NodeData.NodeType);
    }

    /// <summary>NodeのPort設定</summary>
    public void SettingPortNode(BehaviorTreeBaseNode nodeData, Node node, string nodeName, Rect rect, NodeType type)
    {
        //ノードの種類によってPortの表示を変える
        if (type == NodeType.RootNode)
        {
            PortInstantiateSetting(node, Direction.Output, "output", Port.Capacity.Single);
        }
        else
        {
            PortInstantiateSetting(node, Direction.Input, "input", Port.Capacity.Single);

            switch (type)
            {
                case NodeType.CompositeNode:
                    PortInstantiateSetting(node, Direction.Output, "output", Port.Capacity.Multi);
                    break;

                case NodeType.DecoratorNode:
                    PortInstantiateSetting(node, Direction.Output, "action", Port.Capacity.Single);
                    PortInstantiateSetting(node, Direction.Output, "condition", Port.Capacity.Multi);
                    break;
            }
        }

        node.RefreshExpandedState();                    //Nodeの展開と更新
        node.RefreshPorts();

        node.SetPosition(new Rect(rect));               //Nodeの位置設定

        //Nodeの位置が変わったら
        node.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            //元データに保管
            nodeData.NodeData.Rect = node.GetPosition();
        });

    }

    /// <summary>Port生成</summary>
    private void PortInstantiateSetting(Node node, Direction portDirection, string portName, Port.Capacity capacity)
    {
        var port = node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        port.portName = portName;

        if (portDirection == Direction.Input)
            node.inputContainer.Add(port);
        else
            node.outputContainer.Add(port);
    }
}
