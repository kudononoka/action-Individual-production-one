using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

/// <summary>�A�j���\�^�[��p�̃X�e�[�g�}�V�[��</summary>
public abstract class AnimatorStateMachine
{
    /// <summary>�X�e�[�g�̊��N���X�E�e�X�e�[�g�͂��̃N���X���p������</summary>
    public abstract class StateBase
    {
        AnimatorStateMachine _owner;

        Animator _anim;

        protected AnimatorStateMachine Owner => _owner;

        protected Animator Animator => _anim;
        /// <summary>�X�e�[�g�}�V�[���̎Q�Ɛ�i�[</summary>
        public void Set(AnimatorStateMachine owner)
        {
            _owner = owner;
            _anim = owner._anim;
        }

        /// <summary>��ԍŏ��ɂP�񂾂��Ă΂��</summary>
        public abstract void Init();
        /// <summary>�X�e�[�g���؂�ւ탊��ŏ��ɂP��Ă΂��</summary>
        public abstract void OnEnter();
        /// <summary>���t���[���Ă΂��</summary>
        public abstract void OnEnd();

    }

    Animator _anim;
    protected Animator Animator => _anim;

    /// <summary>���݂�State</summary>
    private StateBase _currentState;

    /// <summary>�eState������</summary>
    private readonly Dictionary<int, StateBase> _states = new Dictionary<int, StateBase>();

    public Dictionary<int, StateBase> States => _states;

    public void SetAnimator(Animator anim)
    {
        _anim = anim;
    }

    /// <summary>�X�e�[�g�̓o�^</summary>
    /// <param name="state">�o�^�������X�e�[�g</param>
    public void StateAdd(int stateId, StateBase state)
    {
        //���łɓo�^���Ă����牽�����Ȃ�
        if (_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        state.Set(this);
        //�o�^
        _states.Add(stateId, state);
    }

    /// <summary>�ŏ��ɍs���X�e�[�g�̐ݒ�</summary>
    /// <param name="state">�X�e�[�g�̃^�C�v</param>
    public void Initialize(int stateId)
    {
        //�o�^���Ă��Ȃ�������G���[���o��
        if (!_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        //�ŏ��ɍs����X�e�[�g�Ƃ��Đݒ�
        _currentState = _states[stateId];
        CurrentChangeState(stateId);
        _currentState.OnEnter();
    }

    public abstract void CurrentChangeState(int stateId);

    /// <summary>�X�e�[�g�̐؂�ւ�</summary>
    /// <param name="state">�؂�ւ������X�e�[�g�̃^�C�v</param>
    public void OnChangeState(int stateId)
    {
        _currentState.OnEnd(); 

        CurrentChangeState(stateId);
        if (!_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }

        // �X�e�[�g��؂�ւ���
        _currentState = _states[stateId];
        _currentState.OnEnter();
    }
}
