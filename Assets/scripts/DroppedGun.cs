using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroppedGun : PoolObject
{ 
	public float forceMin;
	public float forceMax;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	// Start is called before the first frame update
	public override void OnObjReuse()
    {
		rb.velocity = Vector3.zero;

		float force = Random.Range(forceMin, forceMax);

		rb.AddTorque(Random.insideUnitSphere * force);
		rb.AddForce(transform.right * force);
	}
}
