using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Particles : MonoBehaviour
{
    public ParticleSystem heal_Particle;
    public Transform healStream;

    private void Update() => healStream.Rotate(Vector3.up * 650 * Time.deltaTime, Space.World);

}
