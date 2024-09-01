using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;

[Serializable]
public class CameraController
{
    [Header("通常時のカメラ")]
    [SerializeField]
    CinemachineVirtualCamera _defaultCamera;

    [Header("ロックオン中に使用するカメラ")]
    [SerializeField]
    CinemachineVirtualCamera _lockonCamera;

    [Header("ロックオン中カメラの中心点となる場所")]
    [SerializeField]
    Transform _lockonCameraCenter;

    [Header("ロックオン時に表示するImage")]
    [SerializeField]
    GameObject _lockonCursor;

    [Header("ロックオン時のTargetとなるもの")]
    [SerializeField]
    Transform _lockonTarget;

    [Header("カーソルの位置")]
    [SerializeField]
    Transform _lockonCursorTra;

    [SerializeField]
    LockonRange _lockonRange;

    Transform _originTra;

    /// <summary>ロックオン中かどうか</summary>
    bool _isLockon = false;

    bool _pastIsLockon = false;

    EnemyAI[] _lockonTargets = null;

    EnemyAI _lockonTargetEnemy = null;

    Camera _mainCamera;

    PlayerInputAction _inputAction;

    Image _lockonCursorImage;

    int _currentLockonTargetID = 0;

    public Transform LockonTarget => _lockonTarget;
    public void Init(PlayerInputAction inputAction)
    {
        _mainCamera = Camera.main;
        _inputAction = inputAction;
        _lockonCursorImage = _lockonCursor.GetComponent<Image>();
        _lockonCursorImage.enabled = false;
        _originTra = inputAction.transform;
        CameraChange(false);
    }

    public void OnUpdate()
    {
        if (_inputAction.IsLockon != _pastIsLockon)      //ロックオン切り替え入力された時だけ処理を行う
        {
            _pastIsLockon = _inputAction.IsLockon;
            _lockonCursorImage.enabled = _inputAction.IsLockon;
            CameraChange(_inputAction.IsLockon);
        }

        if (_inputAction.IsLockon && _inputAction.IsLockonSelect)
        {
            LockonTargetChange();
            _lockonCursorTra = _lockonTarget.transform;
            _inputAction.IsLockonSelect = false;
        }

        if (_isLockon)//ロックオン中
        {
            if(!_lockonTargetEnemy.IsAlive)
            {
                GetLockonTarget();
                if (_lockonTargets == null || _lockonTargets.Length == 0)
                {
                    _isLockon = false;
                    _inputAction.IsLockon = false;
                    return;
                }
                else
                {
                    _currentLockonTargetID = 0;
                    _lockonTarget = _lockonTargets[_currentLockonTargetID].transform;
                    _lockonTargetEnemy = _lockonTargets[_currentLockonTargetID];
                    _lockonCursorTra = _lockonTarget.transform;
                }
            }

            var vec = _lockonTarget.position - _originTra.position;
            Vector3 cameraConterPos = (vec * 0.5f) + _originTra.position;
            _lockonCameraCenter.position = cameraConterPos;

            Vector3 cursorPos = _lockonCursorTra.position;
            cursorPos.y += 1.7f;
            _lockonCursor.transform.position = _mainCamera.WorldToScreenPoint(cursorPos);
        }
    }

    /// <summary>カメラの切り替え</summary>
    /// <param name="isLockon">ロックオンかどうか</param>
    void CameraChange(bool isLockon)
    {
        _isLockon = isLockon;

        if (_isLockon)
        {
            GetLockonTarget();
            if(_lockonTargets == null || _lockonTargets.Length == 0)
            {
                _isLockon = false;
                _inputAction.IsLockon = false;
                _pastIsLockon = _inputAction.IsLockon;
                _lockonCursorImage.enabled = _inputAction.IsLockon;
                return;
            }
            else
            {
                _currentLockonTargetID = 0;
                _lockonTarget = _lockonTargets[_currentLockonTargetID].transform;
                _lockonTargetEnemy = _lockonTargets[_currentLockonTargetID];
                _lockonCursorTra = _lockonTarget.transform;
            }
        }

        if (_isLockon)
        {
            _defaultCamera.Priority = 0;
            _lockonCamera.Priority = 10;
        }
        else
        {
            //ロックオン中のカメラの角度と一緒にしてDefaultに戻った時違和感がないようにする
            var pov = _defaultCamera.GetCinemachineComponent<CinemachinePOV>();
            pov.m_VerticalAxis.Value = Mathf.Repeat(_lockonCamera.transform.eulerAngles.x + 180, 360) - 180;
            pov.m_HorizontalAxis.Value = _lockonCamera.transform.eulerAngles.y;

            _defaultCamera.Priority = 10;
            _lockonCamera.Priority = 0;
        }
    }

    void GetLockonTarget()
    {
        _lockonTargets = null;

        var enemies = _lockonRange.EnemiesInRange;

        _lockonTargets = enemies.Where(enemy => enemy.IsAlive == true)
                                .OrderBy(go => Vector3.Distance(go.gameObject.transform.position, _originTra.position))
                                .ToArray();
    }

    void LockonTargetChange()
    {
        _currentLockonTargetID++;
        if (_currentLockonTargetID == _lockonTargets.Length)
        {
            _currentLockonTargetID = 0;
        }
        _lockonTarget = _lockonTargets[_currentLockonTargetID].transform;
        _lockonTargetEnemy = _lockonTargets[_currentLockonTargetID];
    }

}