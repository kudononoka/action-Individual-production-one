using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[Serializable]
public class EnemyParameter
{
    [Header("HPÅ‘å’l")]
    [SerializeField] int _hpMax;

    [Header("cU‚èUŒ‚—Í")]
    [SerializeField] int _attackVerticalSwingPower;

    [Header("‰¡U‚èUŒ‚—Í")]
    [SerializeField] int _attackHorizontalSwingPower;

    [Header("“ÁêUŒ‚—Í")]
    [SerializeField] int _attackSpecialPower;

    [Header("ƒXƒeƒbƒv‚©‚ç‚ÌUŒ‚")]
    [SerializeField] int _attackStepPower;

    public int HPMax => _hpMax;

    /// <summary>cU‚èUŒ‚—Í</summary>
    public int AttackVerticalSwingPower => _attackVerticalSwingPower;

    /// <summary>cU‚èUŒ‚—Í</summary>
    public int AttackHorizontalSwingPower => _attackHorizontalSwingPower;

    /// <summary>cU‚èUŒ‚—Í</summary>
    public int AttackSpecialPower => _attackSpecialPower;

    /// <summary>cU‚èUŒ‚—Í</summary>
    public int AttckStepPower => _attackStepPower;  
}
