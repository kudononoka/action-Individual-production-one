using UnityEngine;

/// <summary>�v���C���[�̑�܂��ȏ����ƃf�[�^�̊Ǘ�������N���X</summary>
public class PlayerController : MonoBehaviour
{
    [Header("�ݒ�")]

    [SerializeField]
    PlayerParameter _parameter;

    [Space]
    [Header("PlayerModelComponent")]

    [SerializeField]
    Animator _playerModelAnim;

    CharacterController _characterController;

    PlayerInputAction _inputAction;

    [SerializeField]
    PlayerStateMachine _stateMachine = new();

    Transform _playerTra;
    public Animator PlayerAnim => _playerModelAnim;

    public PlayerInputAction InputAction => _inputAction;
    public PlayerParameter Parameter => _parameter;
    public CharacterController CharacterController => _characterController;

    public Transform PlayerTra => _playerTra;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTra = GetComponent<Transform>();
        _inputAction = GetComponent<PlayerInputAction>();
        _stateMachine.Init(this);
    }

    void Update()
    {
        _stateMachine.OnUpdate();
    }

}