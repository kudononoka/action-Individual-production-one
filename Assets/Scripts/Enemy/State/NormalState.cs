using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : EnemyStateBase
{
    EnemyAI _enemyAI;

    public override void Init() 
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
    }

    public override void OnEnter()
    {
        if (!_enemyAI.IsIdle && _enemyAI.IsAlive)
        {
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Search);
        }
    }

    public override void OnUpdate()
    { 

    }
    public override void OnEnd()
    {

    }
}
