using Unity.VisualScripting;
using UnityEngine;

/// <summary>プレイヤーの大まかな処理とデータの管理をするクラス</summary>
public class PlayerController : MonoBehaviour, IDamage, ISlow
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
    PlayerHPController _playerHPSTController = new();

    [SerializeField]
    AttackEffectPlay _attackEffectPlay = new();

    CharacterController _characterController;

    PlayerInputAction _inputAction;

    TimeManager _timeManager;

    Transform _playerTra;

    bool _isAlive = true;

    public Animator PlayerAnim => _playerModelAnim;

    public PlayerInputAction InputAction => _inputAction;
    public PlayerParameter Parameter => _parameter;
    public CharacterController CharacterController => _characterController;

    public Transform PlayerTra => _playerTra;

    public CameraController CameraController => _cameraController;

    public PlayerHPController PlayerHPSTController => _playerHPSTController;

    public Weapon PlayerWeapon => _playerWeapon;

    public CapsuleCollider CapsuleCollider => _capsuleCollider;

    public TimeManager TimeManager => _timeManager;

    void Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _timeManager.SlowSystem.Add(this);

        _characterController = GetComponent<CharacterController>();

        _playerTra = GetComponent<Transform>();

        _inputAction = GetComponent<PlayerInputAction>();

        _stateMachine.Init(this);
        _cameraController.Init(_inputAction);
        _playerHPSTController.Init(_parameter.HPMax);

        _playerWeapon.DamageColliderEnabledSet(false);

        _isAlive = true;

    }

    private void OnDestroy()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if(timeManager != null) 
            timeManager.SlowSystem.Remove(this);
    }

    void Update()
    {
        if(_isAlive)
        {
            _stateMachine.OnUpdate();
            _cameraController.OnUpdate();
        }
    }

    public void Damage(int damage)
    {
        if(!_isAlive) return;

        AudioManager.Instance.SEPlayOneShot(SE.EnemyAttackHit);
        _isAlive = _playerHPSTController.HPDown(damage);
        if(!_isAlive)
        {
            _playerModelAnim.SetBool("IsDeath", true);
            GameManager.Instance.GameEnd(GameState.GameOver);
        }
    }

    public void AttackEffectPlay(int isChargeAttackFlag)
    {
        bool isChargeAttack = isChargeAttackFlag == 0 ? false : true;
        _attackEffectPlay.SlashEffectPlay(isChargeAttack);
    }

    public void AttackCollider(int enableFlag)
    {
        bool isEnable = enableFlag != 0 ? true : false;
        _playerWeapon.DamageColliderEnabledSet(isEnable);
    }

    public void OnSlow(float slowSpeedRate)
    {
        _playerModelAnim.speed = slowSpeedRate;
    }

    public void OffSlow()
    {
        _playerModelAnim.speed = 1;
    }
}