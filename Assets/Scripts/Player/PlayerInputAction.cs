using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>InputSystem���g�������͒l�̎擾�ƊǗ�</summary>
public class PlayerInputAction : MonoBehaviour
{
    [Header("���͂��󂯎���Ă��邩�̊m�F")]
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

    /// <summary>���͒l(�ړ�)</summary>
    public Vector2 InputMove => _inputMove;

    /// <summary>���͒l(��U��)</summary>
    public bool IsAttackWeak { get { return _isAttackWeak; } set { _isAttackWeak = value; } }

    /// <summary>���͒l(���U��)</summary>
    public bool IsAttackStrong { get { return _isAttackStrong; } set { _isAttackStrong = value; } }

    /// <summary>���͒l(�K�[�h)</summary>
    public bool IsGuard => _isGuard;

    /// <summary>����(���b�N�I��)</summary>
    public bool IsLockon => _isLockon;

    /// <summary>�ړ�Action</summary>
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