using System;
using UnityEngine;

//�U��Effect�̕\��
[Serializable]
public class AttackEffectPlay
{
    [Tooltip("�U������Effect")]
    [SerializeField]
    ParticleSystem _slashEffect;

    public void SlashEffectPlay()
    {
        _slashEffect.Play();
    }
}
