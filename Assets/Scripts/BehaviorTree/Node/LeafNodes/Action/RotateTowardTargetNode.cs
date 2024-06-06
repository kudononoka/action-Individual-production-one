using DG.Tweening;
using System;
using UnityEngine;

/// <summary>Targetの方に回転する</summary>
public class RotateTowardTargetNode : BehaviorTreeBaseNode
{
    [Header("回転スピード")]
    [SerializeField]
    float _rotateSpeed;

    Transform _my = null;
    Transform _target = null;

    /// <summary>むく方向が決まっているかどうか</summary>
    bool _isDirection = false;
    /// <summary>むく方向</summary>
    Quaternion targetRotation;

    bool _isComplete = false;
    public RotateTowardTargetNode()
    {
        nodeName = "rotate toward target";
        nodeData = new NodeData(NodeType.ActionNode, typeof(RotateTowardTargetNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _my = my.GetComponent<Transform>();
        _target = target.GetComponent<Transform>();
        _isDirection = false;
    }

    public override Result Evaluate()
    {
        //var dir = _target.transform.position - _my.transform.position;
        //dir.y = 0;

        //if (!_isDirection)                     //向く方向を決める  
        //{
        //    targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        //    _isDirection = true;
        //}

        ////Targetの方に回転
        //_my.rotation = Quaternion.RotateTowards(_my.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

        //float angle = Vector3.Angle(_my.transform.forward, dir);
        //if (angle < 5)          
        //{
        //    _isDirection = false;
        //    return Result.Success;
        //}
        if (!_isDirection)
        {
            var to = _target.transform.position - _my.transform.position;
            var from = _my.forward;
            float angle = Vector2.SignedAngle(new Vector2(from.x, from.z), new Vector2(to.x,to.z));
            if (angle < 0) angle += 360;
            Debug.Log(angle);
            _my.transform.DORotate(new Vector3(0f, angle, 0), 1, RotateMode.Fast).OnComplete(() => _isComplete = true);
            _isDirection = true;
        }

        if (_isComplete)
        {
            _isDirection = false;
            return Result.Success;
        }

        return Result.Runnimg;
    }
}
