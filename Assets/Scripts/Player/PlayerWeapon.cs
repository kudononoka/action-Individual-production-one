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

    [Header("���̃p�[�e�B�N���̕\������")]
    [SerializeField]
    float _particlePlayTime;

    float _particlePlayTimer = 0;

    bool _bloodParticleActive = false;

    /// <summary>�^����_���[�W</summary>
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

    /// <summary>�_���[�W����ƂȂ�R���C�_�[�̔�\���E�\���؂�ւ�</summary>
    /// <param name="isEnabled">�\�����邩</param>
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
