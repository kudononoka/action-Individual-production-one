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
        _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);
    }

    public override void OnUpdate()
    { 

    }
    public override void OnEnd()
    {

    }
}
