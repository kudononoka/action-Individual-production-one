using UnityEngine;

[System.Serializable]
/// <summary>プレイヤー専用ステートマシーン</summary>
public class PlayerStateMachine : StateMachine
{
    StateType _currentStateType;

    
    IdleState _idleState = new();

    [SerializeField]
    WalkState _walkState = new();

    [SerializeField]
    EvadeState _evadeState = new();

    [SerializeField]
    AttackComboOneState _attackComboOneState = new();

    [SerializeField]
    AttackComboTwoState _attackComboTwoState = new();

    [SerializeField]
    AttackComboThreeState _attackComboThreeState = new();

    [SerializeField]
    AttackComboFourState _attackComboFourState = new();

    public StateType CurrentState => _currentStateType;

    /// <summary>プレイヤーの行動状態</summary>
    public enum StateType
    {
        /// <summary>アイドル</summary>
        Idle,
        /// <summary>歩行</summary>
        Walk,
        /// <summary>回避</summary>
        Evade,
        /// <summary>攻撃(コンボ1回目)</summary>
        AttackComboOne,
        /// <summary>攻撃(コンボ2回目)</summary>
        AttackComboTwo,
        /// <summary>攻撃(コンボ1回目)</summary>
        AttackComboThree,
        /// <summary>攻撃(コンボ2回目)</summary>
        AttackComboFour,
    }

    PlayerController _playeController;
    public PlayerController PlayerController => _playeController;

    /// <summary>各ステートの設定</summary>
    public void StateSet()
    {
        _idleState.Set(this);
        _walkState.Set(this);
        _evadeState.Set(this);
        _attackComboOneState.Set(this);
        _attackComboTwoState.Set(this);
        _attackComboThreeState.Set(this);
        _attackComboFourState.Set(this);
    }
    /// <summary>ステートの登録と初期化</summary>
    public void Init(PlayerController playerController)
    {
        _playeController = playerController;

        StateAdd((int)StateType.Walk, _walkState);
        StateAdd((int)StateType.Idle, _idleState);
        StateAdd((int)StateType.Evade, _evadeState);
        StateAdd((int)StateType.AttackComboOne, _attackComboOneState);
        StateAdd((int)StateType.AttackComboTwo, _attackComboTwoState);
        StateAdd((int)StateType.AttackComboThree, _attackComboThreeState);
        StateAdd((int)StateType.AttackComboFour, _attackComboFourState);

        StateSet();

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