using UnityEngine;
using System;

[Serializable]
public struct SoundProperty
{
    [SerializeField]
    [Tooltip("�Đ���������AudioClip")]
    AudioClip _audioClip;

    [SerializeField, Range(0, 1f)]
    [Tooltip("�Đ����̉���")] 
    float _volum;

    public AudioClip audioClip => _audioClip;
    public float volum => _volum;
}

/// <summary>�ǂ̏�Ԏ���SE���Đ����邩�Ǘ�����enum</summary>
public enum SE
{
    Shot,
}

/// <summary>�ǂ̏�Ԏ���BGM���Đ����邩�Ǘ�����enum</summary>
public enum BGM
{
    Title,
    Game,
    GameOver,
    GameClear
}
