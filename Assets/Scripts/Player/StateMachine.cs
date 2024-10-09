using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートマシーン
/// このクラスを持つ場合ステートの状態を表すEnumを作成する必要がある
/// </summary>
/// <typeparam name="StateType">各ステートの状態(TypeはEnumのみ)</typeparam>
public abstract class StateMachine
{
    /// <summary>ステートの基底クラス・各ステートはこのクラスを継承する</summary>
    public abstract class StateBase
    {
        /// <summary>一番最初に１回だけ呼ばれる</summary>
        public abstract void Init();
        /// <summary>ステートが切り替わリ後最初に１回呼ばれる</summary>
        public abstract void OnEnter();
        /// <summary>毎フレーム呼ばれる</summary>
        public abstract void OnUpdate();
        /// <summary>FixedUpdateで呼ばれる</summary>
        public abstract void OnFixedUpdate();
        /// <summary>ステートが切り替わリ前に１回呼ばれる</summary>
        public abstract void OnEnd();
    }
    /// <summary>現在のState</summary>
    private StateBase _currentState;

    /// <summary>各Stateをもつ</summary>
    private readonly Dictionary<int, StateBase> _states = new Dictionary<int, StateBase>();

    public Dictionary<int, StateBase> States => _states;

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
        //登録
        _states.Add(stateId, state);
    }

    /// <summary>最初に行うステートの設定</summary>
    /// <param name="state">ステートのタイプ</param>
    protected void Initialize(int stateId)
    {
        //登録していなかったらエラーを出す
        if (!_states.ContainsKey(stateId))
        {
            Debug.Log("not set state! : " + stateId);
            return;
        }
        //最初に行われるステートとして設定
        _currentState = _states[stateId];
        CurrentChangeState(stateId);
        _currentState.OnEnter();
    }
    /// <summary>現在のステートを毎フレーム行う</summary>
    public void OnUpdate()
    {
        _currentState?.OnUpdate();
        //Debug.Log(_currentState.ToString());
    }

    public void OnFixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public abstract void CurrentChangeState(int stateId);

    /// <summary>ステートの切り替え</summary>
    /// <param name="state">切り替えたいステートのタイプ</param>
    public void OnChangeState(int stateId)
    {
        if (!_states.ContainsKey(stateId))
        {
            Debug.Log("not set state! : " + stateId);
            return;
        }
        _currentState.OnEnd();
        CurrentChangeState(stateId);
        // ステートを切り替える
        _currentState = _states[stateId];
        _currentState.OnEnter();
    }
}