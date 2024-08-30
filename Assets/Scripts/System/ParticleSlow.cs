using UnityEngine;

public class ParticleSlow : MonoBehaviour, ISlow
{
    ParticleSystem _self;

    private void Start()
    {
        _self = GetComponent<ParticleSystem>();
    }

    public void OnSlow(float slowSpeedRate)
    {
        var main = _self.main;
        main.simulationSpeed = slowSpeedRate;
    }

    public void OffSlow()
    {
        var main = _self.main;
        main.simulationSpeed = 1;
    }
}
