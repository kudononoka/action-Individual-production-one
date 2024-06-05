using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSwitch : MonoBehaviour
{
    [Header("DoorのAnimator")]
    [SerializeField] Animator _anim;

    [SerializeField] UIController _uiController;

    public void BattleStart()
    {
        GameManager.Instance.BattleStart();　        //バトル状態にする
        _anim.SetBool("IsClose", true);             //扉を閉める
        _uiController.BattleStart();                  //Enemyバーを表示
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))   //バトルのフィールドに入ってきたら
        {
            BattleStart();
        }
    }
}
