using System;
using UnityEngine;

//�U��Effect�̕\��
[Serializable]
public class AttackEffectPlay
{
    [Tooltip("�U������Effect")]
    [SerializeField]
    ParticleSystem[] _slashEffect;

    public void SlashEffectPlay(bool isChargeAttack)
    {
        int count = isChargeAttack ? 3 : 1;
        for (int i = 0; i < count; i++)
        {
            _slashEffect[i].Play();
        }
    }
}
