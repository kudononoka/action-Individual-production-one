using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>InputSystemを使った入力値の取得と管理</summary>
public class PlayerInputAction : MonoBehaviour
{
    [Header("入力を受け取っているかの確認")]
    [SerializeField] 
    Vector2 _inputMove = Vector2.zero;

    [SerializeField]
    Vector2 _inputCameraMove = Vector2.zero;

    [SerializeField] 
    bool _isAttackWeak = false;

    [SerializeField] 
    bool _isAttackStrong = false;

    [SerializeField] 
    bool _isGuard = false;

    [SerializeField] 
    bool _isLockon = false;

    [SerializeField]
    bool _isLockonSelectGamepad = false;

    [SerializeField]
    Vector2 _isLockonSelectMouse = Vector2.zero;

    [SerializeField]
    bool _isEvade = false;

    /// <summary>入力値(移動)</summary>
    public Vector2 InputMove => _inputMove;

    /// <summary>入力値(カメラ移動)</summary>
    public Vector2 CameraMove => _inputCameraMove;

    /// <summary>入力値(弱攻撃)</summary>
    public bool IsAttackWeak { get => _isAttackWeak;  set => _isAttackWeak = value; }

    /// <summary>入力値(強攻撃)</summary>
    public bool IsAttackStrong { get => _isAttackStrong; set => _isAttackStrong = value; }

    /// <summary>入力値(ガード)</summary>
    public bool IsGuard => _isGuard;

    /// <summary>入力(ロックオン)</summary>
    public bool IsLockon { get => _isLockon; set => _isLockon = value; }

    /// <summary>入力(回避)</summary>
    public bool IsEvade { get => _isEvade; set => _isEvade = value; }

    /// <summary>入力値(ロックオン選択)</summary>
    public bool IsLockonSelectGamepad { get => _isLockonSelectGamepad; set => _isLockonSelectGamepad = value; }


    /// <summary>入力値(ロックオン選択)</summary>
    public Vector2 IsLockonSelectMouse => _isLockonSelectMouse;

    /// <summary>移動Action</summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
    }

    /// <summary>移動Action</summary>
    public void OnCameraMove(InputAction.CallbackContext context)
    {
        _inputCameraMove = context.ReadValue<Vector2>();
    }

    public void OnAttackWeak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isAttackWeak = true;
        }
    }

    public void OnAttackStrong(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isAttackStrong = true;
        }
    }

    public void OnGuard(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    _isGuard = true;
        //}
        //else if (context.canceled)
        //{
        //    _isGuard = false;
        //}
    }

    public void OnLockon(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _isLockon = !_isLockon;
        }
    }

    public void OnLockonSelectMouse(InputAction.CallbackContext context)
    {
        _isLockonSelectMouse = context.ReadValue<Vector2>();
    }

    public void OnLockonSelectGamepod(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isLockonSelectGamepad = true;
        }
    }

    public void OnEvade(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isEvade = true;
        }
    }
}