using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

/// <summary>
/// ビヘイビアツリーをWindowに表示するためのEditorWindow
/// </summary>
public class BehaviorTreeEditorWindow : EditorWindow
{
    private BehaviorTreeGraphView _graphView;

    /// <summary>Asset内にあるNode保管用データ</summary>
    private BehaviorTreeScriptableObject _data;

    /// <summary>BehaviorTreeScriptableObjectのpath</summary>
    private static string aseetpath = "";

    private List<BehaviorTreeScriptableObject> objects = new List<BehaviorTreeScriptableObject>(); 
    
    public BehaviorTreeScriptableObject Data => _data;

    /// <summary>Windowを開く</summary>
    public static void Open(BehaviorTreeScriptableObject data)
    {
        aseetpath = AssetDatabase.GetAssetPath(data);
        var window = (BehaviorTreeEditorWindow)GetWindow(typeof(BehaviorTreeEditorWindow));
        window.Show();
    }

    private void OnEnable()
    {
        _data = (BehaviorTreeScriptableObject)AssetDatabase.LoadAssetAtPath(aseetpath, typeof(BehaviorTreeScriptableObject));
        
        ConstructGraphView();

        if (_data != null)
        {
            Load();
        }
    }

    /// <summary>GraphViewの設定</summary>
    private void ConstructGraphView()
    {
        _graphView = new BehaviorTreeGraphView(this)
        {
            name = "Behavior Tree Graph"
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
        wantsMouseMove = true;
    }

    private void OnDisable()
    {
        EditorUtility.SetDirty(_data);
        AssetDatabase.SaveAssets();
    }

    /// <summary>ノード作成</summary>
    /// <param name="type">生成するScriptableObjectのType</param>
    /// <param name="rect">Nodeの場所</param>
    /// <param name="root">作成するNodeがルートかどうか</param>
    /// <returns></returns>
    public BehaviorTreeBaseNode CreateNode(Type type, Rect rect, bool root)
    {
        BehaviorTreeBaseNode node = ScriptableObject.CreateInstance(type) as BehaviorTreeBaseNode;

        //ScriptableObjectデータにNodeの情報を代入
        if (root)
        {
            _data.RootNodeDataOverwrite(node);
            node.NodeData.ID = -1;
        }
        else
        {
            _data.Nodes.Add(node);
            node.NodeData.ID = _data.Nodes.Count - 1;
        }
        node.NodeData.Rect = rect;
        

        AssetDatabase.AddObjectToAsset(node, _data);
        AssetDatabase.SaveAssets();

        return node;
    }

    /// <summary>ノードの削除</summary>
    /// <param name="id">削除するノードID</param>
    public void DeleteNode(int id)
    {
        if (id == -1 || id > _data.Nodes.Count) return;

        AssetDatabase.RemoveObjectFromAsset(_data.Nodes[id]);
        AssetDatabase.SaveAssets();

        _data.Nodes.RemoveAt(id);
        for (int i = id; i < _data.Nodes.Count; i++)
        {
            _data.Nodes[i].NodeData.ID -= 1;
        }
        ChildIDChange(id);
    }

    /// <summary>親ノードデータに子ノード情報を保管</summary>
    /// <param name="id">親ノードID</param>
    /// <param name="child">子ノードID</param>
    public void ChildNodeDataAdd(int id, ChildData child)
    {
        if (id == -1)
            _data.RootNodeData.NodeData.ChildIDAdd(child);
        else
            _data.Nodes[id].NodeData.ChildIDAdd(child);
    }

    /// <summary>親ノードデータが持っている子ノード情報を解除</summary>
    /// <param name="id">親ノードID</param>
    /// <param name="child">解除する子ノードID</param>
    public void ChildNodeDataRemove(int id, int childID)
    {
        if (id == -1)
        {
            _data.RootNodeData.NodeData.ChildDataRemoveAt(0);
        }
        else
        {
            for (int i = 0; i < _data.Nodes[id].NodeData.ChildData.Count; i++)
            {
                if (_data.Nodes[id].NodeData.ChildData[i].ID == childID)
                {
                    _data.Nodes[id].NodeData.ChildDataRemoveAt(i);
                    break;
                }
            }
        }
    }

    public void ChildIDChange(int index)
    {
        if (_data.RootNodeData.NodeData.ChildData.Count > 0 
            &&_data.RootNodeData.NodeData.ChildData[0].ID == index)
        {
            ChildNodeDataRemove(-1, index);
        }

        for (int i = 0; i < _data.Nodes.Count; i++)
        {
            for (int j = 0; j < _data.Nodes[i].NodeData.ChildData.Count; j++)
            {
                //if (_data.Nodes[i].NodeData.ChildData[j].ID == index)
                //{
                //    _data.Nodes[i].NodeData.ChildDataRemoveAt(j);
                //}
                if (_data.Nodes[i].NodeData.ChildData[j].ID > index)
                {
                    _data.Nodes[i].NodeData.ChildData[j].ID -= 1;
                }
            }
        }
    }

    /// <summary>ScriptableObjectのデータを取得してNodeとEdgeの再表示を行う</summary>
    void Load()
    {
        LoadNodeView();
        LoadConectView();
    }

    /// <summary>
    /// Nodeの表示
    /// </summary>
    void LoadNodeView()
    {
        if (_data.RootNodeData != null)
        {
            _graphView.CreatNodeView(_data.RootNodeData);
        }

        for (int i = 0; i < _data.Nodes.Count; i++)
        {
            _graphView.CreatNodeView(_data.Nodes[i]);
        }
    }

    /// <summary>Node同士を繋ぐEdgeの表示</summary>
    void LoadConectView()
    {
        if (_data.RootNodeData.NodeData.ChildData.Count > 0)
            _graphView.ConnectNodes(_data.RootNodeData.NodeData.ID, _data.RootNodeData.NodeData.ChildData[0].ID);

        for (int i = 0; i < _data.Nodes.Count; i++)
        {
            if (_data.Nodes[i].NodeData.ChildData.Count > 0)
            {
                NodeData node = _data.Nodes[i].NodeData;
                if (node.NodeType == NodeType.DecoratorNode)
                {
                    for (int j = 0; j < node.ChildData.Count; j++)
                    {
                        if (node.ChildData[j].NodeType == NodeType.ConditionNode)
                            _graphView.ConnectNodes(node.ID, node.ChildData[j].ID, 1, 0);
                        else
                            _graphView.ConnectNodes(node.ID, node.ChildData[j].ID, 0, 0);
                    }
                }
                else
                {
                    for (int j = 0; j < node.ChildData.Count; j++)
                    {
                        _graphView.ConnectNodes(node.ID, node.ChildData[j].ID);
                    }
                }
            }
        }
    }

}
