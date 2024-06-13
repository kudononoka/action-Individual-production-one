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

    [Header("Rayをとばす時の始点")]
    [SerializeField]
    Transform _originTra;

    [Header("ロックオンの対象となる範囲")]
    [SerializeField]
    float _lockonRange = 20;

    [Header("ロックオン時に表示するImage")]
    [SerializeField]
    GameObject _lockonCursor;

    [Header("ロックオン時のTargetとなるもの")]
    [SerializeField]
    Transform _lockonTarget;

    [Header("カーソルの位置")]
    [SerializeField]
    Transform _lockonCursorTra;

    [Header("ロックオンの対象となるLayer")]
    [SerializeField]
    LayerMask _lockonLayers = 0;

    /// <summary>ロックオン中かどうか</summary>
    bool _isLockon = false;

    bool _pastIsLockon = false;

    MinorEnemyAI[] _lockonTargets = null;

    MinorEnemyAI _lockonTargetEnemy = null;

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
            if(_lockonTargetEnemy.IsDeath)
            {
                GetLockonTarget();
                if (_lockonTargets == null)
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
                    _lockonCamera.LookAt = _lockonTarget;
                }
            }
            Vector3 pos = _lockonCursorTra.position;
            pos.y += 1.7f;
            _lockonCursor.transform.position = _mainCamera.WorldToScreenPoint(pos);
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
                _lockonCamera.LookAt = _lockonTarget;
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

        RaycastHit[] hits = Physics.SphereCastAll(_originTra.position, _lockonRange, Vector3.up, 0, _lockonLayers);

        if (hits?.Length == 0) return;

        var enemies =  hits.Select(h => h.transform.gameObject.GetComponent<MinorEnemyAI>());


        _lockonTargets = enemies.Where(enemy => enemy.IsDeath == false)
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
        _lockonCamera.LookAt = _lockonTarget;
    }

}