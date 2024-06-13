using UnityEngine;
using System;

[Serializable]
public struct SoundProperty
{
    [SerializeField]
    [Tooltip("再生させたいAudioClip")]
    AudioClip _audioClip;

    [SerializeField, Range(0, 1f)]
    [Tooltip("再生時の音量")] 
    float _volum;

    public AudioClip audioClip => _audioClip;
    public float volum => _volum;
}

/// <summary>どの状態時にSEを再生するか管理するenum</summary>
public enum SE
{
    /// <summary>足音</summary>
    Footsteps,
}

/// <summary>どの状態時にBGMを再生するか管理するenum</summary>
public enum BGM
{
    Title,
    Game,
    GameOver,
    GameClear
}
