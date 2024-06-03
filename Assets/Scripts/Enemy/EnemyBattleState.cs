using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

[Serializable]
public class EnemyBattleState : EnemyStateBase
{
    [SerializeField]
    BehaviorTreeScriptableObject _tree;

    [SerializeField]
    GameObject _target = null;

    [SerializeField]
    GameObject _my = null;

    EnemyHPController _enemyHPSTController;

    public override void Init()
    {
        _enemyHPSTController = _enemyStateMachine.EnemyAI.EnemyHPSTController;
        _tree.RootNodeData.Init(_target, _my);

        for (int i = 0; i < _tree.Nodes.Count; i++)
        {
            _tree.Nodes[i].Init(_target, _my);
        }
        if (_tree.RootNodeData.NodeData.ChildData.Count == 0)
        {
            Debug.Log("Žq‹Ÿ‚ª‚¢‚È‚¢‚æ");
        }
    }

    public override void OnUpdate()
    {
        _tree.Evaluate();
        if(_enemyHPSTController.CurrentHPValue <= 0)
        {
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Death);
        }
        Debug.Log(Vector3.Distance(_target.transform.position, _my.transform.position));
    }
}
