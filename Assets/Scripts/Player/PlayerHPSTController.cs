using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerHPSTController
{
    /// <summary>HP最大値</summary>
    int _hpMax;
    /// <summary>ST最大値</summary>
    float _stMax;
    [SerializeField]
    /// <summary>現在のHP</summary>
    int _hpNow;
    /// <summary>現在のSP</summary>
    float _stNow;
    /// <summary>STが回復する速度</summary>
    float _stRecoverySpeed;

    public float CurrntStValue => _stNow;
    public IObservable<int> MaxHpChanged => _maxHp;
    readonly ReactiveProperty<int> _maxHp = new();

    public IObservable<int> CurrentHpChanged => _currentHp;
    readonly ReactiveProperty<int> _currentHp = new();

    public IObservable<float> MaxStChanged => _maxSt;
    readonly ReactiveProperty<float> _maxSt = new();

    public IObservable<float> CurrentStChanged => _currentSt;
    readonly ReactiveProperty<float> _currentSt = new();

    /// <summary>初期化</summary>
    /// <param name="hpMax">HP最大値</param>
    /// <param name="stMax">ST最大値</param>
    public void Init(int hpMax, float stMax, float stRecoverySpeed)
    {
        _hpMax = hpMax;
        _stMax = stMax;
        _hpNow = hpMax;
        _stNow = stMax;
        _maxHp.Value = _hpMax;
        _currentHp.Value = _hpMax;
        _maxSt.Value = _stMax;
        _currentSt.Value = _stMax;
        _stRecoverySpeed = stRecoverySpeed;
    }

    /// <summary>現在のHP値から減算</summary>
    /// <param name="value">引く値</param>
    /// <returns>０以下だったらFalseを返す</returns>
    public bool HPDown(int value)
    {
        _hpNow -= value;
        _currentHp.Value = _hpNow;
        if (_hpNow <= 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>現在のST値から減算</summary>
    /// <param name="value">引く値</param>
    /// <returns>０以下だったらFalseを返す</returns>
    public bool STDown(float value)
    {
        _stNow -= value;
        _currentSt.Value = _stNow;
        if (_stNow <= 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>STの回復</summary>
    public void RecoveryST()
    {
        if (_stNow == _stMax) return;

        _stNow += Time.deltaTime * _stRecoverySpeed;
        _currentSt.Value = _stNow;

        if (_stNow > _stMax)
        {
            _stNow = _stMax;
            _currentSt.Value = _stNow;
        }
    }
}
