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
    bool _isAttackWeak = false;

    [SerializeField] 
    bool _isAttackStrong = false;

    [SerializeField] 
    bool _isGuard = false;

    [SerializeField] 
    bool _isLockon = false;    

    /// <summary>入力値(移動)</summary>
    public Vector2 InputMove => _inputMove;

    /// <summary>入力値(弱攻撃)</summary>
    public bool IsAttackWeak { get { return _isAttackWeak; } set { _isAttackWeak = value; } }

    /// <summary>入力値(強攻撃)</summary>
    public bool IsAttackStrong { get { return _isAttackStrong; } set { _isAttackStrong = value; } }

    /// <summary>入力値(ガード)</summary>
    public bool IsGuard => _isGuard;

    /// <summary>入力(ロックオン)</summary>
    public bool IsLockon => _isLockon;

    /// <summary>移動Action</summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
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
        if (context.performed)
        {
            _isGuard = true;
        }
        else if (context.canceled)
        {
            _isGuard = false;
        }
    }

    public void OnLockon(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _isLockon = !_isLockon;
        }
    }
}