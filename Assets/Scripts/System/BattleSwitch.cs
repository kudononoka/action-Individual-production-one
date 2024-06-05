using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSwitch : MonoBehaviour
{
    [Header("Door��Animator")]
    [SerializeField] Animator _anim;

    [SerializeField] UIController _uiController;

    public void BattleStart()
    {
        GameManager.Instance.BattleStart();�@        //�o�g����Ԃɂ���
        _anim.SetBool("IsClose", true);             //����߂�
        _uiController.BattleStart();                  //Enemy�o�[��\��
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))   //�o�g���̃t�B�[���h�ɓ����Ă�����
        {
            BattleStart();
        }
    }
}
