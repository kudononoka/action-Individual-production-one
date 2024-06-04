using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamage
{
    [SerializeField]
    EnemyStateMachine _stateMachine = new();

    [SerializeField]
    Animator _enemyAnimator;

    [SerializeField]
    EnemyParameter _parameter;

    [SerializeField]
    Weapon _weapon;

    EnemyAnimationController _animController;

    [SerializeField]
    EnemyHPController _enemyHPSTController = new();

    public EnemyAnimationController AnimController => _animController;

    public Animator EnemyAnimator => _enemyAnimator;

    public EnemyHPController EnemyHPSTController => _enemyHPSTController;

    public EnemyParameter Parameter => _parameter;

    public Weapon Weapon => _weapon;


    private void Start()
    {
        _stateMachine.Init(this);
        _enemyHPSTController.Init(_parameter.HPMax);
        _weapon.DamageColliderEnabledSet(false);
    }
    // Update is called once per frame
    void Update()
    {
        _stateMachine.OnUpdate();
    }

    public void Damage(int damage)
    {
        _enemyHPSTController.HPDown(damage);
    }

}
