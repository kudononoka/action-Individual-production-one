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

public class TutorialAttackTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "左クリック or □ で攻撃ができます";
    }

    public string GetTitle()
    {
        return "基本攻撃";
    }

    public bool CheckTask()
    {
        if (_inputAction.IsAttack)
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
        return "攻撃 は最大４回コンボで攻撃することができます";
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

    public float _avoidTime = 5;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "Space or × で素早い移動をすることができ、回避にも利用できます";
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

        _avoidTime -= Time.deltaTime;
        if (_avoidTime < 0)
        {
            return true;
        }
        return false;
    }

}

public class TutorialLockonTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "マウスホイールクリック or 右スティック押し込み でロックオンができます\nもう1回押すことで解除できます";
    }

    public string GetTitle()
    {
        return "ロックオン";
    }

    public bool CheckTask()
    {
        if (_inputAction.IsLockon)
        {
            return true;
        }
        return false;
    }

}

public class TutorialLockonSelectTask : ITutorialTask
{
    private PlayerInputAction _inputAction;
    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
    }

    public string GetDescription()
    {
        return "マウスホイール or 十字キー↑ でロックオン選択ができます";
    }

    public string GetTitle()
    {
        return "ロックオン選択";
    }

    public bool CheckTask()
    {
        if (_inputAction.IsLockonSelectGamepad || _inputAction.IsLockonSelectMouse.magnitude > 0)
        {
            return true;
        }
        return false;
    }

}
