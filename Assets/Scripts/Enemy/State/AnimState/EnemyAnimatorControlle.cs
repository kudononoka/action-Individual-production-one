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
    }

    EnemyAnimationWalkState _walkState = new();

    EnemyAnimationIdleState _idleState = new();

    /// <summary>�X�e�[�g�̓o�^�Ə�����</summary>
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

    /// <summary> State�̕ύX</summary>
    /// <param name="stateId"></param>
    public override void CurrentChangeState(int stateId)
    {
        _currentStateType = (StateType)stateId;
    }
}
