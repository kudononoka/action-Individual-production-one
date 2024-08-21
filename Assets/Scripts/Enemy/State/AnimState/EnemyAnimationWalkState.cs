public class EnemyAnimationIdleState : AnimatorStateMachine.StateBase
{
    public override void Init(){}
    public override void OnEnd(){}
    public override void OnEnter(){}
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

