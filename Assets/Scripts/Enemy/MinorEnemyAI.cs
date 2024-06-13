using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorEnemyAI : MonoBehaviour, IDamage
{
    [SerializeField]
    BehaviorTreeScriptableObject _tree;

    [SerializeField]
    GameObject _target = null;

    [SerializeField]
    EnemyHPController _hpController;

    bool _isDeath = false;

    public bool IsDeath => _isDeath;
    // Start is called before the first frame update
    void Start()
    {
        _tree.RootNodeData.Init(_target, this.gameObject);

        for (int i = 0; i < _tree.Nodes.Count; i++)
        {
            _tree.Nodes[i].Init(_target, this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //死んでいなかったら
        if (!_isDeath)
        {
            _tree.Evaluate();
        }
    }


    public void Damage(int damage)
    {
        //死んだら
        if (!_hpController.HPDown(damage))
        {
            _isDeath = true;
            GetComponent<Animator>().SetBool("IsDeath", true);
            GetComponent<BoxCollider>().enabled = false;
            GameManager.Instance.EnemyKill();
        }
    }
}
