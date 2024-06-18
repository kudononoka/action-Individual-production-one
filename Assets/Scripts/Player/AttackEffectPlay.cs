using System;
using UnityEngine;

[Serializable]
public class AttackEffectPlay
{
    [Tooltip("UŒ‚‚ÌEffect")]
    [SerializeField]
    ParticleSystem _slashEffect;

    public void SlashEffectPlay()
    {
        _slashEffect.Play();
    }
}
