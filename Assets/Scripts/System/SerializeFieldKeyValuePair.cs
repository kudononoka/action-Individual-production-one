using System;
using UnityEngine;


[Serializable]
public struct KeyValuePair<TKey, TValue>
{
    [SerializeField] TKey _key;
    [SerializeField] TValue _value;

    public TKey Key => _key;
    public TValue Value => _value;
}
