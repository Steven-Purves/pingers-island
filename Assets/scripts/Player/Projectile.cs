using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolObject {

	public float speed = 35;
	public LayerMask hitMask;
	public GameObject impact;

    public int damage = 1;

	TrailRenderer trail;

	float lifeTime = 5;
	float timer;
	float skinWidth = .1f;

	public void SetSpeed (float NewSpeed)
	{
		speed = NewSpeed;
	}
	private void Awake()
	{
		trail = GetComponent<TrailRenderer>();
	}
	void Start ()
	{
		trail.Clear();
		timer = lifeTime + Time.time;

		Collider[] hitStartCols = Physics.OverlapSphere (transform.position, .1f, hitMask);

		if (hitStartCols.Length > 0) {
			OnHitObject(hitStartCols[0], transform.position);
		}
	}

	public override void OnObjReuse()
	{
		trail.Clear();
	    timer = lifeTime + Time.time;
	}
	
	void Update () {

		if (Time.time > timer)gameObject.SetActive(false);
		
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions(moveDistance);
		
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
	void CheckCollisions (float moveDistance)
	{
		Ray ray = new Ray (transform.position,transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, moveDistance + skinWidth, hitMask, QueryTriggerInteraction.Collide))
        {
            Vector3 normal = hit.normal;

            OnHitObject(hit.collider, hit.point);
        }
    }

	public virtual void OnHitObject (Collider c, Vector3 hitPoint)
	{
		IDamageable damagableObject = c.GetComponent<IDamageable>();

		
		if (damagableObject != null) {

			damagableObject.TakeHit(damage,hitPoint,transform.forward);
        }
        

        PoolManager.Instance.ReuseObject(impact, hitPoint, transform.rotation);

		gameObject.SetActive(false);
	}
}
