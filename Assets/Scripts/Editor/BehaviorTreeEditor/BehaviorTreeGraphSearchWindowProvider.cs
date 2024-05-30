using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>�r�w�C�r�A�c���[�p�̌����pWindow</summary>
public class BehaviorTreeGraphSearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private BehaviorTreeGraphView _graphView;

    private BehaviorTreeEditorWindow _window;

    private SearchClassType _searchClassType = new();

    /// <summary>�N���X����Type��A�g</summary>
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
        //����N���X�̒ǉ�
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
        //�}�E�X�̃|�W�V�����擾
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
        var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);

        //class���擾
        string nodeName = SearchTreeEntry.userData.ToString();

        //typeDictionary���g�킸�A�Z���u������T�����@
        //object myObject = _searchClassType.CreateInstance(nodeName);
        //Type nodeType = SearchTreeEntry.userData.GetType();

        //if (myObject != null)
        //{
        //    Type myObjectType = myObject.GetType();
        //    _graphView.CreateNode(myObjectType,new Rect(localMousePosition, new Vector2(100, 150)), false);
        //}

        Type myObjectType = typeDictionary[nodeName]; //�^�C�v�擾

        if(myObjectType != null)
            _graphView.CreateNode(myObjectType, new Rect(localMousePosition, new Vector2(100, 150)), false);

        return true;
    }
}
