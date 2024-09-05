using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BattleState : EnemyStateBase
{
    EnemyAI _enemyAI;

    BehaviorTreeScriptableObject _tree;

    [SerializeField]
    BehaviorTreeScriptableObject _origin;

    [SerializeField]
    GameObject _target = null;

    [SerializeField]
    GameObject _my = null;

    EnemyAnimatorControlle _animatorControlle;

    [SerializeField]
    NavMeshAgent _agent;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _animatorControlle = _enemyAI.AnimatorControlle;

        _tree = _origin.Instance();

        _tree.RootNodeData.Init(_target, _my);

        for (int i = 0; i < _tree.Nodes.Count; i++)
        {
            _tree.Nodes[i].Init(_target, _my);
        }
    }

    public override void OnEnter()
    {
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Battle);
    }

    public override void OnUpdate()
    {
        _tree.Evaluate();

        if(!_enemyAI.IsAlive)
        {
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Normal);
        }
    }
    public override void OnEnd()
    {
        _agent.SetDestination(_my.transform.position);
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Die);
    }
}
