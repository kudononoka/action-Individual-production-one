﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [Header("何秒間でフェードイン・アウトさせるか")]
    [SerializeField]
    float _fadeTime;

    int _enemyCount = 0;

    Fade _fade;

    public IObservable<int> EnemyCount => _enemyMaxCount;
    readonly ReactiveProperty<int> _enemyMaxCount = new();

    public IObservable<int> CurrentKillCount => _currentEnemyKillCount;
    readonly ReactiveProperty<int> _currentEnemyKillCount = new();

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
    [SerializeField] SceneState _currentScene = SceneState.Title;

    [Header("シーンのStateとそれに連動するシーン名")]
    [SerializeField]
    SceneData[] _sceneData =
    {
        new SceneData(SceneState.Title),
        new SceneData(SceneState.Tutorial),
        new SceneData(SceneState.InGame),
        new SceneData(SceneState.GameOver),
        new SceneData(SceneState.GameClear),
    };

    private void Setting(SceneState currentScene)
    {
        //フェードアウト
        _fade = FindObjectOfType<Fade>();
        _fade.FadeOut(_fadeTime);

        //BGMの再生とGameScene時のEnemyのキル数リセット設定
        switch (currentScene)
        {
            case SceneState.Title:
                AudioManager.Instance.BGMPlay(BGM.Title);
                break;

            case SceneState.GameOver:
                AudioManager.Instance.BGMPlay(BGM.GameOver);
                break;

            case SceneState.GameClear:
                AudioManager.Instance.BGMPlay(BGM.GameClear);
                break;

            case SceneState.InGame:
                //フィールド上の敵の数を取得
                _instance._enemyCount = FindObjectsOfType<EnemyAI>().Length;
                _instance._enemyMaxCount.Value = _enemyCount;
                //現在のキル数を０に
                _instance._currentEnemyKillCount.Value = 0;

                AudioManager.Instance.BGMPlay(BGM.Game);
                break;

            case SceneState.Tutorial:
                AudioManager.Instance.BGMPlay(BGM.Game);
                break;
        }

    }

    /// <summary>シーン遷移</summary>
    /// <param name="sceneState"></param>
    public void ChangeScene(SceneState sceneState)
    {
        //現在のシーンの更新
        _currentScene = sceneState;
        var nextSceneName = _sceneData[(int)sceneState].SceneName;

        _fade = FindObjectOfType<Fade>();

        //フェードイン後次のシーンに移動
        _fade.FadeIn(_fadeTime, () =>
        {
            SceneManager.LoadScene(nextSceneName);
        });
    }

    /// <summary>Enemyのキル数更新</summary>
    public void EnemyKill()
    {
        //現在のEnemy数を１減らす
        _enemyCount--;
        _currentEnemyKillCount.Value++;
        //全て倒したら
        if(_enemyCount == 0 )
        {
            //ゲームクリア
            ChangeScene(SceneState.GameClear);
        }
    }
}

/// <summary>シーンの状態管理</summary>
public enum SceneState
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


