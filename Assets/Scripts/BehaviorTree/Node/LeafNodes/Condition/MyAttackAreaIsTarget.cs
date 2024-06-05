using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyAttackAreaIsTarget : BehaviorTreeBaseNode
{
    [Header("UŒ‚‚ª“–‚½‚é‹——£(Å‘å’l)")]
    [SerializeField] float _attackDistanceMax;

    [Header("UŒ‚‚ª“–‚½‚é‹——£(Å¬’l)")]
    [SerializeField] float _attackDistanceMin;

    Transform _target;
    Transform _my;
    public MyAttackAreaIsTarget()
    {
        nodeName = "my attack area is target";
        nodeData = new NodeData(NodeType.ConditionNode, typeof(MyAttackAreaIsTarget).FullName);
    }
    public override void Init(GameObject target, GameObject my)
    {
        _target = target.GetComponent<Transform>();
        _my = my.GetComponent<Transform>();
    }

    public override Result Evaluate()
    {
        float distance = Vector3.Distance(_target.position, _my.position);
        if (distance >= _attackDistanceMin && distance <= _attackDistanceMax)     //—Ìˆæ“à‚É‚¢‚½‚ç
        {
            return Result.Success;
        }

        return Result.Failure;
    }
}
