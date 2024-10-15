using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : BehaviorTreeBaseNode
{
    [Header("鳴らしたい音")]
    [SerializeField]
    SE _soundSe;

    public SoundPlay()
    {
        nodeName = "sound play";
        nodeData = new NodeData(NodeType.ActionNode, typeof(SoundPlay).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
    }

    public override Result Evaluate()
    {
        AudioManager.Instance.SEPlayOneShot(_soundSe);

        return Result.Success;
    }
}
