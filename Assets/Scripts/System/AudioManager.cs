using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>���̃f�[�^�Ǘ��ƍĐ�</summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [Header("AudioSource")]

    [SerializeField]
    [Tooltip("BGM���Đ�����p��AudioSource")]
    AudioSource _bgmAudioSource;

    [SerializeField]
    [Tooltip("SE���Đ�����p��AudioSource")]
    AudioSource _seAudioSource;

    [Space]

    [Header("�Đ���������Audio�̐ݒ�")]

    [SerializeField]
    [Tooltip("�Đ���������SE�Ƃ���Ɋւ���Property")]
    KeyValuePair<SE, SoundProperty>[] _seProperties;

    [SerializeField]
    [Tooltip("�Đ���������BGM�Ƃ���Ɋւ���Property")] 
    KeyValuePair<BGM, SoundProperty>[] _bgmProperties;

    /// <summary>�Đ�������SE��Audio�̃f�[�^</summary>
    Dictionary<SE, SoundProperty> _se = new Dictionary<SE, SoundProperty>();
    /// <summary>�Đ�������BGM��Audio�̃f�[�^</summary>
    Dictionary<BGM, SoundProperty> _bgm = new Dictionary<BGM, SoundProperty>();

    #region�@�V���O���g��
    public static AudioManager Instance
    {
        get
        {
            //instance��null��������
            if (!_instance)
            {
                //�V�[������Gameobject�ɃA�^�b�`����Ă���T���擾
                _instance = FindObjectOfType<AudioManager>();
                _instance.DictinarySet();
                //�A�^�b�`����Ă��Ȃ�������
                if (!_instance)
                {
                    //�G���[���o��
                    Debug.LogError("Scene����" + typeof(AudioManager).Name + "���A�^�b�`���Ă���GameObject������܂���");
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

    /// <summary> SE���Đ����郁�\�b�h</summary>
    /// <param name="se">�Đ�������SE��enum</param>
    public void SEPlay(SE se)
    {
        SoundProperty soundProperty = _se[se];
        _seAudioSource.volume = soundProperty.volum;
        _seAudioSource.PlayOneShot(soundProperty.audioClip);
    }

    /// <summary> BGM���Đ����郁�\�b�h</summary>
    /// <param name="se">�Đ�������BGM��enum</param>
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

    /// <summary>Dictinary�Ɋi�[���邽�߂̃��\�b�h</summary>
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
