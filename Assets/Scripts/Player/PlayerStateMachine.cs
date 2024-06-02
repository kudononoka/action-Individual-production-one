using UnityEngine;

[System.Serializable]
/// <summary>プレイヤー専用ステートマシーン</summary>
public class PlayerStateMachine : StateMachine
{
    StateType _currentStateType;

    WalkState _walkState = new();
    IdleState _idleState = new();
    GuardState _guardState = new();

    [SerializeField]
    EvadeState _evadeState = new();

    [SerializeField]
    AttackWeakPatternAState _attackWeakPatternAState = new();

    [SerializeField]
    AttackWeakPatternBState _attackWeakPatternBState = new();

    [SerializeField]
    AttackStrongPatternAState _attackStrongPatternAState = new();

    [SerializeField]
    AttackStrongPatternBState _attackStrongPatternBState = new();

    public StateType CurrentState => _currentStateType;

    /// <summary>プレイヤーの行動状態</summary>
    public enum StateType
    {
        /// <summary>アイドル</summary>
        Idle,
        /// <summary>歩行</summary>
        Walk,
        /// <summary>ガード</summary>
        Guard,
        /// <summary>回避</summary>
        Evade,
        /// <summary>弱攻撃(コンボ1回目・コンボ3回目)</summary>
        AttackWeakPatternA,
        /// <summary>弱攻撃(コンボ2回目)</summary>
        AttackWeakPatternB,
        /// <summary>強攻撃(コンボ1回目)</summary>
        AttackStrongPatternA,
        /// <summary>強攻撃(コンボ2回目)</summary>
        AttackStrongPatternB,
    }

    PlayerController _playeController;
    public PlayerController PlayerController => _playeController;

    /// <summary>各ステートの設定</summary>
    public void StateSet()
    {
        _idleState.Set(this);
        _walkState.Set(this);
        _guardState.Set(this);
        _evadeState.Set(this);
        _attackWeakPatternAState.Set(this);
        _attackWeakPatternBState.Set(this);
        _attackStrongPatternAState.Set(this);
        _attackStrongPatternBState.Set(this);
    }
    /// <summary>ステートの登録と初期化</summary>
    public void Init(PlayerController playerController)
    {
        _playeController = playerController;

        StateAdd((int)StateType.Walk, _walkState);
        StateAdd((int)StateType.Idle, _idleState);
        StateAdd((int)StateType.Guard, _guardState);
        StateAdd((int)StateType.Evade, _evadeState);
        StateAdd((int)StateType.AttackWeakPatternA, _attackWeakPatternAState);
        StateAdd((int)StateType.AttackWeakPatternB, _attackWeakPatternBState);
        StateAdd((int)StateType.AttackStrongPatternA, _attackStrongPatternAState);
        StateAdd((int)StateType.AttackStrongPatternB, _attackStrongPatternBState);

        StateSet();

        foreach (var state in States)
        {
            state.Value.Init();
        }

        Initialize((int)StateType.Idle);
    }

    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }
}
/// <summary>プレイヤー専用ステートの基底クラス</summary>
public abstract class PlayerStateBase : StateMachine.StateBase
{
    protected PlayerStateMachine _playerStateMachine = null;

    /// <summary>StateMacineをセットする関数</summary>
    /// <param name="stateMachine"></param>
    public void Set(PlayerStateMachine stateMachine)
    {
        _playerStateMachine = stateMachine;
    }
    public override void Init() { }
    public override void OnEnd() { }
    public override void OnEnter() { }
    public override void OnFixedUpdate() { }
    public override void OnUpdate() { }
}