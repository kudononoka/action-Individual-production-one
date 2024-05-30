using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnvironmentData : ScriptableObject
{
    GameObject _target = null;
    GameObject _my = null;

    public GameObject Target => _target;
    public GameObject My => _my;

    public void TargetSet(GameObject target)
    {
        _target = target;
    }

    public void MySet(GameObject my)
    {
        _my = my;
    }
}
