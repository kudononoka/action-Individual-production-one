using UnityEngine;

/// <summary>�U�����s��Node</summary>
[SerializeField]
public class MinorEnemyAttackNode : BehaviorTreeBaseNode
{
    float _coolTimer;

    [Header("�U���ɂ����鎞��")]
    [SerializeField]
    float _coolTime;

    Animator _anim;

    public MinorEnemyAttackNode()
    {
        nodeName = "minor enemy attack";
        nodeData = new NodeData(NodeType.ActionNode, typeof(MinorEnemyAttackNode).FullName);
    }

    public override void Init(GameObject target, GameObject my)
    {
        _anim = my.GetComponent<Animator>();
    }

    public override Result Evaluate()
    {
        if (_coolTimer == _coolTime)�@//Node�ɓ������u��
        {
            _anim.SetTrigger("Attack");     //�A�j���[�V�����ݒ�
        }

        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0) //�҂����Ԍo��
        {
            _coolTimer = _coolTime; �@�@�@�@�@�@�@//������
            return Result.Success;
        }

        return Result.Runnimg;
    }

}
