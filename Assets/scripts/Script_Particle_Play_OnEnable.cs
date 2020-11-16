using UnityEngine;

public class Script_Particle_Play_OnEnable : PoolObject
{
    ParticleSystem myParticleSystem;

    float particleTime;

    void Awake()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        particleTime = myParticleSystem.main.duration;
    }
    public override void OnObjReuse()
    {
        myParticleSystem.Play();
        Invoke(nameof(TurnOff), particleTime);
    }

    private void TurnOff()
    {
        myParticleSystem.Stop();
        Invoke(nameof(SwitchOff), 2);

    }
    private void SwitchOff()
    {
        gameObject.SetActive(false);
    }
}
