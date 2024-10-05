using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayNode : BehaviorTreeBaseNode
{
    [Header("Effect���Đ�����I�u�W�F�N�g�̖��O")]
    [SerializeField] string _effectObjectName = "";

    ParticleSystem _particle;

    public EffectPlayNode()
    {
        nodeName = "effect play";
        nodeData = new NodeData(NodeType.ActionNode, typeof(EffectPlayNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _particle = my.transform.FindChild(_effectObjectName).GetComponent<ParticleSystem>();
    }

    public override Result Evaluate()
    {
        _particle.Play();

        return Result.Success;
    }
}
