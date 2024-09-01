using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonRange : MonoBehaviour
{
    [SerializeField]
    /// <summary>ƒƒbƒNƒIƒ“‚Å‚«‚é”ÍˆÍ“à‚ÌEnemy</summary>
    List<EnemyAI> _enemiesInRange = new List<EnemyAI>();

    public List<EnemyAI> EnemiesInRange => _enemiesInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyAI>(out var enemy))
        {
            _enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyAI>(out var enemy))
        {
            _enemiesInRange.Remove(enemy);
        }
    }
}
