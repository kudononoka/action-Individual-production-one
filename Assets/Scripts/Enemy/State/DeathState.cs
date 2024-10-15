using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeathState : EnemyStateBase
{
    [SerializeField]
    Animator _anim;

    EnemyAI _enemyAI;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
    }

    public override void OnEnter()
    {
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
        _anim.SetBool("IsDeath", true);
    }
}
