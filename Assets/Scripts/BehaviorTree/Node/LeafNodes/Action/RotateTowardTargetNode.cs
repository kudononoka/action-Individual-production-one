using System;
using UnityEngine;

/// <summary>Target�̕��ɉ�]����</summary>
public class RotateTowardTargetNode : BehaviorTreeBaseNode
{
    [Header("��]�X�s�[�h")]
    [SerializeField]
    float _rotateSpeed;

    Transform _my = null;
    Transform _target = null;

    /// <summary>�ނ����������܂��Ă��邩�ǂ���</summary>
    bool _isDirection = false;
    /// <summary>�ނ�����</summary>
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
        if(!_isDirection)                     //�������������߂�  
        {
            var vec = _target.transform.position - _my.transform.position;
            targetRotation = Quaternion.LookRotation(vec, Vector3.up);
            _isDirection = true;
        }

        //Target�̕��ɉ�]
        _my.rotation = Quaternion.RotateTowards(_my.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        if (MathF.Abs(_my.rotation.y - targetRotation.y) < 2)          
        {
            _isDirection = false;
            return Result.Success;
        }
        Debug.Log("��]");
        return Result.Runnimg;
    }
}
