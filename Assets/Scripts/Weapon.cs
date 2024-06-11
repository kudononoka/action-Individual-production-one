using UnityEngine;

/// <summary>����̓����蔻�����ɍs��</summary>
public class Weapon : MonoBehaviour
{
    [Header("����������Ă���L������Tag")]
    [SerializeField] string _ownerTagName;

    [Header("�^����_���[�W")]
    [SerializeField]
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
       if(_bloodParticleActive)�@�@�@�@//����Partical���\������Ă����ꍇ
        {
            _particlePlayTimer += Time.deltaTime;
            if(_particlePlayTimer > _particlePlayTime)�@//�\�����Ă����莞�Ԍo�߂�����
            {
                _particlePlayTimer = 0;�@�@�@�@�@�@�@�@�@//����\��
                BloodParticalActive(false);
                _bloodParticleActive = false;
            }
        }
    }

    /// <summary>����Partical�̔�\���ƕ\���؂�ւ�</summary>
    /// <param name="isActive">�\������</param>
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
    /// <param name="isEnabled">�\������</param>
    public void DamageColliderEnabledSet(bool isEnabled)
    {
        _boxCollider.enabled = isEnabled;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_ownerTagName))       //�����ȊO�̓����蔻����s��
        {
            if (other.gameObject.TryGetComponent<IDamage>(out var IDamage))
            {
                IDamage.Damage(_damage);
                _bloodParticleActive = true;�@�@�@//���\��
                BloodParticalActive(true);
            }
        }
    }
}
