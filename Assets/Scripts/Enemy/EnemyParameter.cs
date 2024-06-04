using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[Serializable]
public class EnemyParameter
{
    [Header("HP�ő�l")]
    [SerializeField] int _hpMax;

    [Header("�c�U��U����")]
    [SerializeField] int _attackVerticalSwingPower;

    [Header("���U��U����")]
    [SerializeField] int _attackHorizontalSwingPower;

    [Header("����U����")]
    [SerializeField] int _attackSpecialPower;

    [Header("�X�e�b�v����̍U��")]
    [SerializeField] int _attackStepPower;

    public int HPMax => _hpMax;

    /// <summary>�c�U��U����</summary>
    public int AttackVerticalSwingPower => _attackVerticalSwingPower;

    /// <summary>�c�U��U����</summary>
    public int AttackHorizontalSwingPower => _attackHorizontalSwingPower;

    /// <summary>�c�U��U����</summary>
    public int AttackSpecialPower => _attackSpecialPower;

    /// <summary>�c�U��U����</summary>
    public int AttckStepPower => _attackStepPower;  
}
