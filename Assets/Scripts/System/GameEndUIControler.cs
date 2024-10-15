using UnityEngine;
using UnityEngine.EventSystems;

public class GameEndUIControler : MonoBehaviour
{
    [Header("ゲームオーバーUI")]
    [SerializeField] GameObject _gameOverUI;

    [SerializeField] GameObject _gameOverFirstButton;

    [Header("ゲームクリアUI")]
    [SerializeField] GameObject _gameClearUI;

    [SerializeField] GameObject _gameClearFirstButton;

    [SerializeField] EventSystem _eventSystem;

    Canvas _gameOverCanvas;

    Canvas _gameClearCanvas;


    public void Start()
    {
        _gameOverCanvas = _gameOverUI.GetComponent<Canvas>();
        _gameClearCanvas = _gameClearUI.GetComponent<Canvas>();
        _gameOverCanvas.enabled = false;
        _gameClearCanvas.enabled = false;
    }

    public void GameOverCanvasActive()
    {
        _gameOverCanvas.enabled = true;
        EventSystem.current.SetSelectedGameObject(_gameOverFirstButton);
    }

    public void GameClearCanvasActive()
    {
        _gameClearCanvas.enabled = true;
        EventSystem.current.SetSelectedGameObject(_gameClearFirstButton);
    }
}
