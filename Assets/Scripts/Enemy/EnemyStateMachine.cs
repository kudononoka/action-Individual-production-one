using UnityEngine;

[System.Serializable]
/// <summary>エネミー専用ステートマシーン</summary>
public class EnemyStateMachine : StateMachine
{
    StateType _currentStateType;

    public StateType CurrentState => _currentStateType;

    /// <summary>エネミーの行動状態</summary>
    public enum StateType
    {
        /// <summary>通常</summary>
        Normal,
        /// <summary>探索</summary>
        Search,
        /// <summary>戦闘</summary>
        Battle,
    }

    EnemyAI _enemyAI;

    public EnemyAI EnemyAI => _enemyAI;

    NormalState _normalState = new();

    BattleState _battleState = new();

    SearchState _searchState = new();

    /// <summary>各ステートの設定</summary>
    public void StateSet()
    {
        _normalState.Set(this);
        _battleState.Set(this);
        _searchState.Set(this);
    }

    /// <summary>ステートの登録と初期化</summary>
    public void Init(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;

        StateAdd((int)StateType.Normal, _normalState);
        StateAdd((int)StateType.Battle, _battleState);
        StateAdd((int)StateType.Search, _searchState);
        
        StateSet();

        foreach (var state in States)
        {
            state.Value.Init();
        }

        Initialize((int)StateType.Normal);
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
