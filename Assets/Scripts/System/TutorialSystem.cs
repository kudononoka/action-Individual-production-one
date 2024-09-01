using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    ITutorialTask[] _tutorialTasks =
    {
        new TutorialMoveTask(),
        new TutorialCameraMoveTask(),
        new TutorialWeakAttackTask(),
        new TutorialStrongAttackTask(),
        new TutorialAttackComboTask(),
        new TutorialAvoidanceTask(),
    };

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

    int _taskNum = -1;

    bool _isTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputAction playerInputAction = FindObjectOfType<PlayerInputAction>();
        if (playerInputAction == null)
        {
            Debug.LogError(nameof(PlayerInputAction) + "がアタッチされたGameObjectがありません");
        }

        foreach (var task in _tutorialTasks)
        {
            task.Init(playerInputAction);
        }

        NextTutorialTask();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTutorial && _currentTask.CheckTask())
        {
            if (!NextTutorialTask())
            {
                _isTutorial = false;
            }
        }
    }

    /// <summary>次のチュートリアルに遷移</summary>
    /// <returns>Falseだったら全てのチュートリアルが終了</returns>
    bool NextTutorialTask()
    {
        _taskNum++;

        if (_taskNum >= _tutorialTasks.Length)
        {
            return false;
        }

        _currentTask = _tutorialTasks[_taskNum];

        _tutorialTaskTitle.text = _currentTask.GetTitle();
        _tutorialTaskDescription.text = _currentTask.GetDescription();

        _tutorialCanvasAnim.SetTrigger("Active");


        return true;
    }
}
