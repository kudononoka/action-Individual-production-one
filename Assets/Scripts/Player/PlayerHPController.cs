﻿using System;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerHPController
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

    public float CurrntStValue => _stNow;
    public IObservable<int> MaxHpChanged => _maxHp;
    readonly ReactiveProperty<int> _maxHp = new();

    public IObservable<int> CurrentHpChanged => _currentHp;
    readonly ReactiveProperty<int> _currentHp = new();

    /// <summary>初期化</summary>
    /// <param name="hpMax">HP最大値</param>
    /// <param name="stMax">ST最大値</param>
    public void Init(int hpMax)
    {
        _hpMax = hpMax;
        _hpNow = _hpMax;
        _maxHp.Value = _hpMax;
        _currentHp.Value = _hpNow;
    }

    /// <summary>現在のHP値から減算</summary>
    /// <param name="value">引く値</param>
    /// <returns>０以下だったらFalseを返す</returns>
    public bool HPDown(int value)
    {
        _hpNow -= value;
        
        if (_hpNow <= 0)
        {
            _hpNow = 0;
            _currentHp.Value = 0;
            return false;
        }
        _currentHp.Value = _hpNow;
        return true;
    }
}
