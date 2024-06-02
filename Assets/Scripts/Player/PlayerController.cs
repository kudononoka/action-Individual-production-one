using Unity.VisualScripting;
using UnityEngine;

/// <summary>プレイヤーの大まかな処理とデータの管理をするクラス</summary>
public class PlayerController : MonoBehaviour
{
    [Header("設定")]

    [SerializeField]
    PlayerParameter _parameter;

    [Space]
    [Header("PlayerModelComponent")]

    [SerializeField]
    Animator _playerModelAnim;

    CharacterController _characterController;

    PlayerInputAction _inputAction;

    [SerializeField]
    CameraController _cameraController;

    [SerializeField]
    PlayerStateMachine _stateMachine = new();

    PlayerHPSTController _playerHPSTController = new();

    Transform _playerTra;

    bool _isAction = false;
    public bool IsAction { get => _isAction; set => _isAction = value; }
    public Animator PlayerAnim => _playerModelAnim;

    public PlayerInputAction InputAction => _inputAction;
    public PlayerParameter Parameter => _parameter;
    public CharacterController CharacterController => _characterController;

    public Transform PlayerTra => _playerTra;

    public CameraController CameraController => _cameraController;

    public PlayerHPSTController PlayerHPSTController => _playerHPSTController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTra = GetComponent<Transform>();
        _inputAction = GetComponent<PlayerInputAction>();
        _stateMachine.Init(this);
        _cameraController.Init(_inputAction);
        _playerHPSTController.Init(_parameter.HPMax, _parameter.STMax, _parameter.StRecoverySpeed);
    }

    void Update()
    {
        _stateMachine.OnUpdate();
        _cameraController.OnUpdate();

        if(_stateMachine.CurrentState == PlayerStateMachine.StateType.Idle
            || _stateMachine.CurrentState == PlayerStateMachine.StateType.Walk)
            _playerHPSTController.RecoveryST();
    }
}