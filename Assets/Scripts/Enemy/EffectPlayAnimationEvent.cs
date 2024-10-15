using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayAnimationEvent : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;

    public void OnPlay()
    {
        _particleSystem.Play();
    }
}
