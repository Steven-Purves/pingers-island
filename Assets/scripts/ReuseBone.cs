using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReuseBone : PoolObject
{
    public GameObject hitEffect;
    BoneProjectile boneProjectile;
    bool hitSomething;

    void Awake()
    {
        boneProjectile = GetComponent<BoneProjectile>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Living damagableObject = collision.gameObject.GetComponent<Living>();

        if (damagableObject != null)
        {
            if (!hitSomething)
            {
                collision.gameObject.GetComponent<Living>().TakeDamage(1, true);

                PoolManager.Instance.ReuseObject(hitEffect, transform.position, Quaternion.identity);
            }
        }

        hitSomething = true;
    }

    public override void OnObjReuse()
    {
        hitSomething = false;
        boneProjectile.Launch();
    }
}
