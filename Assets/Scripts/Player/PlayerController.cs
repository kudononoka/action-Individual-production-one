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

    Transform _playerTra;
    public Animator PlayerAnim => _playerModelAnim;

    public PlayerInputAction InputAction => _inputAction;
    public PlayerParameter Parameter => _parameter;
    public CharacterController CharacterController => _characterController;

    public Transform PlayerTra => _playerTra;

    public CameraController CameraController => _cameraController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTra = GetComponent<Transform>();
        _inputAction = GetComponent<PlayerInputAction>();
        _stateMachine.Init(this);
        _cameraController.Init(_inputAction);
    }

    void Update()
    {
        _stateMachine.OnUpdate();
        _cameraController.OnUpdate();
    }
}