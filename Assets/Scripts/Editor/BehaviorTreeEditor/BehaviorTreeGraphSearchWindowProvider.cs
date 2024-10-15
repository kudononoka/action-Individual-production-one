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
            { nameof(MoveToTargetNode), typeof(MoveToTargetNode) },
            { nameof(RandomNode), typeof(RandomNode) },
            { nameof(LookAt), typeof(LookAt) },
            { nameof(IsVisible), typeof(IsVisible) },
            { nameof(IsAudible), typeof(IsAudible) },
            { nameof(ReturnToMyPostNode), typeof(ReturnToMyPostNode) },
            { nameof(MoveToSoundLocationNode), typeof(MoveToSoundLocationNode) },
            { nameof(MinorEnemyAttackNode), typeof(MinorEnemyAttackNode) },
            { nameof(AttackNode), typeof(AttackNode) },
            { nameof(AnimationNode), typeof(AnimationNode) },
            { nameof(TargetDistanceJudge), typeof(TargetDistanceJudge) },
            { nameof(SetActiveNode), typeof(SetActiveNode) },
            { nameof(ChangePositionNode), typeof(ChangePositionNode) },
            { nameof(EffectPlayNode), typeof(EffectPlayNode) },
            { nameof(Probability), typeof(Probability) },
            { nameof(TargetRotateAroundNode), typeof(TargetRotateAroundNode) },
            { nameof(HpJudge), typeof(HpJudge) },
            { nameof(SoundPlay), typeof(SoundPlay) },

        };
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        //自作ノードクラスの追加
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(SelectorNode))) { level = 1, userData = typeof(SelectorNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(SequenceNode))) { level = 1, userData = typeof(SequenceNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(DecoratoeNodeCondition))) { level = 1, userData = typeof(DecoratoeNodeCondition).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(WaitNode))) { level = 1, userData = typeof(WaitNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(MoveToTargetNode))) { level = 1, userData = typeof(MoveToTargetNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(RandomNode))) { level = 1, userData = typeof(RandomNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(LookAt))) { level = 1, userData = typeof(LookAt).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(IsVisible))) { level = 1, userData = typeof(IsVisible).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(IsAudible))) { level = 1, userData = typeof(IsAudible).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(ReturnToMyPostNode))) { level = 1, userData = typeof(ReturnToMyPostNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(MoveToSoundLocationNode))) { level = 1, userData = typeof(MoveToSoundLocationNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(MinorEnemyAttackNode))) { level = 1, userData = typeof(MinorEnemyAttackNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(AttackNode))) { level = 1, userData = typeof(AttackNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(AnimationNode))) { level = 1, userData = typeof(AnimationNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(TargetDistanceJudge))) { level = 1, userData = typeof(TargetDistanceJudge).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(SetActiveNode))) { level = 1, userData = typeof(SetActiveNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(ChangePositionNode))) { level = 1, userData = typeof(ChangePositionNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(EffectPlayNode))) { level = 1, userData = typeof(EffectPlayNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(Probability))) { level = 1, userData = typeof(Probability).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(TargetRotateAroundNode))) { level = 1, userData = typeof(TargetRotateAroundNode).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(HpJudge))) { level = 1, userData = typeof(HpJudge).FullName});
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(SoundPlay))) { level = 1, userData = typeof(SoundPlay).FullName});

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
