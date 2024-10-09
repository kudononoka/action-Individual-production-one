using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DownState : EnemyStateBase
{
    [SerializeField]
    float _downTime = 10f;

    [SerializeField]
    Animator _anim;

    EnemyAI _enemyAI;

    float _timer;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
    }

    public override void OnEnter()
    {
        _anim.SetBool("IsDown", true);
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _downTime)
        {
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);
        }
    }

    public override void OnEnd()
    {
        _timer = 0;
        _anim.SetBool("IsDown", false);
    }
}
