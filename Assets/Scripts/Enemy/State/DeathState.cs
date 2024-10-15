using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>死んだ状態</summary>
[Serializable]
public class DeathState : EnemyStateBase
{
    Animator _anim;

    EnemyAI _enemyAI;

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
        _anim.SetBool("IsDeath", true);
    }
}
