using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ITutorialTask 
{
    public EnemyStateMachine.StateType EnemyType { get;}
    /// <summary>������</summary>
    public void Init(PlayerInputAction playerInput){}

    /// <summary>�`���[�g���A���̃^�C�g�����擾</summary>
    string GetTitle();

    /// <summary>���������擾</summary>
    string GetDescription();

    /// <summary>�`���[�g���A�����B�����ꂽ�����肷��</summary>
    /// <returns>True��������B���I</returns>
    bool CheckTask();
}
