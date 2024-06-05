using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    #region�@�V���O���g��
    public static GameManager Instance
    {
        get
        {
            //instance��null��������
            if (!_instance)
            {
                //�V�[������Gameobject�ɃA�^�b�`����Ă���T���擾
                _instance = FindObjectOfType<GameManager>();
                //�A�^�b�`����Ă��Ȃ�������
                if (!_instance)
                {
                    //�G���[���o��
                    Debug.LogError("Scene����" + typeof(GameManager).Name + "���A�^�b�`���Ă���GameObject������܂���");
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
            Setting(_currentScene);
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            _instance._currentScene = this._currentScene;
            _instance.Setting(_instance._currentScene);
            Destroy(this);
        }
    }
    #endregion

    [Header("���݂̃V�[��")]
    [SerializeField] SceneState _currentScene = SceneState.Title;

    [Header("�V�[����State�Ƃ���ɘA������V�[����")]
    [SerializeField]
    SceneData[] _sceneData =
    {
        new SceneData(SceneState.Title),
        new SceneData(SceneState.InGame),
        new SceneData(SceneState.GameOver),
        new SceneData(SceneState.GameClear),
    };

    /// <summary>�o�g�������ǂ���</summary>
    bool _isBattle = false;

    public bool IsBattle => _isBattle;

    private void Setting(SceneState currentScene)
    {
        switch(currentScene)
        {
            case SceneState.Title:
                AudioManager.Instance.BGMPlay(BGM.Title);
                break;
            case SceneState.GameOver:
                AudioManager.Instance.BGMPlay(BGM.GameOver);
                _isBattle = false;
                break;
            case SceneState.GameClear:
                AudioManager.Instance.BGMPlay(BGM.GameClear);
                _isBattle = false;
                break;
            case SceneState.InGame:
                AudioManager.Instance.BGMStop();    //�V�[���ɓ����Ă��΂炭���Ă��痬������
                break;
        }
    }

    /// <summary>�V�[���J��</summary>
    /// <param name="sceneState"></param>
    public void ChangeScene(SceneState sceneState)
    {
        _currentScene = sceneState;
        var nextSceneName = _sceneData[(int)sceneState].SceneName;
        SceneManager.LoadScene(nextSceneName);
    }

    public void BattleStart()
    {
        AudioManager.Instance.BGMPlay(BGM.Game);
        _isBattle = true;
    }
}

/// <summary>�V�[���̏�ԊǗ�</summary>
public enum SceneState
{
    /// <summary>�^�C�g��</summary>
    Title,
    /// <summary>�Q�[��(�o�g��)</summary>
    InGame,
    /// <summary>�Q�[���I�[�o�[</summary>
    GameOver,
    /// <summary>�Q�[���N���A</summary>
    GameClear,
}

/// <summary>SceneState�Ƃ���ɘA������V�[�����̊Ǘ�</summary>
[Serializable]
public class SceneData
{
    [SerializeField]
    SceneState _sceneState;

    [SerializeField]
    string _sceneName;

    public SceneState SceneState => _sceneState;

    public string SceneName => _sceneName;

    public SceneData(SceneState sceneState)
    {
        _sceneState = sceneState;
    }
}


