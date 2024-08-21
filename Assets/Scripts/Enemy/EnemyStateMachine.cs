using UnityEngine;

[System.Serializable]
/// <summary>�G�l�~�[��p�X�e�[�g�}�V�[��</summary>
public class EnemyStateMachine : StateMachine
{
    StateType _currentStateType;

    public StateType CurrentState => _currentStateType;

    /// <summary>�G�l�~�[�̍s�����</summary>
    public enum StateType
    {
        /// <summary>�ʏ�</summary>
        Normal,
        /// <summary>�T��</summary>
        Search,
        /// <summary>�퓬</summary>
        Battle,
    }

    EnemyAI _enemyAI;

    public EnemyAI EnemyAI => _enemyAI;

    NormalState _normalState = new();

    BattleState _battleState = new();

    SearchState _searchState = new();

    /// <summary>�e�X�e�[�g�̐ݒ�</summary>
    public void StateSet()
    {
        _normalState.Set(this);
        _battleState.Set(this);
        _searchState.Set(this);
    }

    /// <summary>�X�e�[�g�̓o�^�Ə�����</summary>
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

    /// <summary> State�̕ύX</summary>
    /// <param name="stateId"></param>
    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }
}

/// <summary>�G�l�~�[��p�X�e�[�g�̊��N���X</summary>
public abstract class EnemyStateBase : StateMachine.StateBase
{
    protected EnemyStateMachine _enemyStateMachine = null;

    /// <summary>StateMacine���Z�b�g����֐�</summary>
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
