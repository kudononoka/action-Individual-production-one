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
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale); //ズームの設定

        this.AddManipulator(new ContentDragger()); //キャンバス上の要素をドラッグして位置を変更可能にする
        this.AddManipulator(new SelectionDragger()); //選択したNodeをドラッグで移動可能にする
        this.AddManipulator(new RectangleSelector()); //範囲選択を可能にする

        var grid = new GridBackground();
        Insert(0, grid);

        //Node作成する時の検索Window設定
        var searchWindowProvider = ScriptableObject.CreateInstance<BehaviorTreeGraphSearchWindowProvider>();
        searchWindowProvider.Init(this, window);
        this.nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
        };

        if(_window.Data.RootNodeData == null)           //ルート作成
        {
            CreateNode(typeof(RootNode), new Rect(100, 200, 100, 150), true);
        }

        //Graphの見た目が変わった時に呼んでもらうメソッドを追加
        this.graphViewChanged += DeleteNode;
        this.graphViewChanged += EdgeCreateDelete;
    }

    /// <summary>Port同士を繋ぐときの条件が書かれている</summary>
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            //同じPortはつながらないようにする
            //同じNodeのPort同士はつながらないようにする
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    /// <summary>ノードの作成と表示</summary>
    /// <param name="nodeType">ノードクラスType</param>
    /// <param name="rect">Nodeの場所</param>
    /// <param name="isRoot">ルートノードかどうか</param>
    public void CreateNode(Type nodeType, Rect rect, bool isRoot)
    {
        var node = _window.CreateNode(nodeType, rect, isRoot);
        if (!isRoot)
        {
            CreatNodeView(node);
        }
    }

    /// <summary>ノードの表示</summary>
    public void CreatNodeView(BehaviorTreeBaseNode node)
    {
        //Nodeの設定
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

        //Nodeが選択されたら
        nodeView.RegisterCallback<MouseDownEvent>(evt =>
        {
            if (evt.clickCount == 1)
            {
                //ScriptableObject(ノード)のプロパティをInspectorに表示
                Selection.objects = new UnityEngine.Object[] { nodeView.Node };
            }
        });

        AddElement(nodeView);
    }

    /// <summary>Nodeの削除時に呼ばれる</summary>
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

    /// <summary>Edgeの接続と切断時に呼ばれる</summary>
    private GraphViewChange EdgeCreateDelete(GraphViewChange graphViewChange)
    {
        //Edgeの接続時
        if (graphViewChange.edgesToCreate != null)
        {
            foreach (Edge edge in graphViewChange.edgesToCreate)
            {
                var parentNode = edge.output.node;
                var parentNodeView = parentNode as NodeView;
                var childNodeView = edge.input.node as NodeView;
                BehaviorTreeBaseNode nodeData = parentNodeView.Node;

                //親ノードに子ノード情報を追加
                if (childNodeView != null && nodeData is IChildNodeSetting iChildNodeSet)
                {
                    iChildNodeSet.ChildNodeSet(childNodeView.Node);
                    _window.ChildNodeDataAdd(parentNodeView.Node.NodeData.ID, 
                        new ChildData(childNodeView.Node.NodeData.ID, childNodeView.Node.NodeData.NodeType));
                }
            }
        }

        //Edgeの切断時
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

                    //親ノードの子ノード情報を解除
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

    /// <summary>Edgeの接続</summary>
    /// <param name="parentID">親ノードID</param>
    /// <param name="childID">子ノードID</param>
    /// <param name="outputNum">親NodePortのContainerのNumber</param>
    /// <param name="inputNum">子NodePortのContainerのNumber</param>
    public void ConnectNodes(int parentID, int childID, int outputNum = 0, int inputNum = 0)
    {
        Node parentNode = parentID == -1 ? rootNode : _nodes[parentID];
        Node childNode = _nodes[childID];

        Port outputPort = parentNode.outputContainer[outputNum] as Port;
        Port inputPort = childNode.inputContainer[inputNum] as Port;

        Edge edge = new Edge                 //Edgeの作成
        {
            output = outputPort,
            input = inputPort
        };

        outputPort.Connect(edge);　          //Edgeの接続
        inputPort.Connect(edge);
        this.AddElement(edge);
    }
}
