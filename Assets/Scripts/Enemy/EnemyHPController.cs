using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VRM;

[Serializable]
public class EnemyHPController
{
    /// <summary>HP最大値</summary>
    int _hpMax;
    /// <summary>現在のHP</summary>
    int _hpNow;

    public int CurrentHPValue => _hpNow;

    public int HpMax => _hpMax;

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
        _hpNow = hpMax;
        _maxHp.Value = _hpMax;
        _currentHp.Value = _hpMax;
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
}
