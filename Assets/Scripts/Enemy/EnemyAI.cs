using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamage, ISlow
{
    EnemyAnimatorControlle _animatorControlle = new();

    [Header("���s��������Idle�̂܂܂ɂ��邩")]
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

        //�X���[�����̓o�^
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        timeManager.SlowSystem.Add(this);
    }


    private void OnDestroy()
    {
        //�X���[�����̉���
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

        //�q�b�g�A�j���[�V�����Đ�
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.GetHit);

        AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackHit);

        _isAlive = _enemyHPController.HPDown(damage);
        //����ł�����
        if (!_isAlive)
        {
            //���񂾃A�j���[�V�����Đ�
            _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Die);
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Normal);
            GameManager.Instance.EnemyKill();
        }
    }

    /// <summary>Enemy�������Ԃ����悤�ȏ���(�`���[�g���A����������|����Ă����悤��)</summary>
    public void Resuscitation()
    {
        _enemyHPController.Init();
        _isAlive = true;
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Idle);
    }

    public void OnSlow(float slowSpeedRate)
    {
        //�A�j���[�V�����Đ����x�ύX
        _animatorControlle.SetAnimSpeed(slowSpeedRate);
    }

    public void OffSlow()
    {
        //�A�j���[�V�����Đ����x��ʏ�ɖ߂�
        _animatorControlle.SetAnimSpeed(1);
    }

    /// <summary>Animation��Event�ŌĂяo��(�U���O�̉��o)</summary>
    public void AttackSignPlay()
    {
        _enemyAttackControlle.AttackSign();
    }

    /// <summary>Animation��Event�ŌĂяo��(�U���̉��o)</summary>
    public void Attack()
    {
        _enemyAttackControlle.Attack();
    }

}
