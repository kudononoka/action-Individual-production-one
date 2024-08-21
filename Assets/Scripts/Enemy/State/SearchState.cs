using System;
using UnityEngine;

[Serializable]
public class SearchState : EnemyStateBase
{
    EnemyAI _enemyAI;

    [SerializeField]
    TravelRouteSystem _routeSystem = new();

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _routeSystem.Init(_enemyAI.gameObject.transform, _enemyAI.MoveDestinationPoint);
        _routeSystem.PreparingToMove();
    }

    public override void OnEnter()
    {
        _routeSystem.PatrolPlay();
        _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);
    }

    public override void OnUpdate() { }

    public override void OnEnd()
    {
        _routeSystem.PatrolPause();
    }
}
