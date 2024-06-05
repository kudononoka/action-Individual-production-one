using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�g�}�V�[��
/// ���̃N���X�����ꍇ�X�e�[�g�̏�Ԃ�\��Enum���쐬����K�v������
/// </summary>
/// <typeparam name="StateType">�e�X�e�[�g�̏��(Type��Enum�̂�)</typeparam>
public abstract class StateMachine
{
    /// <summary>�X�e�[�g�̊��N���X�E�e�X�e�[�g�͂��̃N���X���p������</summary>
    public abstract class StateBase
    {
        /// <summary>��ԍŏ��ɂP�񂾂��Ă΂��</summary>
        public abstract void Init();
        /// <summary>�X�e�[�g���؂�ւ탊��ŏ��ɂP��Ă΂��</summary>
        public abstract void OnEnter();
        /// <summary>���t���[���Ă΂��</summary>
        public abstract void OnUpdate();
        /// <summary>FixedUpdate�ŌĂ΂��</summary>
        public abstract void OnFixedUpdate();
        /// <summary>�X�e�[�g���؂�ւ탊�O�ɂP��Ă΂��</summary>
        public abstract void OnEnd();
    }
    /// <summary>���݂�State</summary>
    private StateBase _currentState;

    /// <summary>�eState������</summary>
    private readonly Dictionary<int, StateBase> _states = new Dictionary<int, StateBase>();

    public Dictionary<int, StateBase> States => _states;

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
    /// <summary>���݂̃X�e�[�g�𖈃t���[���s��</summary>
    public void OnUpdate()
    {
        _currentState.OnUpdate();
        //Debug.Log(_currentState.ToString());
    }

    public void OnFixedUpdate()
    {
        _currentState.OnFixedUpdate();
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