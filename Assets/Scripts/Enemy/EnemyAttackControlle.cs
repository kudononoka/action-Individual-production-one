using UnityEngine;

public class EnemyAttackControlle : MonoBehaviour
{
    int _damage = 0;

    public void SetDamageValue(int damage)
    {
        _damage = damage;
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