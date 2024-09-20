using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    Transform _lockonTargetTra;

    [Header("カーソルの位置")]
    [SerializeField]
    Transform _lockonCursorTra;

    [SerializeField]
    LockonRange _lockonRange;

    Transform _originTra;

    /// <summary>ロックオン中かどうか</summary>
    bool _isLockon = false;

    bool _pastIsLockon = false;

    EnemyAI[] _lockonTargetsEnemyAI = null;

    EnemyAI _lockonTargetEnemyAI = null;

    Camera _mainCamera;

    PlayerInputAction _inputAction;

    Image _lockonCursorImage;

    int _currentLockonTargetID = 0;

    public Transform LockonTarget => _lockonTargetTra;

    public LockonRange LockonRange => _lockonRange;

    public void Init(PlayerInputAction inputAction)
    {
        _mainCamera = Camera.main;
        _inputAction = inputAction;
        _lockonCursorImage = _lockonCursor.GetComponent<Image>();
        _lockonCursorImage.enabled = false;
        _originTra = inputAction.transform;

        //カメラを最初Defaultに設定
        CameraChange(false);
    }

    public void OnUpdate()
    {
        // ロックオン切り替え入力された時だけ処理を行う
        if (_inputAction.IsLockon != _pastIsLockon)   
        {
            _pastIsLockon = _inputAction.IsLockon;
            _lockonCursorImage.enabled = _inputAction.IsLockon;
            CameraChange(_inputAction.IsLockon);
        }

        //ロックオン選択が入力されたら(ゲームパッド用)
        if (_inputAction.IsLockon && _inputAction.IsLockonSelectGamepad)
        {
            LockonTargetChange();
            _lockonCursorTra = _lockonTargetTra.transform;
            _inputAction.IsLockonSelectGamepad = false;
        }
        //ロックオン選択が入力されたら(マウスホイール用)
        if (_inputAction.IsLockon && _inputAction.IsLockonSelectMouse.magnitude > 0)
        {
            int num = _inputAction.IsLockonSelectMouse.y < 0 ? -1 : 1; 
            LockonTargetChange(num);
            _lockonCursorTra = _lockonTargetTra.transform;
            _inputAction.IsLockonSelectGamepad = false;
        }

        if (_isLockon)//ロックオン中
        {
            //ロックオンしていた敵が死んだら
            if(!_lockonTargetEnemyAI.IsAlive)
            {
                //ロックオン対象となるものを再度探す
                GetLockonTarget();

                //ロックオンするものがなかったら
                if (_lockonTargetsEnemyAI == null || _lockonTargetsEnemyAI.Length == 0)
                {
                    //ロックオン解除
                    _isLockon = false;
                    _inputAction.IsLockon = false;
                    return;
                }
                //あったら
                else
                {
                    //Target更新
                    _currentLockonTargetID = 0;
                    _lockonTargetTra = _lockonTargetsEnemyAI[_currentLockonTargetID].transform;
                    _lockonTargetEnemyAI = _lockonTargetsEnemyAI[_currentLockonTargetID];
                    _lockonCursorTra = _lockonTargetTra.transform;
                }
            }

            //カメラの中心点をPlayerとロックオン対象の真ん中に設定
            var vec = _lockonTargetTra.position - _originTra.position;
            Vector3 cameraConterPos = (vec * 0.5f) + _originTra.position;
            _lockonCameraCenter.position = cameraConterPos;

            //ロックオンカーソル位置設定
            Vector3 cursorPos = _lockonCursorTra.position;
            cursorPos.y += 1;
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
            if(_lockonTargetsEnemyAI == null || _lockonTargetsEnemyAI.Length == 0)
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
                _lockonTargetTra = _lockonTargetsEnemyAI[_currentLockonTargetID].transform;
                _lockonTargetEnemyAI = _lockonTargetsEnemyAI[_currentLockonTargetID];
                _lockonCursorTra = _lockonTargetTra.transform;
            }
        }

        if (_isLockon)
        {
            //通常からロックオンカメラ用に変更
            _defaultCamera.Priority = 0;
            _lockonCamera.Priority = 10;
        }
        else
        {
            //ロックオン中のカメラの角度と一緒にしてDefaultに戻った時違和感がないようにする
            var pov = _defaultCamera.GetCinemachineComponent<CinemachinePOV>();
            pov.m_VerticalAxis.Value = Mathf.Repeat(_lockonCamera.transform.eulerAngles.x + 180, 360) - 180;
            pov.m_HorizontalAxis.Value = _lockonCamera.transform.eulerAngles.y;

            //ロックオンカメラから通常に変更
            _defaultCamera.Priority = 10;
            _lockonCamera.Priority = 0;
        }
    }

    /// <summary>ロックオン対象となる敵をあらいだす</summary>
    void GetLockonTarget()
    {
        _lockonTargetsEnemyAI = null;

        //ロックオン可能範囲内のEnemyを保持
        var enemies = _lockonRange.EnemiesInRange;

        //そのEnemyは生きていることを条件にPlayerから近い順で整理し配列にしなおす
        _lockonTargetsEnemyAI = enemies.Where(enemy => enemy.IsAlive == true)
                                .OrderBy(go => Vector3.Distance(go.gameObject.transform.position, _originTra.position))
                                .ToArray();
    }

    /// <summary>ロックオンの対象を変える</summary>
    void LockonTargetChange(int up = 1 )
    {
        //ロックオン対象が複数の場合、選択しやすいようIDよりindex（次の対象のＩＤ）
        //が下か上どちらでもすぐ変更できるようにした
        _currentLockonTargetID += up;
        if (_currentLockonTargetID == _lockonTargetsEnemyAI.Length)
        {
            _currentLockonTargetID = 0;
        }
        else if(_currentLockonTargetID < 0)
        {
            _currentLockonTargetID = _lockonTargetsEnemyAI.Length - 1;
        }

    　　//ロックオン対象の更新
        _lockonTargetTra = _lockonTargetsEnemyAI[_currentLockonTargetID].transform;
        _lockonTargetEnemyAI = _lockonTargetsEnemyAI[_currentLockonTargetID];
    }

}