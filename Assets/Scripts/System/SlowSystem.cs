using System;
using UnityEngine;

[Serializable]
public class SlowSystem
{
    [Header("�X���[���x�̊���")]
    [Tooltip("�X���[���x�̊���")]
    [SerializeField, Range(0, 1)] float _slowSpeedRate = 0.05f;

    /// <summary>�X���[�����ǂ���</summary>
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
