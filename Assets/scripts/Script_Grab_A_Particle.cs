using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Grab_A_Particle : MonoBehaviour
{
    public ParticleSystem myParticleSystem;

    private void OnEnable()
    {
        myParticleSystem.Play();
    }
}
