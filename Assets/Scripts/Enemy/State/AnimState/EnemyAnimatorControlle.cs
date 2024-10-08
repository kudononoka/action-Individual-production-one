using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorControlle : AnimatorStateMachine
{
    StateType _currentStateType;
    public StateType CurrentState => _currentStateType;

    /// <summary>プレイヤーの行動状態</summary>
    public enum StateType
    {
        /// <summary>アイドル</summary>
        Idle,
        /// <summary>歩行</summary>
        Walk,
        /// <summary>攻撃</summary>
        Attack,
        /// <summary>見わたす</summary>
        LookAround,
        /// <summary>戦闘</summary>
        Battle,
        /// <summary>攻撃を受けた</summary>
        GetHit,
        /// <summary>死亡</summary>
        Die,
    }

    EnemyAnimationWalkState _walkState = new();

    EnemyAnimationIdleState _idleState = new();

    EnemyAnimationLookAroundState _lookAroundState = new();

    EnemyAnimationAttackState _attackState = new();

    EnemyAnimationBattleState _battleState = new();

    EnemyAnimationGetHitState _getHitState = new();

    EnemyAnimationDieState _dieState = new();

    /// <summary>ステートの登録と初期化</summary>
    public void Init()
    {
        StateAdd((int)StateType.Idle, _idleState);
        StateAdd((int)StateType.Walk, _walkState);
        StateAdd((int)StateType.Attack, _attackState);
        StateAdd((int)StateType.LookAround, _lookAroundState);
        StateAdd((int)StateType.Battle, _battleState);
        StateAdd((int)StateType.GetHit, _getHitState);
        StateAdd((int)StateType.Die, _dieState);

        foreach (var state in States)
        {
            state.Value.Init();
        }

        Initialize((int)StateType.Idle);
    }

    /// <summary> Stateの変更</summary>
    /// <param name="stateId"></param>
    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }

    public void SetAnimSpeed(float speed)
    {
        Animator.speed = speed;
    }
}
