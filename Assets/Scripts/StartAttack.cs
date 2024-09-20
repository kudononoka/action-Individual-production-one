using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAttack : MonoBehaviour
{
    [SerializeField]
    bool _isStartAttack;

    public bool IsStartAttack  => _isStartAttack;

    public void OnStartAttack(bool startAttack)
    {
        _isStartAttack = startAttack;
    }
}
