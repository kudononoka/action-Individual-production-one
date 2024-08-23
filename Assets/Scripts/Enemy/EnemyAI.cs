using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyAnimatorControlle _animatorControlle = new();

    [SerializeField]
    EnemyStateMachine _enemyStateMachine = new();

    MoveDestinationPoint _moveDestinationPoint;

    SightController _sightController;

    public MoveDestinationPoint MoveDestinationPoint => _moveDestinationPoint;

    public EnemyAnimatorControlle AnimatorControlle => _animatorControlle;

    public SightController SightController => _sightController;

    // Start is called before the first frame update
    void Start()
    {
        _moveDestinationPoint = GetComponent<MoveDestinationPoint>();

        _animatorControlle.SetAnimator(GetComponent<Animator>());
        _animatorControlle.Init();

        _sightController = GetComponent<SightController>();

        _enemyStateMachine.Init(this);

        
    }

    // Update is called once per frame
    void Update()
    {
        _enemyStateMachine.OnUpdate();
    }
}
