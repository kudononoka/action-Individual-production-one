using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : BehaviorTreeBaseNode
{
    EnemyAnimatorControlle _animControlle;

    [Header("çUåÇéûä‘")]
    [SerializeField]
    float _attackTime = 0;

    float _timer = 0;

    bool _isAttacked = false;

    public AttackNode()
    {
        nodeName = "attack";
        nodeData = new NodeData(NodeType.ActionNode, typeof(AttackNode).FullName);
    }
    public override void Init(GameObject target, GameObject my)
    {
        _animControlle = my.GetComponent<EnemyAI>().AnimatorControlle;
        _isAttacked = false;

    }
    public override Result Evaluate()
    {
        _timer += Time.deltaTime;

        if (!_isAttacked)
        {
            _animControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Attack);
            _isAttacked = true;
        }

        if (_timer >= _attackTime) 
        {
            _timer = 0; //èâä˙âª
            _isAttacked = false;
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
