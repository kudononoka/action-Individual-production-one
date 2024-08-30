using System;
using UnityEngine;

[Serializable]
public class SlowSystem
{
    [Header("スロー速度の割合")]
    [Tooltip("スロー速度の割合")]
    [SerializeField, Range(0, 1)] float _slowSpeedRate = 0.05f;

    /// <summary>スロー中かどうか</summary>
    bool _isSlowing = false;

    private event Action<float> OnSlowAction;

    private event Action OffSlowAction;

    public bool IsSlowing => _isSlowing;

    public void OnOffSlow(bool isOn)
    {
        _isSlowing = isOn;
        if (_isSlowing)
        {
            OnSlowAction.Invoke(_slowSpeedRate);
        }
        else
        {
            OffSlowAction.Invoke();
        }
    }

    
    public void Add(ISlow slow)
    {
        OnSlowAction += slow.OnSlow;
        OffSlowAction += slow.OffSlow;

        if(_isSlowing)
        {
            slow.OnSlow(_slowSpeedRate);
        }
    }

    public void Remove(ISlow slow)
    {
        OnSlowAction -= slow.OnSlow;
        OffSlowAction -= slow.OffSlow;
    }
}
