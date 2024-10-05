using System.Collections.Generic;
using UnityEngine;

public class SetActiveNode : BehaviorTreeBaseNode
{
    [Header("•\Ž¦‚·‚é‚©‚Ç‚¤‚©")]
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
        _skinnedMeshRenderer = my.GetComponentsInChildren<SkinnedMeshRenderer>();
        _meshRenderer = my.GetComponentsInChildren<MeshRenderer>();

    }

    public override Result Evaluate()
    {
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
