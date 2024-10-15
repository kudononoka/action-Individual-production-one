using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DownState : EnemyStateBase
{
    [SerializeField]
    float _downTime = 10f;

    Animator _anim;

    EnemyAI _enemyAI;

    float _timer;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _anim = _enemyAI.Animator;
    }

    public override void OnEnter()
    {
        //MeshRendererは非表示の場合を考え表示する
        SkinnedMeshRenderer[] _skinnedMeshRenderer = _enemyAI.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] _meshRenderer = _enemyAI.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in _skinnedMeshRenderer)
        {
            renderer.enabled = true;
        }
        foreach (var renderer in _meshRenderer)
        {
            renderer.enabled = true;
        }

        //アニメーション設定
        _anim.SetBool("IsDown", true);
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        //時間経過したら
        if (_timer >= _downTime)
        {
            //戦闘モードに戻る
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);
        }
    }

    public override void OnEnd()
    {
        //初期化
        _timer = 0;
        //アニメーション設定
        _anim.SetBool("IsDown", false);
    }
}
