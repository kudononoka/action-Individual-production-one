using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    ITutorialTask[] _tutorialTasks =
    {
        new TutorialMoveTask(),
        new TutorialCameraMoveTask(),
        new TutorialAttackTask(),
        new TutorialAttackComboTask(),
        new TutorialChargeAttack(),
        new TutorialAvoidanceTask(),
        new TutorialLockonTask(),
        new TutorialSuccess(),
        //new TutorialLockonSelectTask(),
    };

    [Header("チュートリアル開始するかどうか")]
    [SerializeField] bool _isTutorialStart;

    /// <summary>現在行っているチュートリアル</summary>
    ITutorialTask _currentTask;

    [SerializeField]
    /// <summary>タスクが終わってから次のタスクに遷移するまでの時間</summary>
    float _nextTutorialTaskTime = 1f;

    [SerializeField]
    Text _tutorialTaskTitle;

    [SerializeField]
    Text _tutorialTaskDescription;

    [SerializeField]
    Animator _tutorialCanvasAnim;

    [SerializeField]
    EnemyAI _enemyAI;

    int _taskNum = -1;

    bool _isTutorial = false;

    bool _isNextTutorialChange = false;

    [SerializeField]
    float _timer;

    // Start is called before the first frame update
    void Start()
    {
        //チュートリアル開始
        if (_isTutorialStart)
        {
            //入力値クラス取得
            PlayerInputAction playerInputAction = FindObjectOfType<PlayerInputAction>();
            if (playerInputAction == null)
            {
                Debug.LogError(nameof(PlayerInputAction) + "がアタッチされたGameObjectがありません");
            }

            //それぞれのタスクの初期化
            foreach (var task in _tutorialTasks)
            {
                task.Init(playerInputAction);
            }

            //チュートリアル中
            _isTutorial = true;

            //チュートリアルタスク更新
            NextTutorialTask();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //チュートリアル開始
        if (_isTutorialStart)
        {
            //チュートリアル中、現在のチュートリアルタスクを達成したら
            if (_isTutorial && _currentTask.CheckTask())
            {
                //次のチュートリアルの準備にかかる
                _isNextTutorialChange = true;
            }

            if (_isNextTutorialChange)
            {
                _timer += Time.deltaTime;
                //次のチュートリアルに遷移する時間になったら
                if (_timer >= _nextTutorialTaskTime)
                {
                    //次のチュートリアルのタスクがない時
                    if (!NextTutorialTask())
                    {
                        //チュートリアル終了
                        _isTutorial = false;
                        //ゲームシーンに遷移
                        GameManager.Instance.ChangeScene(SceneState.InGame);
                    }

                    //初期化
                    _timer = 0f;
                    _isNextTutorialChange = false;
                }
            }
        }
        
    }

    /// <summary>次のチュートリアルに遷移</summary>
    /// <returns>Falseだったら全てのチュートリアルが終了</returns>
    bool NextTutorialTask()
    {
        _taskNum++;

        //全てのチュートリアルがおわったら
        if (_taskNum >= _tutorialTasks.Length)
        {
            return false;
        }

        //次行うチュートリアルに変更
        _currentTask = _tutorialTasks[_taskNum];

        //概要、説明Textの変更
        _tutorialTaskTitle.text = _currentTask.GetTitle;
        _tutorialTaskDescription.text = _currentTask.GetDescription;
        //次遷移するまでの時間の変更
        _nextTutorialTaskTime = _currentTask.NextTutorialTaskTime;

        //チュートリアル用キャンバスのアニメーション再生
        _tutorialCanvasAnim.SetTrigger("Active");

        //EnemyのAnimationTypeを変更
        _enemyAI.StateReset(_currentTask.EnemyType);

        return true;
    }
}
