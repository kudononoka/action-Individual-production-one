using UnityEngine;

public class EnemyAttackControlle : MonoBehaviour
{
    [Header("�U����")]
    [SerializeField]
    int _damage = 0;

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