using System;
using UnityEngine;

[Serializable]
public class SearchState : EnemyStateBase
{
    EnemyAI _enemyAI;

    [SerializeField]
    TravelRouteSystem _routeSystem = new();

    [Tooltip("•à‚¢‚Ä‚©‚ç—§‚¿~‚Ü‚é‚Ü‚Å‚ÌŠÔ")]
    [SerializeField]
    float _patrolTime = 3;

    [Tooltip("—§‚¿~‚Ü‚Á‚Äü‚è‚ğŒ©“n‚·ŠÔ")]
    [SerializeField]
    float _lookAroundTime = 5;

    [Tooltip("“G‚Æ‚İ‚È‚·•¨‚ÌˆÊ’u")]
    [SerializeField]
    Transform _target;

    /// <summary>ü‚è‚ğŒ©“n‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©</summary>
    bool _isLookAround = false;

    float _timer = 0;

    /// <summary>‹ŠE</summary>
    SightController _sightController;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _sightController = _enemyAI.SightController;
        //’Tõ‚Ì‰Šú‰»
        _routeSystem.Init(_enemyAI.gameObject.transform, _enemyAI.MoveDestinationPoint);
        //’TõŒo˜H€”õ
        _routeSystem.PreparingToMove();
        //ˆê’â~
        _routeSystem.PatrolPause();
    }

    public override void OnEnter()
    {
        //’TõŠJn
        _routeSystem.PatrolPlay();
        //ƒAƒjƒ[ƒVƒ‡ƒ“İ’è
        _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);

        _isLookAround = false;
        _timer = 0;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        //ü‚è‚ğŒ©“n‚µ‚Ä‚¢‚½‚ç
        if (_isLookAround)
        {
            //ŠÔ‚ª‚½‚Á‚½‚ç
            if (_timer > _lookAroundTime)
            {
                _timer = 0;

                //’TõŠJn
                _routeSystem.PatrolPlay();
                //•à‚«ƒAƒjƒ[ƒVƒ‡ƒ“‚É•ÏX
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);

                _isLookAround = false;
            }
        }
        //’Tõ’†
        else
        {
            //ŠÔ‚ª‚½‚Á‚½‚ç
            if (_timer > _patrolTime)
            {
                _timer = 0;

                //Œ©“n‚·ƒAƒjƒ[ƒVƒ‡ƒ“•ÏX
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.LookAround);
                //’Tõˆê’â~
                _routeSystem.PatrolPause();

                _isLookAround = true;
            }
        }

        //Target(Player)‚ğŒ©‚Â‚¯‚½‚ç
        if (_sightController.isVisible(_target.position))
            //í“¬ó‘Ô‚É“ü‚é
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);

    }

    public override void OnEnd()
    {
        //’Tõˆê’â~
        _routeSystem.PatrolPause();
    }
}
