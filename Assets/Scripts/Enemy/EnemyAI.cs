using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamage, ISlow
{
    EnemyAnimatorControlle _animatorControlle = new();

    [Header("実行中ずっとIdleのままにするか")]
    [SerializeField]
    bool _isIdle = false;

    [Header("最初のEnemy状態")]
    [SerializeField]
    EnemyStateMachine.StateType _startState;

    [SerializeField]
    EnemyStateMachine _enemyStateMachine = new();

    [SerializeField]
    EnemyHPController _enemyHPController = new();

    [SerializeField]
    EnemyAttackControlle _enemyAttackControlle = new();

    [SerializeField]
    int _downHp = 50;

    [SerializeField]
    bool _isDowned = false;

    [SerializeField]
    SE _soundSE = SE.EnemyAttack;

    MoveDestinationPoint _moveDestinationPoint;

    SightController _sightController;

    bool _isAlive = true;

    public MoveDestinationPoint MoveDestinationPoint => _moveDestinationPoint;

    public EnemyAnimatorControlle AnimatorControlle => _animatorControlle;

    public SightController SightController => _sightController;

    public EnemyHPController HPController => _enemyHPController;

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

        _enemyStateMachine.Init(this, _startState);

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

        _isAlive = _enemyHPController.HPDown(damage);

        //探索中だったら
        if(_enemyStateMachine.CurrentState == EnemyStateMachine.StateType.Search)
        {
            //戦闘状態に切り替える
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);
        }

        if (!_isAlive)
        {
            //死んだアニメーション再生
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Death);
            GameManager.Instance.GameEnd(GameState.GameClear);
        }

        if (!_isDowned && _enemyHPController.CurrentHPValue <= _downHp)
        {
            _isDowned = true;
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Down);
        }

    }

    /// <summary>チュートリアル用</summary>
    public void StateReset(EnemyStateMachine.StateType setState)
    {
        _enemyHPController.Init();
        _isAlive = true;
        _enemyStateMachine.OnChangeState((int)setState);
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

    public void AudioEvent()
    {
        AudioManager.Instance.SEPlayOneShot(_soundSE);
    }

}
