using DG.Tweening;
using System;
using UnityEngine;

/// <summary>Target‚Ì•û‚É‰ñ“]‚·‚é</summary>
public class RotateTowardTargetNode : BehaviorTreeBaseNode
{
    [Header("‰ñ“]ƒXƒs[ƒh")]
    [SerializeField]
    float _rotateSpeed;

    Transform _my = null;
    Transform _target = null;

    /// <summary>‚Ş‚­•ûŒü‚ªŒˆ‚Ü‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©</summary>
    bool _isDirection = false;
    /// <summary>‚Ş‚­•ûŒü</summary>
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

        //if (!_isDirection)                     //Œü‚­•ûŒü‚ğŒˆ‚ß‚é  
        //{
        //    targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        //    _isDirection = true;
        //}

        ////Target‚Ì•û‚É‰ñ“]
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
