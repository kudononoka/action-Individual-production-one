using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    #region　シングルトン
    public static GameManager Instance
    {
        get
        {
            //instanceがnullだったら
            if (!_instance)
            {
                //シーン内のGameobjectにアタッチされているTを取得
                _instance = FindObjectOfType<GameManager>();
                //アタッチされていなかったら
                if (!_instance)
                {
                    //エラーを出す
                    Debug.LogError("Scene内に" + typeof(GameManager).Name + "をアタッチしているGameObjectがありません");
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

    [Header("現在のシーン")]
    [SerializeField] SceneState _currentScene = SceneState.Title;

    [Header("シーンのStateとそれに連動するシーン名")]
    [SerializeField]
    SceneData[] _sceneData =
    {
        new SceneData(SceneState.Title),
        new SceneData(SceneState.InGame),
        new SceneData(SceneState.GameOver),
        new SceneData(SceneState.GameClear),
    };

    /// <summary>バトル中かどうか</summary>
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
                AudioManager.Instance.BGMStop();    //シーンに入ってしばらくしてから流すため
                break;
        }
    }

    /// <summary>シーン遷移</summary>
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

/// <summary>シーンの状態管理</summary>
public enum SceneState
{
    /// <summary>タイトル</summary>
    Title,
    /// <summary>ゲーム(バトル)</summary>
    InGame,
    /// <summary>ゲームオーバー</summary>
    GameOver,
    /// <summary>ゲームクリア</summary>
    GameClear,
}

/// <summary>SceneStateとそれに連動するシーン名の管理</summary>
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


