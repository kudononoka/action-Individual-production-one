using System;
using UnityEngine;

[Serializable]
public class SearchState : EnemyStateBase
{
    EnemyAI _enemyAI;

    [SerializeField]
    TravelRouteSystem _routeSystem = new();

    [Tooltip("�����Ă��痧���~�܂�܂ł̎���")]
    [SerializeField]
    float _patrolTime = 3;

    [Tooltip("�����~�܂��Ď�������n������")]
    [SerializeField]
    float _lookAroundTime = 5;

    [Tooltip("�G�Ƃ݂Ȃ����̈ʒu")]
    [SerializeField]
    Transform _target;

    /// <summary>��������n���Ă��邩�ǂ���</summary>
    bool _isLookAround = false;

    float _timer = 0;

    /// <summary>���E</summary>
    SightController _sightController;

    public override void Init()
    {
        _enemyAI = _enemyStateMachine.EnemyAI;
        _sightController = _enemyAI.SightController;
        //�T���̏�����
        _routeSystem.Init(_enemyAI.gameObject.transform, _enemyAI.MoveDestinationPoint);
        //�T���o�H����
        _routeSystem.PreparingToMove();
        //�ꎞ��~
        _routeSystem.PatrolPause();
    }

    public override void OnEnter()
    {
        //�T���J�n
        _routeSystem.PatrolPlay();
        //�A�j���[�V�����ݒ�
        _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);

        _isLookAround = false;
        _timer = 0;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        //��������n���Ă�����
        if (_isLookAround)
        {
            //���Ԃ���������
            if (_timer > _lookAroundTime)
            {
                _timer = 0;

                //�T���J�n
                _routeSystem.PatrolPlay();
                //�����A�j���[�V�����ɕύX
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.Walk);

                _isLookAround = false;
            }
        }
        //�T����
        else
        {
            //���Ԃ���������
            if (_timer > _patrolTime)
            {
                _timer = 0;

                //���n���A�j���[�V�����ύX
                _enemyAI.AnimatorControlle.OnChangeState((int)EnemyAnimatorControlle.StateType.LookAround);
                //�T���ꎞ��~
                _routeSystem.PatrolPause();

                _isLookAround = true;
            }
        }

        //Target(Player)����������
        if (_sightController.isVisible(_target.position))
            //�퓬��Ԃɓ���
            _enemyStateMachine.OnChangeState((int)EnemyStateMachine.StateType.Battle);

    }

    public override void OnEnd()
    {
        //�T���ꎞ��~
        _routeSystem.PatrolPause();
    }
}
