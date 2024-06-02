using UnityEngine;

[System.Serializable]
/// <summary>�v���C���[��p�X�e�[�g�}�V�[��</summary>
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

    /// <summary>�v���C���[�̍s�����</summary>
    public enum StateType
    {
        /// <summary>�A�C�h��</summary>
        Idle,
        /// <summary>���s</summary>
        Walk,
        /// <summary>�K�[�h</summary>
        Guard,
        /// <summary>���</summary>
        Evade,
        /// <summary>��U��(�R���{1��ځE�R���{3���)</summary>
        AttackWeakPatternA,
        /// <summary>��U��(�R���{2���)</summary>
        AttackWeakPatternB,
        /// <summary>���U��(�R���{1���)</summary>
        AttackStrongPatternA,
        /// <summary>���U��(�R���{2���)</summary>
        AttackStrongPatternB,
    }

    PlayerController _playeController;
    public PlayerController PlayerController => _playeController;

    /// <summary>�e�X�e�[�g�̐ݒ�</summary>
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
    /// <summary>�X�e�[�g�̓o�^�Ə�����</summary>
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
/// <summary>�v���C���[��p�X�e�[�g�̊��N���X</summary>
public abstract class PlayerStateBase : StateMachine.StateBase
{
    protected PlayerStateMachine _playerStateMachine = null;

    /// <summary>StateMacine���Z�b�g����֐�</summary>
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