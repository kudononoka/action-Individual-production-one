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

    [SerializeField]
    SightController _sightController;

    public EnemyAnimationController AnimController => _animController;

    public Animator EnemyAnimator => _enemyAnimator;

    public EnemyHPController EnemyHPSTController => _enemyHPSTController;

    public EnemyParameter Parameter => _parameter;

    public Weapon Weapon => _weapon;

    public SightController SightController => _sightController;


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
        if(!_enemyHPSTController.HPDown(damage))
        {
            GameManager.Instance.ChangeScene(SceneState.GameClear);
        }
    }

}
