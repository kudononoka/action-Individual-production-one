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

    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.EnemyCount.Subscribe(value => _uiController.SetUpEnemyCount(value));
        gameManager.CurrentKillCount.Skip(1).Subscribe(value => _uiController.SetCurrentEnemyKillCount(value));

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
