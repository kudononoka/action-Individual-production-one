using UnityEngine;
using System;

[Serializable]
public class SoundProperty
{
    [SerializeField]
    [Tooltip("再生させたいAudioClip")]
    AudioClip _audioClip;

    [SerializeField, Range(0, 1f)]
    [Tooltip("再生時の音量")] 
    float _volum;

    [SerializeField, Range(0, 1f)]
    float _pitch = 1; 

    public AudioClip AudioClip => _audioClip;
    public float Volum => _volum;
    public float Pitch => _pitch;
}

/// <summary>どの状態時にSEを再生するか管理するenum</summary>
public enum SE
{
    /// <summary>Player足音</summary>
    PlayerFootsteps,
    /// <summary>Playerが武器を振る音(弱攻撃)</summary>
    PlayerAttackWeakSwish,
    /// <summary>Playerが武器を振る音(強攻撃)</summary>
    PlayerAttackStrongSwish,
    /// <summary>Player攻撃が当たった音</summary>
    PlayerAttackHit,
    /// <summary>Enemyの攻撃音</summary>
    EnemyAttack,
    /// <summary>Enemyの攻撃が当たった音</summary>
    EnemyAttackHit,
    /// <summary>Enemyの攻撃サイン音</summary>
    EnemyAttackSign,
}

/// <summary>どの状態時にBGMを再生するか管理するenum</summary>
public enum BGM
{
    /// <summary>タイトル</summary>
    Title,
    /// <summary>インゲーム</summary>
    Game,
    /// <summary>ゲームオーバー</summary>
    GameOver,
    /// <summary>ゲームクリア</summary>
    GameClear
}
