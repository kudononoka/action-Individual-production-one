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
        //Ž€‚ñ‚Å‚¢‚È‚©‚Á‚½‚ç
        if (!_isDeath)
        {
            _tree.Evaluate();
        }
    }

    public void Damage(int damage)
    {
        //Ž€‚ñ‚¾‚ç
        if (!_hpController.HPDown(damage))
        {
            _isDeath = true;
            GetComponent<Animator>().SetBool("IsDeath", true);
        }
    }
}
