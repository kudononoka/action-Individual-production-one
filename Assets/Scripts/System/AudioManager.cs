using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>音のデータ管理と再生</summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [Header("AudioSource")]

    [SerializeField]
    [Tooltip("BGMを再生する用のAudioSource")]
    AudioSource _bgmAudioSource;

    [SerializeField]
    [Tooltip("SEを再生する用のAudioSource")]
    AudioSource _seAudioSource;

    [Space]

    [Header("再生させたいAudioの設定")]

    [SerializeField]
    [Tooltip("再生させたいSEとそれに関するProperty")]
    KeyValuePair<SE, SoundProperty>[] _seProperties;

    [SerializeField]
    [Tooltip("再生させたいBGMとそれに関するProperty")] 
    KeyValuePair<BGM, SoundProperty>[] _bgmProperties;

    /// <summary>再生させるSEのAudioのデータ</summary>
    Dictionary<SE, SoundProperty> _se = new Dictionary<SE, SoundProperty>();
    /// <summary>再生させるBGMのAudioのデータ</summary>
    Dictionary<BGM, SoundProperty> _bgm = new Dictionary<BGM, SoundProperty>();

    #region　シングルトン
    public static AudioManager Instance
    {
        get
        {
            //instanceがnullだったら
            if (!_instance)
            {
                //シーン内のGameobjectにアタッチされているTを取得
                _instance = FindObjectOfType<AudioManager>();
                _instance.DictinarySet();
                //アタッチされていなかったら
                if (!_instance)
                {
                    //エラーを出す
                    Debug.LogError("Scene内に" + typeof(AudioManager).Name + "をアタッチしているGameObjectがありません");
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DictinarySet();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    /// <summary> SEを再生するメソッド</summary>
    /// <param name="se">再生したいSEのenum</param>
    public void SEPlay(SE se)
    {
        SoundProperty soundProperty = _se[se];
        _seAudioSource.volume = soundProperty.volum;
        _seAudioSource.PlayOneShot(soundProperty.audioClip);
    }

    /// <summary> BGMを再生するメソッド</summary>
    /// <param name="se">再生したいBGMのenum</param>
    public void BGMPlay(BGM bgm)
    {
        SoundProperty soundProperty = _bgm[bgm];

        if (_bgmAudioSource.clip == soundProperty.audioClip)
        {
            return;
        }

        if (_bgmAudioSource.isPlaying)
        {
            _bgmAudioSource.Stop();
        }

        _bgmAudioSource.clip = soundProperty.audioClip;
        _bgmAudioSource.volume = soundProperty.volum;
        _bgmAudioSource.Play();
    }

    public void BGMStop()
    {
        if (_bgmAudioSource.isPlaying)
        {
            _bgmAudioSource.Stop();
        }
    }

    /// <summary>Dictinaryに格納するためのメソッド</summary>
    void DictinarySet()
    {
        foreach (var seProperty in _seProperties)
        {
            _se.Add(seProperty.Key, seProperty.Value);
        }
        foreach (var bgmProperty in _bgmProperties)
        {
            _bgm.Add(bgmProperty.Key, bgmProperty.Value);
        }
    }
}
