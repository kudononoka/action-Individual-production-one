using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>エネミー専用ステートマシーン</summary>
public class EnemyStateMachine : StateMachine
{
    [Header("登録したいState")]
    [SerializeField] StateType _insertState;

    StateType _currentStateType;

    public StateType CurrentState => _currentStateType;

    /// <summary>エネミーの行動状態</summary>
    [Serializable]
    [Flags]
    public enum StateType
    {
        /// <summary>通常</summary>
        Idle = 1 << 0,
        /// <summary>探索</summary>
        Search = 1 << 1,
        /// <summary>戦闘</summary>
        Battle = 1 << 2,
        /// <summary>ダウン</summary>
        Down = 1 << 3,
    }

    EnemyAI _enemyAI;

    public EnemyAI EnemyAI => _enemyAI;

    NormalState _normalState = new();

    [SerializeField]
    BattleState _battleState = new();

    [SerializeField]
    SearchState _searchState = new();

    [SerializeField]
    DownState _downState = new();

    List<EnemyStateBase> _states = new List<EnemyStateBase>();

    /// <summary>ステートの登録と初期化</summary>
    public void Init(EnemyAI enemyAI, StateType startState)
    {
        _enemyAI = enemyAI;

        _states.Add(_normalState);
        _states.Add(_searchState);
        _states.Add(_battleState);
        _states.Add(_downState);

        int states = (int)_insertState;

        for (int i = 0; i < 4; i++)
        {
            int flag = states & 1;
            if (flag == 1)
            {
                int id = 1;
                for (int j = 0; j < i; j++)
                {
                    id *= 2;
                }
                _states[i].Set(this);
                _states[i].Init();
                StateAdd(id, _states[i]);
            }
            states = states >> 1;
        }

        Initialize((int)startState);
    }

    /// <summary> Stateの変更</summary>
    /// <param name="stateId"></param>
    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }
}

/// <summary>エネミー専用ステートの基底クラス</summary>
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
