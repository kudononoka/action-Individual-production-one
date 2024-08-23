using System;
using UnityEngine;

[Serializable]
public class SearchState : EnemyStateBase
{
    EnemyAI _enemyAI;

    [SerializeField]
    TravelRouteSystem _routeSystem = new();

    [Tooltip("•à‚¢‚Ä‚©‚ç—§‚¿Ž~‚Ü‚é‚Ü‚Å‚ÌŽžŠÔ")]
    [SerializeField]
    float _patrolTime = 3;

    [Tooltip("—§‚¿Ž~‚Ü‚Á‚ÄŽü‚è‚ðŒ©“n‚·ŽžŠÔ")]
    [SerializeField]
    float _lookAroundTime = 5;

    [Tooltip("“G‚Æ‚Ý‚È‚·•¨‚ÌˆÊ’u")]
    [SerializeField]
    Transform _target;

    bool _isLookAround = false;

    float _timer = 0;

    SightController _sightController;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _sightController = _enemyAI.SightController;
        _routeSystem.Init(_enemyAI.gameObject.transform, _enemyAI.MoveDestinationPoint);
        _routeSystem.PreparingToMove();
        _routeSystem.PatrolPause();
    }

    public override void OnEnter()
    {
        _routeSystem.PatrolPlay();
        _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);
        _isLookAround = false;
        _timer = 0;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_isLookAround)
        {
            if (_timer > _lookAroundTime)
            {
                _timer = 0;
                _routeSystem.PatrolPlay();
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);
                _isLookAround = false;
            }
        }
        else
        {
            if (_timer > _patrolTime)
            {
                _timer = 0;
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.LookAround);
                _routeSystem.PatrolPause();
                _isLookAround = true;
            }
        }

        if (_sightController.isVisible(_target.position))
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);

    }

    public override void OnEnd()
    {
        _routeSystem.PatrolPause();
    }
}
