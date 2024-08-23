using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorControlle : AnimatorStateMachine
{
    StateType _currentStateType;
    public StateType CurrentState => _currentStateType;

    /// <summary>�v���C���[�̍s�����</summary>
    public enum StateType
    {
        /// <summary>�A�C�h��</summary>
        Idle,
        /// <summary>���s</summary>
        Walk,
        /// <summary>�U��</summary>
        Attack,
        /// <summary>���킽��</summary>
        LookAround,
        /// <summary>�퓬</summary>
        Battle,
    }

    EnemyAnimationWalkState _walkState = new();

    EnemyAnimationIdleState _idleState = new();

    EnemyAnimationLookAroundState _lookAroundState = new();

    EnemyAnimationAttackState _attackState = new();

    EnemyAnimationBattleState _battleState = new();

    /// <summary>�X�e�[�g�̓o�^�Ə�����</summary>
    public void Init()
    {
        StateAdd((int)StateType.Idle, _idleState);
        StateAdd((int)StateType.Walk, _walkState);
        StateAdd((int)StateType.Attack, _attackState);
        StateAdd((int)StateType.LookAround, _lookAroundState);
        StateAdd((int)StateType.Battle, _battleState);

        foreach (var state in States)
        {
            state.Value.Init();
        }

        Initialize((int)StateType.Idle);
    }

    /// <summary> State�̕ύX</summary>
    /// <param name="stateId"></param>
    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }
}
