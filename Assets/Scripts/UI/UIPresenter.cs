using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIPresenter : MonoBehaviour
{
    [Header("�X�N���v�g")]

    [SerializeField, Tooltip("Player�Ɋւ���UI�̕\�����Ǘ�")] 
    UIController _uiController;

    [SerializeField, Tooltip("Player")] 
    PlayerController _playerConroller;

    [SerializeField, Tooltip("Enemy")]
    EnemyAI _enemyAI;

    void Start()
    {
        //GameManager
        GameManager gameManager = GameManager.Instance;
        //�G��
        gameManager.EnemyCount.Subscribe(value => _uiController.SetUpEnemyCount(value));
        gameManager.CurrentKillCount.Skip(1).Subscribe(value => _uiController.SetCurrentEnemyKillCount(value));

        //Enemy
        EnemyHPController enemyHPController = _enemyAI.HPController;
        enemyHPController.MaxHpChanged.Subscribe(value => _uiController.EnemySetUpMaxHP(value));
        enemyHPController.CurrentHpChanged.Skip(1).Subscribe(value => _uiController.EnemySetCurrentHP(value));

        //Player
        PlayerHPSTController playerHpStController = _playerConroller.PlayerHPSTController;
        //HP
        playerHpStController.MaxHpChanged.Subscribe(value => _uiController.PlayerSetUpMaxHP(value));
        playerHpStController.CurrentHpChanged.Skip(1).Subscribe(value => _uiController.PlayerSetCurrentHP(value));
        //ST
        playerHpStController.MaxStChanged.Subscribe(value => _uiController.SetUpMaxST(value));
        playerHpStController.CurrentStChanged.Skip(1).Subscribe(value => _uiController.SetCurrentST(value));
    }
}
