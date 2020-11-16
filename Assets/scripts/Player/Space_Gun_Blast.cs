using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Gun_Blast : Gun_Fire
{
    public GameObject lazerBeam;

    public override void ShootBullet()
    {
        base.ShootBullet();

        PoolManager.Instance.ReuseObject(lazerBeam, muzzel[0].position, transform.rotation);
    }
}
