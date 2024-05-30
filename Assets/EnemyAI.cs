using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    BehaviorTreeScriptableObject _tree;

    [SerializeField]
    GameObject _target;
    [SerializeField]
    GameObject _my;

    private void Start()
    {
        _tree.RootNodeData.Init(_target, _my);
        for(int i = 0; i < _tree.Nodes.Count; i++)
        {
            _tree.Nodes[i].Init(_target, _my);
        }
    }
    // Update is called once per frame
    void Update()
    {
        _tree.Evaluate();
    }
}
