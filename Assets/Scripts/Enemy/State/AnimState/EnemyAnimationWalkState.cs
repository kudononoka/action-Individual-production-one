using UnityEngine;

public class EnemyAnimationIdleState : AnimatorStateMachine.StateBase
{
    public override void Init(){}

    public override void OnEnter()
    {
        Animator.SetLayerWeight(1, 0);
    }

    public override void OnEnd(){}
}

public class EnemyAnimationWalkState : AnimatorStateMachine.StateBase
{
    public override void Init()
    {
        Animator.SetBool("IsWalk", false);
    }

    public override void OnEnter()
    {
        Animator.SetBool("IsWalk", true);
    }

    public override void OnEnd()
    {
        Animator.SetBool("IsWalk", false);
    }
}

public class EnemyAnimationLookAroundState : AnimatorStateMachine.StateBase
{
    public override void Init()
    {
        Animator.SetBool("IsLookAround", false);
    }

    public override void OnEnter()
    {
        Animator.SetBool("IsLookAround", true);
    }

    public override void OnEnd()
    {
        Animator.SetBool("IsLookAround", false);
    }
}

public class EnemyAnimationAttackState : AnimatorStateMachine.StateBase
{
    public override void Init(){}

    public override void OnEnter()
    {
        Animator.SetTrigger("Attack");
    }

    public override void OnEnd(){}
}

public class EnemyAnimationBattleState : AnimatorStateMachine.StateBase
{
    public override void Init()
    {
        Animator.SetLayerWeight(1, 0);
    }

    public override void OnEnter()
    {
        Debug.Log("laalal");
        Animator.SetTrigger("Discovery");
        Animator.SetLayerWeight(1, 1);
    }

    public override void OnEnd() {}
}

