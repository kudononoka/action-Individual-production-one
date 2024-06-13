using Unity.VisualScripting;
using UnityEngine;

/// <summary>プレイヤーの大まかな処理とデータの管理をするクラス</summary>
public class PlayerController : MonoBehaviour, IDamage
{
    [Header("設定")]

    [SerializeField]
    PlayerParameter _parameter;

    [Space]
    [Header("PlayerComponent")]

    [SerializeField]
    Animator _playerModelAnim;

    [SerializeField]
    Weapon _playerWeapon;

    [SerializeField]
    CameraController _cameraController;

    [SerializeField]
    PlayerStateMachine _stateMachine = new();

    [SerializeField]
    CapsuleCollider _capsuleCollider;

    [SerializeField]
    MakeASound _makeASound;

    [SerializeField]
    PlayerHPSTController _playerHPSTController = new();

    CharacterController _characterController;

    PlayerInputAction _inputAction;

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

    public Weapon PlayerWeapon => _playerWeapon;

    public CapsuleCollider CapsuleCollider => _capsuleCollider;

    public MakeASound MakeASound => _makeASound;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTra = GetComponent<Transform>();
        _inputAction = GetComponent<PlayerInputAction>();
        _stateMachine.Init(this);
        _cameraController.Init(_inputAction);
        _playerHPSTController.Init(_parameter.HPMax, _parameter.STMax, _parameter.StRecoverySpeed);
        _playerWeapon.DamageColliderEnabledSet(false);
    }

    void Update()
    {
        _stateMachine.OnUpdate();
        _cameraController.OnUpdate();

        if(_stateMachine.CurrentState == PlayerStateMachine.StateType.Idle
            || _stateMachine.CurrentState == PlayerStateMachine.StateType.Walk)
            _playerHPSTController.RecoveryST();
    }

    public void Damage(int damage)
    {
        if(!_playerHPSTController.HPDown(damage))
        {
            GameManager.Instance.ChangeScene(SceneState.GameOver);
        }
    }
}