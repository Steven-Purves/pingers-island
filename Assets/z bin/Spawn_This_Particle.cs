using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_This_Particle : MonoBehaviour
{
    public GameObject particleToPlay;

    public void SpawnYourParticle(Vector3 position, Quaternion quaternion)
    {
        PoolManager.Instance.ReuseObject(particleToPlay, position, quaternion);
    }
}
