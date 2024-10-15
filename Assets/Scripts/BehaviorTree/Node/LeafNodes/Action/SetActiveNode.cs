using System.Collections.Generic;
using UnityEngine;

/// <summary>オブジェクトのMeshRendererコンポーネントの表示非表示で疑似的にないように錯覚させる</summary>

public class SetActiveNode : BehaviorTreeBaseNode
{
    [Header("表示するかどうか")]
    [SerializeField] bool _isActive = false;

    SkinnedMeshRenderer[] _skinnedMeshRenderer; 
    MeshRenderer[] _meshRenderer;

    public SetActiveNode()
    {
        nodeName = "set active";
        nodeData = new NodeData(NodeType.ActionNode, typeof(SetActiveNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        //自分のMeshRendererコンポーネントを全て取得
        _skinnedMeshRenderer = my.GetComponentsInChildren<SkinnedMeshRenderer>();
        _meshRenderer = my.GetComponentsInChildren<MeshRenderer>();

    }

    public override Result Evaluate()
    {
        //表示・非表示
        foreach (var renderer in _skinnedMeshRenderer)
        {
            renderer.enabled = _isActive;
        }
        foreach (var renderer in _meshRenderer)
        {
            renderer.enabled = _isActive;
        }

        return Result.Success;
    }
}
