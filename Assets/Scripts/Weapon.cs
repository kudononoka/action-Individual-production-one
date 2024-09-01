using UnityEngine;

/// <summary>武器の当たり判定を主に行う</summary>
public class Weapon : MonoBehaviour
{
    [Header("武器を持っているキャラのTag")]
    [SerializeField] string _ownerTagName;

    [Header("与えるダメージ")]
    [SerializeField]
    int _damage;

    [SerializeField]
    BoxCollider _boxCollider;

    [SerializeField]
    ParticleSystem[] _bloodParticle;

    [SerializeField]
    HitDirection _hitDirection;

    [Header("血のパーティクルの表示時間")]
    [SerializeField]
    float _particlePlayTime;

    float _particlePlayTimer = 0;

    bool _bloodParticleActive = false;

    bool _isHitJudge = false;

    /// <summary>与えるダメージ</summary>
    public int Damage { get => _damage; set => _damage = value; }

    public void Update()
    {
       if(_bloodParticleActive)　　　　//血のParticalが表示されていた場合
       {
            _particlePlayTimer += Time.deltaTime;
            if(_particlePlayTimer > _particlePlayTime)　//表示してから一定時間経過したら
            {
                _particlePlayTimer = 0;　　　　　　　　　//血非表示
                BloodParticalActive(false);
                _bloodParticleActive = false;
            }
       }

    }

    /// <summary>血のParticalの非表示と表示切り替え</summary>
    /// <param name="isActive">表示する</param>
    public void BloodParticalActive(bool isActive)
    {
        for(int i = 0; i < _bloodParticle.Length; i++)
        {
            if(isActive)
                _bloodParticle[i].Play();
            else
                _bloodParticle[i].Stop();
        }
    }

    /// <summary>ダメージ判定となるコライダーの非表示・表示切り替え</summary>
    /// <param name="isEnabled">表示する</param>
    public void DamageColliderEnabledSet(bool isEnabled)
    {
        _boxCollider.enabled = isEnabled;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_ownerTagName))       //自分以外の当たり判定を行う
        {
            _hitDirection.HitAction(other.gameObject);
            if (other.gameObject.TryGetComponent<IDamage>(out var IDamage))
            {
                IDamage.Damage(_damage);
                _bloodParticleActive = true;　　　//血Effect表示
                BloodParticalActive(true);
            }
        }
    }
}
