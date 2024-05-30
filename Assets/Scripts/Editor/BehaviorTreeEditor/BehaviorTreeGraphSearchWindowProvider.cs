using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>ビヘイビアツリー用の検索用Window</summary>
public class BehaviorTreeGraphSearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private BehaviorTreeGraphView _graphView;

    private BehaviorTreeEditorWindow _window;

    private SearchClassType _searchClassType = new();

    /// <summary>クラス名とTypeを連携</summary>
    private Dictionary<string, Type> typeDictionary;

    public void Init(BehaviorTreeGraphView graphView, BehaviorTreeEditorWindow window)
    { 
        _graphView = graphView;
        _window = window;
        typeDictionary = new Dictionary<string, Type>
        {
            { nameof(SelectorNode), typeof(SelectorNode) },
            { nameof(SequenceNode), typeof(SequenceNode) },
            { nameof(DecoratoeNodeCondition), typeof(DecoratoeNodeCondition) },
            { nameof(WaitNode), typeof(WaitNode) },
            { nameof(MoveToNode), typeof(MoveToNode) },
            { nameof(AttackNode), typeof(AttackNode) },
        };
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        //自作クラスの追加
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(SelectorNode))) { level = 1, userData = typeof(SelectorNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(SequenceNode))) { level = 1, userData = typeof(SequenceNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(DecoratoeNodeCondition))) { level = 1, userData = typeof(DecoratoeNodeCondition).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(WaitNode))) { level = 1, userData = typeof(WaitNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(MoveToNode))) { level = 1, userData = typeof(MoveToNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(AttackNode))) { level = 1, userData = typeof(AttackNode).FullName});
        return entries;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        //マウスのポジション取得
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
        var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);

        //class名取得
        string nodeName = SearchTreeEntry.userData.ToString();

        //typeDictionaryを使わずアセンブリから探す方法
        //object myObject = _searchClassType.CreateInstance(nodeName);
        //Type nodeType = SearchTreeEntry.userData.GetType();

        //if (myObject != null)
        //{
        //    Type myObjectType = myObject.GetType();
        //    _graphView.CreateNode(myObjectType,new Rect(localMousePosition, new Vector2(100, 150)), false);
        //}

        Type myObjectType = typeDictionary[nodeName]; //タイプ取得

        if(myObjectType != null)
            _graphView.CreateNode(myObjectType, new Rect(localMousePosition, new Vector2(100, 150)), false);

        return true;
    }
}
