using UnityEngine;

public class EnemyAttackControlle : MonoBehaviour
{
    [Header("çUåÇóÕ")]
    [SerializeField]
    int _damage = 0;

    [SerializeField]
    ParticleSystem _attackSineEffect;

    public void AttackSign()
    {
        _attackSineEffect.Play();
        AudioManager.Instance.SEPlayOneShot(SE.EnemyAttackSign);
    }

    public void Attack()
    {
        AudioManager.Instance.SEPlayOneShot(SE.EnemyAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<IDamage>(out var IDamage))
            {
                IDamage.Damage(_damage);
            }
        }
    }
}