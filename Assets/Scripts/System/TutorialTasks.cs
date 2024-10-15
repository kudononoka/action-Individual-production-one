
/// <summary>移動用チュートリアル</summary>
public class TutorialMoveTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType => EnemyStateMachine.StateType.Idle;

    public string GetDescription => "WSAD or 左スティック　で　移動";

    public string GetTitle => "移動";

    public float NextTutorialTaskTime => 4f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>カメラ操作用チュートリアル</summary>
public class TutorialCameraMoveTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType  => EnemyStateMachine.StateType.Idle;

    public string GetDescription => "マウス移動 or 右スティック　でカメラ操作";

    public string GetTitle => "カメラ移動";

    public float NextTutorialTaskTime => 4f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>攻撃用チュートリアル</summary>
public class TutorialAttackTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType => EnemyStateMachine.StateType.Idle;

    public string GetDescription => "左クリック or □ で攻撃ができます";

    public string GetTitle => "基本攻撃";

    public float NextTutorialTaskTime => 6f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>コンボ攻撃用チュートリアル</summary>
public class TutorialAttackComboTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType => EnemyStateMachine.StateType.Idle;

    public string GetDescription => "攻撃 は最大４回連撃することができます";

    public string GetTitle => "基本攻撃の連撃";

    public float NextTutorialTaskTime => 8f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>ため攻撃用チュートリアル</summary>
public class TutorialChargeAttack : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType  => EnemyStateMachine.StateType.Down; 

    public string GetDescription => 
        "左クリック or □ で長押しでため攻撃ができます" +
        "\n黄色い雷電がなくなった時にボタンを離すことで高いダメージを与える攻撃ができます";

    public string GetTitle => "ため攻撃";

    public float NextTutorialTaskTime => 12f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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

/// <summary>ステップ回避用チュートリアル</summary>
public class TutorialAvoidanceTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType  => EnemyStateMachine.StateType.Idle; 

    public string GetDescription => 
        "Space or × で素早い移動をすることができ、回避にも利用できます";

    public string GetTitle => "ステップ(回避)";

    public float NextTutorialTaskTime => 4f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>ロックオン用チュートリアル</summary>
public class TutorialLockonTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType 
        => EnemyStateMachine.StateType.Idle; 

    public string GetDescription => 
        "マウスホイールクリック or 右スティック押し込み でロックオンができます" +
        "\nもう1回押すことで解除できます" +
        "\nロックオンをすることで攻撃時自動的に対象の方を向くので攻撃が当たりやすくなります";

    public string GetTitle => "ロックオン";

    public float NextTutorialTaskTime => 8f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>ロックオン選択チュートリアル</summary>
public class TutorialLockonSelectTask : ITutorialTask
{
    private PlayerInputAction _inputAction;

    EnemyStateMachine.StateType ITutorialTask.EnemyType { get => EnemyStateMachine.StateType.Idle;}

    public string GetDescription => 
        "マウスホイール or 十字キー↑ でロックオン選択ができます";

    public string GetTitle => "ロックオン選択";

    public float NextTutorialTaskTime => 3f;

    public void Init(PlayerInputAction playerInput)
    {
        _inputAction = playerInput;
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
/// <summary>チュートリアル終了</summary>
public class TutorialSuccess : ITutorialTask
{
    EnemyStateMachine.StateType ITutorialTask.EnemyType { get => EnemyStateMachine.StateType.Idle; }

    public string GetDescription => 
        "これでチュートリアル終了です。7秒後、戦闘に入ります" +
        "\n敵のHPが0になったらゲームクリア!" +
        "\nその前にPlayerのHPが0になるとゲームオーバーです。" +
        "\n頑張ってください";

    public string GetTitle => "";

    public float NextTutorialTaskTime => 7f;

    public bool CheckTask()
    {
        return true;
    }
}