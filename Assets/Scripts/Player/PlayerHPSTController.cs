using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerHPSTController
{
    /// <summary>HP�ő�l</summary>
    int _hpMax;
    /// <summary>ST�ő�l</summary>
    float _stMax;
    [SerializeField]
    /// <summary>���݂�HP</summary>
    int _hpNow;
    /// <summary>���݂�SP</summary>
    float _stNow;
    /// <summary>ST���񕜂��鑬�x</summary>
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

    /// <summary>������</summary>
    /// <param name="hpMax">HP�ő�l</param>
    /// <param name="stMax">ST�ő�l</param>
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

    /// <summary>���݂�ST�l���猸�Z</summary>
    /// <param name="value">�����l</param>
    /// <returns>�O�ȉ���������False��Ԃ�</returns>
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

    /// <summary>ST�̉�</summary>
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
