using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Animator�̃p�����[�^�[�l��ύX����N���X</summary>
[SerializeField]
public class AnimationNode : BehaviorTreeBaseNode
{
    public AnimationNode()
    {
        nodeName = "animation";
        nodeData = new NodeData(NodeType.ActionNode, typeof(AnimationNode).FullName);
    }

    public enum AnimParaValueType
    {
        Bool,
        Trigger,
        Float,
        Int,
    }

    [Header("Animation�p�����[�^�[��")]
    [SerializeField] string _animPara = "";

    [Header("Animation�̃^�C�v")]
    [SerializeField] AnimParaValueType _animState = AnimParaValueType.Bool;

    [Header("Animation�p�����[�^�[�l")]

    [SerializeField] bool _isBoolValue = false;

    [SerializeField] float _floatValue = 0f;

    [SerializeField] int _intValue = 0;

    Animator _anim;

    public override void Init(GameObject target, GameObject my)
    {
        _anim = my.GetComponent<Animator>();
    }

    public override Result Evaluate()
    {
        if (_anim == null)
        {
            Debug.LogError("�Q�Ɛ�I�u�W�F�N�g��Animator���A�^�b�`����Ă��܂���");
        }

        switch (_animState)
        {
            case AnimParaValueType.Bool:
                _anim.SetBool(_animPara, _isBoolValue); 
                break;
            case AnimParaValueType.Trigger:
                _anim.SetTrigger(_animPara);
                break;
            case AnimParaValueType.Float:
                _anim.SetFloat(_animPara, _floatValue);
                break;
            case AnimParaValueType.Int:
                _anim.SetInteger(_animPara, _intValue);
                break;
        }

        return Result.Success;

    }
}
