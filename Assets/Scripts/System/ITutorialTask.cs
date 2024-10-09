using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ITutorialTask 
{
    public EnemyStateMachine.StateType EnemyType { get;}
    /// <summary>初期化</summary>
    public void Init(PlayerInputAction playerInput){}

    /// <summary>チュートリアルのタイトルを取得</summary>
    string GetTitle();

    /// <summary>説明文を取得</summary>
    string GetDescription();

    /// <summary>チュートリアルが達成されたか判定する</summary>
    /// <returns>Trueだったら達成！</returns>
    bool CheckTask();
}
