using UnityEngine;

public class EnemyDeathState : EnemyStateBase
{
    Animator _anim;
    public override void Init()
    {
        _anim = _enemyStateMachine.EnemyAI.EnemyAnimator;
    }

    public override void OnEnter()
    {
        _anim.SetBool("IsDeath", true);
    }

    public override void OnEnd()
    {
        _anim.SetBool("IsDeath", false);
    }
}
