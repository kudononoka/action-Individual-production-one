using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEditor;
using System.Linq;

public class BehaviorTreeGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(100, 150);

    BehaviorTreeEditorWindow _window = null;

    List<NodeView> _nodes = new List<NodeView>();
    NodeView rootNode = null;

    public BehaviorTreeGraphView(BehaviorTreeEditorWindow window)
    {
        _window = window;

        styleSheets.Add(Resources.Load<StyleSheet>("BehaviorTree"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale); //�Y�[���̐ݒ�

        this.AddManipulator(new ContentDragger()); //�L�����o�X��̗v�f���h���b�O���Ĉʒu��ύX�\�ɂ���
        this.AddManipulator(new SelectionDragger()); //�I������Node���h���b�O�ňړ��\�ɂ���
        this.AddManipulator(new RectangleSelector()); //�͈͑I�����\�ɂ���

        var grid = new GridBackground();
        Insert(0, grid);

        //Node�쐬���鎞�̌���Window�ݒ�
        var searchWindowProvider = ScriptableObject.CreateInstance<BehaviorTreeGraphSearchWindowProvider>();
        searchWindowProvider.Init(this, window);
        this.nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
        };

        if(_window.Data.RootNodeData == null)           //���[�g�쐬
        {
            CreateNode(typeof(RootNode), new Rect(100, 200, 100, 150), true);
        }

        //Graph�̌����ڂ��ς�������ɌĂ�ł��炤���\�b�h��ǉ�
        this.graphViewChanged += DeleteNode;
        this.graphViewChanged += EdgeCreateDelete;
    }

    /// <summary>Port���m���q���Ƃ��̏�����������Ă���</summary>
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            //����Port�͂Ȃ���Ȃ��悤�ɂ���
            //����Node��Port���m�͂Ȃ���Ȃ��悤�ɂ���
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    /// <summary>�m�[�h�̍쐬�ƕ\��</summary>
    /// <param name="nodeType">�m�[�h�N���XType</param>
    /// <param name="rect">Node�̏ꏊ</param>
    /// <param name="isRoot">���[�g�m�[�h���ǂ���</param>
    public void CreateNode(Type nodeType, Rect rect, bool isRoot)
    {
        var node = _window.CreateNode(nodeType, rect, isRoot);
        CreatNodeView(node);
    }

    /// <summary>�m�[�h�̕\��</summary>
    public void CreatNodeView(BehaviorTreeBaseNode node)
    {
        //Node�̐ݒ�
        NodeSetting setting = new NodeSetting(this);
        NodeView nodeView = new NodeView(node);
        setting.Setting(node, node.NodeData.Rect, nodeView);
        if (nodeView.Node.NodeData.NodeType == NodeType.RootNode)
        {
            rootNode = nodeView;
        }
        else
        {
            _nodes.Add(nodeView);
        }

        //Node���I�����ꂽ��
        nodeView.RegisterCallback<MouseDownEvent>(evt =>
        {
            if (evt.clickCount == 1)
            {
                //ScriptableObject(�m�[�h)�̃v���p�e�B��Inspector�ɕ\��
                Selection.objects = new UnityEngine.Object[] { nodeView.Node };
            }
        });

        AddElement(nodeView);
    }

    /// <summary>Node�̍폜���ɌĂ΂��</summary>
    private GraphViewChange DeleteNode(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            foreach (GraphElement element in graphViewChange.elementsToRemove)
            {
                if (element is NodeView node)
                {
                    _window.DeleteNode(node.Node.NodeData.ID);
                }
            }
        }
        return graphViewChange;
    }

    /// <summary>Edge�̐ڑ��Ɛؒf���ɌĂ΂��</summary>
    private GraphViewChange EdgeCreateDelete(GraphViewChange graphViewChange)
    {
        //Edge�̐ڑ���
        if (graphViewChange.edgesToCreate != null)
        {
            foreach (Edge edge in graphViewChange.edgesToCreate)
            {
                var parentNode = edge.output.node;
                var parentNodeView = parentNode as NodeView;
                var childNodeView = edge.input.node as NodeView;
                BehaviorTreeBaseNode nodeData = parentNodeView.Node;

                //�e�m�[�h�Ɏq�m�[�h����ǉ�
                if (childNodeView != null && nodeData is IChildNodeSetting iChildNodeSet)
                {
                    iChildNodeSet.ChildNodeSet(childNodeView.Node);
                    _window.ChildNodeDataAdd(parentNodeView.Node.NodeData.ID, 
                        new ChildData(childNodeView.Node.NodeData.ID, childNodeView.Node.NodeData.NodeType));
                }
            }
        }

        //Edge�̐ؒf��
        if (graphViewChange.elementsToRemove != null)
        {
            foreach (GraphElement element in graphViewChange.elementsToRemove)
            {
                if (element is Edge edge)
                {
                    var parentNode = edge.output.node;
                    var parentNodeView = parentNode as NodeView;
                    var childNodeView = edge.input.node as NodeView;
                    BehaviorTreeBaseNode node = parentNodeView.Node;

                    //�e�m�[�h�̎q�m�[�h��������
                    if (childNodeView != null && node is IChildNodeSetting iChildNodeSet)
                    {
                        iChildNodeSet.ChildNodeRemove(childNodeView.Node);
                        _window.ChildNodeDataRemove(parentNodeView.Node.NodeData.ID, childNodeView.Node.NodeData.ID);
                    }
                }
            }
        }

        return graphViewChange;
    }

    /// <summary>Edge�̐ڑ�</summary>
    /// <param name="parentID">�e�m�[�hID</param>
    /// <param name="childID">�q�m�[�hID</param>
    /// <param name="outputNum">�eNodePort��Container��Number</param>
    /// <param name="inputNum">�qNodePort��Container��Number</param>
    public void ConnectNodes(int parentID, int childID, int outputNum = 0, int inputNum = 0)
    {
        Node parentNode = parentID == -1 ? rootNode : _nodes[parentID];
        Node childNode = _nodes[childID];

        Port outputPort = parentNode.outputContainer[outputNum] as Port;
        Port inputPort = childNode.inputContainer[inputNum] as Port;

        Edge edge = new Edge                 //Edge�̍쐬
        {
            output = outputPort,
            input = inputPort
        };

        outputPort.Connect(edge);�@          //Edge�̐ڑ�
        inputPort.Connect(edge);
        this.AddElement(edge);
    }
}
