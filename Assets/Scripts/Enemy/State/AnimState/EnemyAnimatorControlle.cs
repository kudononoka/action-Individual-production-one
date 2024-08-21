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
    }

    EnemyAnimationWalkState _walkState = new();

    EnemyAnimationIdleState _idleState = new();

    /// <summary>ステートの登録と初期化</summary>
    public void Init()
    {
        StateAdd((int)StateType.Idle, _idleState);
        StateAdd((int)StateType.Walk, _walkState);

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
}
