using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyAnimatorControlle _animatorControlle = new();

    public EnemyAnimatorControlle AnimatorControlle => _animatorControlle;

    EnemyStateMachine _enemyStateMachine = new();
    // Start is called before the first frame update
    void Start()
    {
        _animatorControlle.SetAnimator(GetComponent<Animator>());
        _animatorControlle.Init();

        _enemyStateMachine.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        _enemyStateMachine.OnUpdate();
    }
}
