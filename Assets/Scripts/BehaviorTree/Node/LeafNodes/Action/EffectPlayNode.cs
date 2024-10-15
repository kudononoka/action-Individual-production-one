using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>パーティクルの再生</summary>
public class EffectPlayNode : BehaviorTreeBaseNode
{
    [Header("Scene内の再生したいEffectObject")]
    [SerializeField] string _effectObjectName = "";

    ParticleSystem _particle;

    public EffectPlayNode()
    {
        nodeName = "effect play";
        nodeData = new NodeData(NodeType.ActionNode, typeof(EffectPlayNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _particle = my.transform.Find(_effectObjectName).GetComponent<ParticleSystem>();
    }

    public override Result Evaluate()
    {
        _particle.Play();

        return Result.Success;
    }
}
