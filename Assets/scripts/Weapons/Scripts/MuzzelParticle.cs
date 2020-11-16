using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzelParticle : Gun_Muzzel_Effect
{
    public GameObject muzzelparticle;
    public override void EffectShow(Transform muzzel)
    {
        PoolManager.Instance.ReuseObject(muzzelparticle, muzzel.position, muzzel.rotation);
    }
}
