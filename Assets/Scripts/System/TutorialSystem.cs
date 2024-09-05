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
        new TutorialLockonTask(),
        new TutorialLockonSelectTask(),
    };

    /// <summary>���ݍs���Ă���`���[�g���A��</summary>
    ITutorialTask _currentTask;

    [SerializeField]
    /// <summary>�^�X�N���I����Ă��玟�̃^�X�N�ɑJ�ڂ���܂ł̎���</summary>
    float _nextTutorialTaskTime = 1f;

    [SerializeField]
    Text _tutorialTaskTitle;

    [SerializeField]
    Text _tutorialTaskDescription;

    [SerializeField]
    Animator _tutorialCanvasAnim;

    [SerializeField]
    EnemyAI[] _enemyAI;

    int _taskNum = -1;

    bool _isTutorial = false;

    bool _isNextTutorialChange = false;

    float _timer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputAction playerInputAction = FindObjectOfType<PlayerInputAction>();
        if (playerInputAction == null)
        {
            Debug.LogError(nameof(PlayerInputAction) + "���A�^�b�`���ꂽGameObject������܂���");
        }

        foreach (var task in _tutorialTasks)
        {
            task.Init(playerInputAction);
        }

        _isTutorial = true;
        
        NextTutorialTask();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTutorial && _currentTask.CheckTask())
        {
            _isNextTutorialChange = true;
        }

        if(_isNextTutorialChange)
        {
            _timer += Time.deltaTime;
            if(_timer >= _nextTutorialTaskTime)
            {
                if (!NextTutorialTask())
                {
                    _isTutorial = false;
                    GameManager.Instance.ChangeScene(SceneState.InGame);
                }
                _timer = 0f;
                _isNextTutorialChange = false;
            }
        }
    }

    /// <summary>���̃`���[�g���A���ɑJ��</summary>
    /// <returns>False��������S�Ẵ`���[�g���A�����I��</returns>
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

        foreach (var enemy in _enemyAI)
        {
            if (!enemy.IsAlive)
            {
                enemy.Resuscitation();
            }
        }

        return true;
    }
}
