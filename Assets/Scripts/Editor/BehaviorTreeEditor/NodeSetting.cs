using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>Node�̐ݒ��p�N���X</summary>
public class NodeSetting
{
    BehaviorTreeGraphView _graphView;

    public NodeSetting(BehaviorTreeGraphView graphView)
    {
        _graphView = graphView;
    }

    /// <summary>Node�̐ݒ�</summary>
    public void Setting(BehaviorTreeBaseNode nodeData, Rect rect, Node node)
    {
        SettingPortNode(nodeData,node, node.name, rect, nodeData.NodeData.NodeType);
    }

    /// <summary>Node��Port�ݒ�</summary>
    public void SettingPortNode(BehaviorTreeBaseNode nodeData, Node node, string nodeName, Rect rect, NodeType type)
    {
        //�m�[�h�̎�ނɂ����Port�̕\����ς���
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

        node.RefreshExpandedState();                    //Node�̓W�J�ƍX�V
        node.RefreshPorts();

        node.SetPosition(new Rect(rect));               //Node�̈ʒu�ݒ�

        //Node�̈ʒu���ς������
        node.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            //���f�[�^�ɕۊ�
            nodeData.NodeData.Rect = node.GetPosition();
        });

    }

    /// <summary>Port����</summary>
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
