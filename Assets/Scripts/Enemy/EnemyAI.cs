using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamage, ISlow
{
    EnemyAnimatorControlle _animatorControlle = new();

    [Header("実行中ずっとIdleのままにするか")]
    [SerializeField]
    bool _isIdle = false;

    [SerializeField]
    EnemyStateMachine _enemyStateMachine = new();

    [SerializeField]
    EnemyHPController _enemyHPController = new();

    [SerializeField]
    EnemyAttackControlle _enemyAttackControlle = new();

    MoveDestinationPoint _moveDestinationPoint;

    SightController _sightController;

    bool _isAlive = true;

    public MoveDestinationPoint MoveDestinationPoint => _moveDestinationPoint;

    public EnemyAnimatorControlle AnimatorControlle => _animatorControlle;

    public SightController SightController => _sightController;

    public bool IsAlive => _isAlive;    

    public bool IsIdle => _isIdle;  


    // Start is called before the first frame update
    void Start()
    {
        _isAlive = true;

        _moveDestinationPoint = GetComponent<MoveDestinationPoint>();

        _animatorControlle.SetAnimator(GetComponent<Animator>());
        _animatorControlle.Init();

        _sightController = GetComponent<SightController>();

        _enemyStateMachine.Init(this);

        _enemyHPController.Init();

        //スロー処理の登録
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        timeManager.SlowSystem.Add(this);
    }


    private void OnDestroy()
    {
        //スロー処理の解除
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
            timeManager.SlowSystem.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isIdle && IsAlive)
        {
            _enemyStateMachine.OnUpdate();
        }
    }


    public void Damage(int damage)
    {
        if (!_isAlive) return;

        //ヒットアニメーション再生
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.GetHit);

        AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackHit);

        _isAlive = _enemyHPController.HPDown(damage);
        //死んでいたら
        if (!_isAlive)
        {
            //死んだアニメーション再生
            _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Die);
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Normal);
            GameManager.Instance.EnemyKill();
        }
    }

    /// <summary>Enemyが生き返ったような処理(チュートリアル中何回も倒されていいように)</summary>
    public void Resuscitation()
    {
        _enemyHPController.Init();
        _isAlive = true;
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Idle);
    }

    public void OnSlow(float slowSpeedRate)
    {
        //アニメーション再生速度変更
        _animatorControlle.SetAnimSpeed(slowSpeedRate);
    }

    public void OffSlow()
    {
        //アニメーション再生速度を通常に戻す
        _animatorControlle.SetAnimSpeed(1);
    }

    /// <summary>AnimationのEventで呼び出す(攻撃前の演出)</summary>
    public void AttackSignPlay()
    {
        _enemyAttackControlle.AttackSign();
    }

    /// <summary>AnimationのEventで呼び出す(攻撃の演出)</summary>
    public void Attack()
    {
        _enemyAttackControlle.Attack();
    }

}
