using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    int _damage;

    [SerializeField]
    BoxCollider _boxCollider;

    [SerializeField]
    ParticleSystem[] _bloodParticle;

    [Header("血のパーティクルの表示時間")]
    [SerializeField]
    float _particlePlayTime;

    float _particlePlayTimer = 0;

    bool _bloodParticleActive = false;

    /// <summary>与えるダメージ</summary>
    public int Damage { get => _damage; set => _damage = value; }

    public void Update()
    {
       if(_bloodParticleActive)
        {
            _particlePlayTimer += Time.deltaTime;
            if(_particlePlayTimer > _particlePlayTime )
            {
                _particlePlayTimer = 0;
                BloodParticalActive(false);
                _bloodParticleActive = false;
            }
        }
    }

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
    /// <param name="isEnabled">表示するか</param>
    public void DamageColliderEnabledSet(bool isEnabled)
    {
        _boxCollider.enabled = isEnabled;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<IDamage>(out var IDamage))
            {
                IDamage.Damage(_damage);
                _bloodParticleActive = true;
                BloodParticalActive(true);
            }
        }
    }
}
