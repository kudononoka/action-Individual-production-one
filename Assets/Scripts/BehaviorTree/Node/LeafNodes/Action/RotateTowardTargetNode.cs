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
        if(!_isDirection)                     //Œü‚­•ûŒü‚ğŒˆ‚ß‚é  
        {
            var vec = _target.transform.position - _my.transform.position;
            targetRotation = Quaternion.LookRotation(vec, Vector3.up);
            _isDirection = true;
        }

        //Target‚Ì•û‚É‰ñ“]
        _my.rotation = Quaternion.RotateTowards(_my.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        if (MathF.Abs(_my.rotation.y - targetRotation.y) < 2)          
        {
            _isDirection = false;
            return Result.Success;
        }
        Debug.Log("‰ñ“]");
        return Result.Runnimg;
    }
}
