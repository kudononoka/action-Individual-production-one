using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[Serializable]
public class CameraController
{
    [Header("通常時のカメラ")]
    [SerializeField]
    CinemachineVirtualCamera _defaultCamera;

    [Header("ロックオン中に使用するカメラ")]
    [SerializeField]
    CinemachineVirtualCamera _lockonCamera;

    [Header("ロックオン時に表示するImage")]
    [SerializeField]
    GameObject _lockonCursor;

    [Header("ロックオン時のTargetとなるもの")]
    [SerializeField]
    Transform _lockonTarget;

    /// <summary>ロックオン中かどうか</summary>
    bool _isLockon = false;

    bool _pastIsLockon = false;
    
    Camera _mainCamera;

    PlayerInputAction _inputAction;

    Image _lockonCursorImage;

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

        if(_isLockon)   //ロックオン中カーソル
            _lockonCursor.transform.position = _mainCamera.WorldToScreenPoint(_lockonTarget.transform.position);
    }

    /// <summary>カメラの切り替え</summary>
    /// <param name="isLockon">ロックオンかどうか</param>
    void CameraChange(bool isLockon)
    {
        _isLockon = isLockon;
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
}
