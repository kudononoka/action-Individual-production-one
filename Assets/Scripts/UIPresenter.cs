using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIPresenter : MonoBehaviour
{
    [Header("スクリプト")]

    [SerializeField, Tooltip("Playerに関するUIの表示を管理")] 
    UIController _uiController;

    [SerializeField, Tooltip("Player")] 
    PlayerController _playerConroller;

    [SerializeField, Tooltip("Enemy")]
    EnemyAI _enemyAI;

    void Start()
    {
        //Enemy　HP
        EnemyHPController enemyHPSTController = _enemyAI.EnemyHPSTController;
        enemyHPSTController.MaxHpChanged.Subscribe(value => _uiController.SetUpMaxEnemyHP(value));
        enemyHPSTController.CurrentHpChanged.Skip(1).Subscribe(value => _uiController.SetCurrentEnemyHP(value));

        //Player
        PlayerHPSTController playerHpStController = _playerConroller.PlayerHPSTController;
        //HP
        playerHpStController.MaxHpChanged.Subscribe(value => _uiController.SetUpMaxHP(value));
        playerHpStController.CurrentHpChanged.Skip(1).Subscribe(value => _uiController.SetCurrentHP(value));
        //ST
        playerHpStController.MaxStChanged.Subscribe(value => _uiController.SetUpMaxST(value));
        playerHpStController.CurrentStChanged.Skip(1).Subscribe(value => _uiController.SetCurrentST(value));
    }
}
