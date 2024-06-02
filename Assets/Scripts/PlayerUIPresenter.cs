using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerUIPresenter : MonoBehaviour
{
    [Header("�X�N���v�g")]

    [SerializeField, Tooltip("Player�Ɋւ���UI�̕\�����Ǘ�")] 
    PlayerUIController _playerUIController;

    [SerializeField, Tooltip("Player")] 
    PlayerController _playerConroller;

    void Start()
    {
        PlayerHPSTController playerHpStController = _playerConroller.PlayerHPSTController;
        //HP
        playerHpStController.MaxHpChanged.Subscribe(value => _playerUIController.SetUpMaxHP(value));
        playerHpStController.CurrentHpChanged.Skip(1).Subscribe(value => _playerUIController.SetCurrentHP(value));

        //ST
        playerHpStController.MaxStChanged.Subscribe(value => _playerUIController.SetUpMaxST(value));
        playerHpStController.CurrentStChanged.Skip(1).Subscribe(value => _playerUIController.SetCurrentST(value));
    }
}
