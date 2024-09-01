using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialMoveTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "WSAD or 左スティック　で　移動";
    }

    public string GetTitle()
    {
        return "移動";
    }

    public bool CheckTask()
    {
        if (_inputAction.InputMove.magnitude > 0)
        {
            return true;
        }
        return false;
    }

}

public class TutorialCameraMoveTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "マウス移動 or 右スティック　でカメラ操作";
    }

    public string GetTitle()
    {
        return "カメラ移動";
    }

    public bool CheckTask()
    {
        if (_inputAction.CameraMove.magnitude > 0)
        {
            return true;
        }
        return false;
    }

}

public class TutorialWeakAttackTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "左クリック or □ で弱攻撃ができます";
    }

    public string GetTitle()
    {
        return "基本攻撃 1 / 2";
    }

    public bool CheckTask()
    {
        if (_inputAction.IsAttackWeak)
        {
            return true;
        }
        return false;
    }

}

public class TutorialStrongAttackTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "右クリック or △ で強攻撃ができます";
    }

    public string GetTitle()
    {
        return "基本攻撃 2 / 2";
    }

    public bool CheckTask()
    {
        if (_inputAction.IsAttackStrong)
        {
            return true;
        }
        return false;
    }

}

public class TutorialAttackComboTask : ITutorialTask
{
    public float _comboPracticeTime = 8;
    public string GetDescription()
    {
        return "弱攻撃 と　強攻撃　を組み合わせて攻撃することができます\n□ + □ + △\n □ + △ + △";
    }

    public string GetTitle()
    {
        return "基本攻撃の連撃";
    }

    public bool CheckTask()
    {
        _comboPracticeTime -= Time.deltaTime;
        if(_comboPracticeTime < 0)
        {
            return true;
        }
        return false;
    }

}


public class TutorialAvoidanceTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "Space or ✕ で素早い移動をすることができ、回避にも利用できます";
    }

    public string GetTitle()
    {
        return "ステップ(回避)";
    }

    public bool CheckTask()
    {
        if (_inputAction.IsEvade)
        {
            return true;
        }
        return false;
    }

}
