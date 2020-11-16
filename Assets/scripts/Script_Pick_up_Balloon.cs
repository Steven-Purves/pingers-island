using System;
using UnityEngine;

public class Script_Pick_up_Balloon : MonoBehaviour
{
    public MeshRenderer myMeshRenderer;
    public MeshRenderer stringMeshRenderer;

    public ParticleSystemRenderer fragmentSystemRenderer;
    public ParticleSystem fragmentParticleSystem;

    public Material[] materials;

    public void ResetBalloon()
    {
        Material newBallonColour = materials[UnityEngine.Random.Range(0, materials.Length)];
        myMeshRenderer.material = newBallonColour;

        fragmentSystemRenderer.material = newBallonColour;

        myMeshRenderer.enabled = true;
        stringMeshRenderer.enabled = true;
    }

    public void PopBalloon()
    {
        myMeshRenderer.enabled = false;
        stringMeshRenderer.enabled = false;

        fragmentParticleSystem.Play();
    }
}
