using UnityEngine;

/// <summary>攻撃を行うNode</summary>
[SerializeField]
public class MinorEnemyAttackNode : BehaviorTreeBaseNode
{
    /// <summary>攻撃終了までにかかる時間</summary>
    float _coolTimer;

    [Header("攻撃にかかる時間")]
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
        if (_coolTimer == _coolTime)　//Nodeに入った瞬間
        {
            _anim.SetTrigger("Attack");     //アニメーション設定
        }

        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0) //待ち時間経過
        {
            _coolTimer = _coolTime; 　　　　　　　//初期化
            return Result.Success;
        }

        return Result.Runnimg;
    }

}
