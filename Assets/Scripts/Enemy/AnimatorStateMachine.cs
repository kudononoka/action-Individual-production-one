using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

/// <summary>アニメ―ター専用のステートマシーン</summary>
public abstract class AnimatorStateMachine
{
    /// <summary>ステートの基底クラス・各ステートはこのクラスを継承する</summary>
    public abstract class StateBase
    {
        AnimatorStateMachine _owner;

        Animator _anim;

        protected AnimatorStateMachine Owner => _owner;

        protected Animator Animator => _anim;
        /// <summary>ステートマシーンの参照先格納</summary>
        public void Set(AnimatorStateMachine owner)
        {
            _owner = owner;
            _anim = owner._anim;
        }

        /// <summary>一番最初に１回だけ呼ばれる</summary>
        public abstract void Init();
        /// <summary>ステートが切り替わリ後最初に１回呼ばれる</summary>
        public abstract void OnEnter();
        /// <summary>毎フレーム呼ばれる</summary>
        public abstract void OnEnd();

    }

    Animator _anim;
    protected Animator Animator => _anim;

    /// <summary>現在のState</summary>
    private StateBase _currentState;

    /// <summary>各Stateをもつ</summary>
    private readonly Dictionary<int, StateBase> _states = new Dictionary<int, StateBase>();

    public Dictionary<int, StateBase> States => _states;

    public void SetAnimator(Animator anim)
    {
        _anim = anim;
    }

    /// <summary>ステートの登録</summary>
    /// <param name="state">登録したいステート</param>
    public void StateAdd(int stateId, StateBase state)
    {
        //すでに登録していたら何もしない
        if (_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        state.Set(this);
        //登録
        _states.Add(stateId, state);
    }

    /// <summary>最初に行うステートの設定</summary>
    /// <param name="state">ステートのタイプ</param>
    public void Initialize(int stateId)
    {
        //登録していなかったらエラーを出す
        if (!_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        //最初に行われるステートとして設定
        _currentState = _states[stateId];
        CurrentChangeState(stateId);
        _currentState.OnEnter();
    }

    public abstract void CurrentChangeState(int stateId);

    /// <summary>ステートの切り替え</summary>
    /// <param name="state">切り替えたいステートのタイプ</param>
    public void OnChangeState(int stateId)
    {
        _currentState.OnEnd(); 

        CurrentChangeState(stateId);
        if (!_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }

        // ステートを切り替える
        _currentState = _states[stateId];
        _currentState.OnEnter();
    }
}
