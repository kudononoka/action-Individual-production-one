using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController
{
    public EnemyAnimationController(Animator anim)
    {
        _anim = anim;
    }

    public enum AnimType
    {
        Idle,
        Walk,
        Attack
    }

    Animator _anim;
    
    public void AnimationChange(AnimType animType)
    {
        switch(animType)
        {
            case AnimType.Idle:
                _anim.SetBool("IsWalk", false);
                break;
            case AnimType.Walk:
                _anim.SetBool("IsWalk", true);
                break;
            case AnimType.Attack:
                _anim.SetTrigger("Attack");
                _anim.SetInteger("AttackPattern", 0);
                break;
        }
    }
}
