using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[Serializable]
public class EnemyStateMachine : StateMachine
{
    StateType _currentStateType;

    public StateType CurrentState => _currentStateType;

    [SerializeField]
    EnemyBattleState _enemyBattleState = new();

    EnemyIdle _enemyIdle = new();

    EnemyDeathState _enemyDeathState = new();

    public enum StateType
    {
        /// <summary>アイドル</summary>
        Idle,
        /// <summary>戦闘モード</summary>
        Battle,
        /// <summary>死</summary>
        Death,
    }

    EnemyAI _enemyAI;
    public EnemyAI EnemyAI => _enemyAI;

    /// <summary>各ステートの設定</summary>
    public void StateSet()
    {
        _enemyBattleState.Set(this);
        _enemyIdle.Set(this);
        _enemyDeathState.Set(this);
    }

    /// <summary>ステートの登録と初期化</summary>
    public void Init(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;

        StateAdd((int)StateType.Idle, _enemyIdle);
        StateAdd((int)StateType.Battle, _enemyBattleState);
        StateAdd((int)StateType.Death, _enemyDeathState);

        StateSet();

        foreach (var state in States)
        {
            state.Value.Init();
        }

        Initialize((int)StateType.Idle);
        //Initialize((int)StateType.Battle);
    }

    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }

}

public abstract class EnemyStateBase : StateMachine.StateBase
{
    protected EnemyStateMachine _enemyStateMachine = null;

    /// <summary>StateMacineをセットする関数</summary>
    /// <param name="stateMachine"></param>
    public void Set(EnemyStateMachine stateMachine)
    {
        _enemyStateMachine = stateMachine;
    }
    public override void Init() { }
    public override void OnEnd() { }
    public override void OnEnter() { }
    public override void OnFixedUpdate() { }
    public override void OnUpdate() { }
}
