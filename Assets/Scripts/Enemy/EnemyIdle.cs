using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyStateBase
{
    public override void OnUpdate()
    {
        if(GameManager.Instance.IsBattle)
        {
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);
        }
    }
}
