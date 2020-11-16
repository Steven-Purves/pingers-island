using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_Projectile : Projectile
{
    public override void OnHitObject(Collider c, Vector3 hitPoint)
	{
		IDamageable damagableObject = c.GetComponent<Collider>().GetComponent<IDamageable>();

		if (damagableObject != null)
		{
			damagableObject.TakeHit(damage, hitPoint, transform.forward);
			PoolManager.Instance.ReuseObject(impact, hitPoint, transform.rotation);
		}

		PoolManager.Instance.ReuseObject(Class_Pool_Manager_Create_The_Pool_Objects.Instance.particles[14], hitPoint, transform.rotation);
	}
}
