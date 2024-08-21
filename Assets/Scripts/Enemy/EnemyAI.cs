using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyAnimatorControlle _animatorControlle = new();

    public EnemyAnimatorControlle AnimatorControlle => _animatorControlle;

    [SerializeField]
    EnemyStateMachine _enemyStateMachine = new();

    MoveDestinationPoint _moveDestinationPoint;

    public MoveDestinationPoint MoveDestinationPoint => _moveDestinationPoint;

    // Start is called before the first frame update
    void Start()
    {
        _moveDestinationPoint = GetComponent<MoveDestinationPoint>();

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
