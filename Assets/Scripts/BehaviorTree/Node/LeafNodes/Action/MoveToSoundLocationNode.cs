using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>音が聞こえた場所に向かう専用ノード</summary>
[SerializeField]
public class MoveToSoundLocationNode : BehaviorTreeBaseNode
{
    [Header("移動速度")]
    [SerializeField]
    float _moveSpeed;

    [Header("移動をやめる時の目的地との距離")]
    [SerializeField]
    float _stopDistance;

    /// <summary>動かしたいオブジェクト</summary>
    NavMeshAgent _agent = null;

    /// <summary>聞こえた場所</summary>
    Vector3 _soundLocation = Vector3.zero;

    /// <summary>動かしたいオブジェクトの位置</summary>
    Transform _my = null;

    Animator _anim;

    AudibilityController _audibilityController;

    Transform _target;

    SightController _sightController;

    public MoveToSoundLocationNode()
    {
        nodeName = "move to sound location";
        nodeData = new NodeData(NodeType.ActionNode, typeof(MoveToSoundLocationNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _audibilityController = my.GetComponent<AudibilityController>();
        _my = my.GetComponent<Transform>();
        _agent = my.GetComponent<NavMeshAgent>();
        _anim = my.GetComponent<Animator>();
        _sightController = my.GetComponent<SightController>();
        _target = target.GetComponent<Transform>();
    }
    public override Result Evaluate()
    {
        if (_soundLocation == Vector3.zero)
        {
            _soundLocation = _audibilityController.SoundLocation;
        }
        _agent.speed = _moveSpeed;

        _agent.SetDestination(_soundLocation);　//聞こえた場所まで移動

        _anim.SetBool("IsWalk", true);    //アニメーション設定

        //聞こえた場所に追いついたら成功を返す
        if (Vector3.Distance(_soundLocation, _my.position) <= _stopDistance)
        {
            _soundLocation = Vector3.zero;
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        //攻撃対象が視界に入ったら失敗を返し,音が聞こえた場所に行くのを中断する
        if (_sightController.isVisible(_target.position))
        {
            _soundLocation = Vector3.zero;
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Failure;
        }
        
        return Result.Runnimg;
    }
}
