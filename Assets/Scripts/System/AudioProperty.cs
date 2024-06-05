using UnityEngine;
using System;

[Serializable]
public struct SoundProperty
{
    [SerializeField]
    [Tooltip("Ä¶‚³‚¹‚½‚¢AudioClip")]
    AudioClip _audioClip;

    [SerializeField, Range(0, 1f)]
    [Tooltip("Ä¶‚Ì‰¹—Ê")] 
    float _volum;

    public AudioClip audioClip => _audioClip;
    public float volum => _volum;
}

/// <summary>‚Ç‚Ìó‘Ô‚ÉSE‚ğÄ¶‚·‚é‚©ŠÇ—‚·‚éenum</summary>
public enum SE
{
    Shot,
}

/// <summary>‚Ç‚Ìó‘Ô‚ÉBGM‚ğÄ¶‚·‚é‚©ŠÇ—‚·‚éenum</summary>
public enum BGM
{
    Title,
    Game,
    GameOver,
    GameClear
}
