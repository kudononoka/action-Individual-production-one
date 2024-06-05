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
        if(!_isDirection)                     //向く方向を決める  
        {
            var vec = _target.transform.position - _my.transform.position;
            targetRotation = Quaternion.LookRotation(vec, Vector3.up);
            _isDirection = true;
        }

        //Targetの方に回転
        _my.rotation = Quaternion.RotateTowards(_my.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        if (MathF.Abs(_my.rotation.y - targetRotation.y) < 2)          
        {
            _isDirection = false;
            return Result.Success;
        }
        Debug.Log("回転");
        return Result.Runnimg;
    }
}
