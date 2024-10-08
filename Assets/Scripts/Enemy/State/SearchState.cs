using System;
using UnityEngine;

[Serializable]
public class SearchState : EnemyStateBase
{
    EnemyAI _enemyAI;

    [SerializeField]
    TravelRouteSystem _routeSystem = new();

    [Tooltip("歩いてから立ち止まるまでの時間")]
    [SerializeField]
    float _patrolTime = 3;

    [Tooltip("立ち止まって周りを見渡す時間")]
    [SerializeField]
    float _lookAroundTime = 5;

    [Tooltip("敵とみなす物の位置")]
    [SerializeField]
    Transform _target;

    /// <summary>周りを見渡しているかどうか</summary>
    bool _isLookAround = false;

    float _timer = 0;

    /// <summary>視界</summary>
    SightController _sightController;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _sightController = _enemyAI.SightController;
        //探索の初期化
        _routeSystem.Init(_enemyAI.gameObject.transform, _enemyAI.MoveDestinationPoint);
        //探索経路準備
        _routeSystem.PreparingToMove();
        //一時停止
        _routeSystem.PatrolPause();
    }

    public override void OnEnter()
    {
        //探索開始
        _routeSystem.PatrolPlay();
        //アニメーション設定
        _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);

        _isLookAround = false;
        _timer = 0;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        //周りを見渡していたら
        if (_isLookAround)
        {
            //時間がたったら
            if (_timer > _lookAroundTime)
            {
                _timer = 0;

                //探索開始
                _routeSystem.PatrolPlay();
                //歩きアニメーションに変更
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);

                _isLookAround = false;
            }
        }
        //探索中
        else
        {
            //時間がたったら
            if (_timer > _patrolTime)
            {
                _timer = 0;

                //見渡すアニメーション変更
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.LookAround);
                //探索一時停止
                _routeSystem.PatrolPause();

                _isLookAround = true;
            }
        }

        //Target(Player)を見つけたら
        if (_sightController.isVisible(_target.position))
            //戦闘状態に入る
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);

    }

    public override void OnEnd()
    {
        //探索一時停止
        _routeSystem.PatrolPause();
    }
}
