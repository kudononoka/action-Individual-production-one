using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [Header("何秒間でフェードイン・アウトさせるか")]
    [SerializeField]
    float _fadeTime;

    Fade _fade;

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
        //空だったら
        if (_instance == null)
        {
            _instance = this;
            Setting(_currentScene);
            DontDestroyOnLoad(this.gameObject);
        }
        //自分が入ってたら
        else if (Instance == this)
        {
            _instance._currentScene = this._currentScene;
            _instance.Setting(_instance._currentScene);
            DontDestroyOnLoad(this.gameObject);
        }
        //自分以外すでにあったら
        else
        {
            _instance._currentScene = this._currentScene;
            _instance.Setting(_instance._currentScene);
            Destroy(this);
        }
    }
    #endregion

    [Header("現在のシーン")]
    [SerializeField] GameState _currentScene = GameState.Title;

    [Header("シーンのStateとそれに連動するシーン名")]
    [SerializeField]
    SceneData[] _sceneData =
    {
        new SceneData(GameState.Title),
        new SceneData(GameState.Tutorial),
        new SceneData(GameState.InGame),
    };

    private void Setting(GameState currentScene)
    {
        //フェードアウト
        _fade = FindObjectOfType<Fade>();
        _fade.FadeOut(_fadeTime);

        //BGMの再生
        switch (currentScene)
        {
            case GameState.Title:
                AudioManager.Instance.BGMPlay(BGM.Title);
                break;

            case GameState.InGame:
                AudioManager.Instance.BGMPlay(BGM.Game);
                break;

            case GameState.Tutorial:
                AudioManager.Instance.BGMPlay(BGM.Tutorial);
                break;
        }

    }

    /// <summary>シーン遷移</summary>
    /// <param name="sceneState"></param>
    public void ChangeScene(GameState sceneState)
    {
        //現在のシーンの更新
        _currentScene = sceneState;
        if ((int)sceneState >= _sceneData.Length)
        {
            Debug.LogError("シーンの名前登録していないです");
        }
        var nextSceneName = _sceneData[(int)sceneState].SceneName;

        _fade = FindObjectOfType<Fade>();

        //フェードイン後次のシーンに移動
        _fade.FadeIn(_fadeTime, () =>
        {
            SceneManager.LoadScene(nextSceneName);
        });
    }

    public void GameEnd(GameState gameState)
    {
        GameEndUIControler gameEndUI = FindObjectOfType<GameEndUIControler>();
        //BGM
        switch (gameState)
        {
            case GameState.GameOver:
                gameEndUI.GameOverCanvasActive();
                AudioManager.Instance.BGMPlay(BGM.GameOver);
                break;

            case GameState.GameClear:
                gameEndUI.GameClearCanvasActive();
                AudioManager.Instance.BGMPlay(BGM.GameClear);
                break;
        }
    }
}

/// <summary>ゲームの状態管理</summary>
public enum GameState
{
    /// <summary>タイトル</summary>
    Title,
    /// <summary>チュートリアル</summary>
    Tutorial,
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
    GameState _sceneState;

    [SerializeField]
    string _sceneName;

    public GameState SceneState => _sceneState;

    public string SceneName => _sceneName;

    public SceneData(GameState sceneState)
    {
        _sceneState = sceneState;
    }
}


