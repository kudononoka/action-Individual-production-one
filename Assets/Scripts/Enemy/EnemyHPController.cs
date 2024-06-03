using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VRM;

[Serializable]
public class EnemyHPController
{
    /// <summary>HP�ő�l</summary>
    int _hpMax;
    /// <summary>���݂�HP</summary>
    int _hpNow;

    public int CurrentHPValue => _hpNow;

    public int HpMax => _hpMax;

    public IObservable<int> MaxHpChanged => _maxHp;
    readonly ReactiveProperty<int> _maxHp = new();

    public IObservable<int> CurrentHpChanged => _currentHp;
    readonly ReactiveProperty<int> _currentHp = new();

    /// <summary>������</summary>
    /// <param name="hpMax">HP�ő�l</param>
    /// <param name="stMax">ST�ő�l</param>
    public void Init(int hpMax)
    {
        _hpMax = hpMax;
        _hpNow = hpMax;
        _maxHp.Value = _hpMax;
        _currentHp.Value = _hpMax;
    }

    /// <summary>���݂�HP�l���猸�Z</summary>
    /// <param name="value">�����l</param>
    /// <returns>�O�ȉ���������False��Ԃ�</returns>
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
