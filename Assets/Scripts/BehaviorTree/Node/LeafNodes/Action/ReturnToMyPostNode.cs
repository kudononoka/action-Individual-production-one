using UnityEngine;
using UnityEngine.AI;

/// <summary>自分の持ち場に戻るNode</summary>
public class ReturnToMyPostNode : BehaviorTreeBaseNode
{
    [Header("移動速度")]
    [SerializeField]
    float _moveSpeed;

    /// <summary>動かしたいオブジェクト</summary>
    NavMeshAgent _agent = null;
    /// <summary>自分の持ち場</summary>
    Vector3 _point = Vector3.zero;
    /// <summary>動かしたいオブジェクトの位置</summary>
    Transform _my = null;

    Animator _anim;

    GameObject _target;

    AudibilityController _audibilityController;


    SightController _sightController;

    public ReturnToMyPostNode()
    {
        nodeName = "return to my post";
        nodeData = new NodeData(NodeType.ActionNode, typeof(ReturnToMyPostNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        //親オブジェクトに持ち場を設定してある事が前提
        _point = my.transform.parent.position;
        _my = my.GetComponent<Transform>();
        _agent = my.GetComponent<NavMeshAgent>();
        _anim = my.GetComponent<Animator>();
        _agent.speed = _moveSpeed;
        _audibilityController = my.GetComponent<AudibilityController>();
        _sightController = my.GetComponent<SightController>();
        _target = target;
    }

    public override Result Evaluate()
    {
        _agent.speed = _moveSpeed;

        _agent.SetDestination(_point);　//Targetまで移動

        _anim.SetBool("IsWalk", true);    //アニメーション設定

        //目的地についたら成功を返す
        if (Vector3.Distance(_point, _my.position) <= 1)
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Success;
        }

        //音が聞こえたら戻るのを中断
        if (_audibilityController.IsAudible(_target))
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Failure;
        }

        //攻撃対象を見つけたら戻るのを中断
        if (_sightController.isVisible(_target.transform.position))
        {
            _agent.SetDestination(_my.position);
            _anim.SetBool("IsWalk", false);
            return Result.Failure;
        }

        return Result.Runnimg;
    }

}
