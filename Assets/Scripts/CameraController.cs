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
    [Header("�ʏ펞�̃J����")]
    [SerializeField]
    CinemachineVirtualCamera _defaultCamera;

    [Header("���b�N�I�����Ɏg�p����J����")]
    [SerializeField]
    CinemachineVirtualCamera _lockonCamera;

    [Header("���b�N�I�����ɕ\������Image")]
    [SerializeField]
    GameObject _lockonCursor;

    [Header("���b�N�I������Target�ƂȂ����")]
    [SerializeField]
    Transform _lockonTarget;

    /// <summary>���b�N�I�������ǂ���</summary>
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
        if (_inputAction.IsLockon != _pastIsLockon)      //���b�N�I���؂�ւ����͂��ꂽ�������������s��
        {
            _pastIsLockon = _inputAction.IsLockon;
            _lockonCursorImage.enabled = _inputAction.IsLockon;
            CameraChange(_inputAction.IsLockon);
        }

        if(_isLockon)   //���b�N�I�����J�[�\��
            _lockonCursor.transform.position = _mainCamera.WorldToScreenPoint(_lockonTarget.transform.position);
    }

    /// <summary>�J�����̐؂�ւ�</summary>
    /// <param name="isLockon">���b�N�I�����ǂ���</param>
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
            //���b�N�I�����̃J�����̊p�x�ƈꏏ�ɂ���Default�ɖ߂�������a�����Ȃ��悤�ɂ���
            var pov = _defaultCamera.GetCinemachineComponent<CinemachinePOV>();
            pov.m_VerticalAxis.Value = Mathf.Repeat(_lockonCamera.transform.eulerAngles.x + 180, 360) - 180;
            pov.m_HorizontalAxis.Value = _lockonCamera.transform.eulerAngles.y;

            _defaultCamera.Priority = 10;
            _lockonCamera.Priority = 0;
        }
    }
}
