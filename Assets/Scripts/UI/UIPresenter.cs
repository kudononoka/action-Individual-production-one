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
        //Enemy
        EnemyHPController enemyHPController = _enemyAI.HPController;
        enemyHPController.MaxHpChanged.Subscribe(value => _uiController.EnemySetUpMaxHP(value));
        enemyHPController.CurrentHpChanged.Skip(1).Subscribe(value => _uiController.EnemySetCurrentHP(value));

        //Player
        PlayerHPController playerHpStController = _playerConroller.PlayerHPSTController;
        //HP
        playerHpStController.MaxHpChanged.Subscribe(value => _uiController.PlayerSetUpMaxHP(value));
        playerHpStController.CurrentHpChanged.Skip(1).Subscribe(value => _uiController.PlayerSetCurrentHP(value));
    }
}
