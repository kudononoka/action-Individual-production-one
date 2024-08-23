using System;
using UnityEngine;

//UŒ‚Effect‚Ì•\¦
[Serializable]
public class AttackEffectPlay
{
    [Tooltip("UŒ‚‚ÌEffect")]
    [SerializeField]
    ParticleSystem[] _slashEffect;

    public void SlashEffectPlay()
    {
        foreach (ParticleSystem p in _slashEffect)
        {
            p.Play();
        }
    }
}
