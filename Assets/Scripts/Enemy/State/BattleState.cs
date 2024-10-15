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

    [SerializeField]
    NavMeshAgent _agent;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;

        //BehaviorTreeの初期化
        _tree = _origin.Instance();

        _tree.RootNodeData.Init(_target, _my);

        for (int i = 0; i < _tree.Nodes.Count; i++)
        {
            _tree.Nodes[i].Init(_target, _my);
        }
    }

    public override void OnEnter()
    {
        //追従を開始
        _agent.isStopped = false;
    }

    public override void OnUpdate()
    {
        _tree.Evaluate();
    }

    public override void OnEnd()
    {
        //追従をやめる
        _agent.SetDestination(_my.transform.position);
        _agent.isStopped = true;
    }
}
