using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamage, ISlow
{
    EnemyAnimatorControlle _animatorControlle = new();

    [SerializeField]
    EnemyStateMachine _enemyStateMachine = new();

    [SerializeField]
    EnemyHPController _enemyHPController = new();

    MoveDestinationPoint _moveDestinationPoint;

    SightController _sightController;

    bool _isAlive = true;

    public MoveDestinationPoint MoveDestinationPoint => _moveDestinationPoint;

    public EnemyAnimatorControlle AnimatorControlle => _animatorControlle;

    public SightController SightController => _sightController;

    public bool IsAlive => _isAlive;    


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

        TimeManager timeManager = FindObjectOfType<TimeManager>();
        timeManager.SlowSystem.Add(this);
    }


    private void OnDestroy()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
            timeManager.SlowSystem.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Damage(int damage)
    {
        //�q�b�g�A�j���[�V�����Đ�
        _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.GetHit);

        _isAlive = _enemyHPController.HPDown(damage);
        //����ł�����
        if (!_isAlive)
        {
            //���񂾃A�j���[�V�����Đ�
            _animatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Die);
        }
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
}
