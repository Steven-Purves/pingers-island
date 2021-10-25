using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile_Granade : PoolObject
{
    public int blastDamage;

    private Rigidbody myRigidbody;
    private TrailRenderer trail;
    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    public override void OnObjReuse()
    {
        trail.Clear();
        myRigidbody.velocity = CalculateLaunchVelocity(CrosshairController.Instance.crosshairs.position);
        myRigidbody.maxAngularVelocity = 10;
        myRigidbody.AddTorque(transform.right * 100);

        Invoke(nameof(BlowUp), 1);
    }
    Vector3 CalculateLaunchVelocity(Vector3 target)
    {
        float height = 1f;
        float gravity = -9;

        float displacementY = (target.y - myRigidbody.position.y);
        Vector3 displacementXZ = new Vector3(target.x - myRigidbody.position.x, 0, target.z - myRigidbody.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

        Vector3 value = velocityXZ + velocityY;

        if (float.IsNaN(value.x))
        {
            return Vector3.zero;
        }

        return value;
    }

    private void BlowUp()
    {
        PoolManager.Instance.ReuseObject(Class_Pool_Manager_Create_The_Pool_Objects.Instance.particles[1], transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 4);

        foreach (Collider collider in colliders)
        {
            Living damagableObject = collider.gameObject.GetComponent<Living>();
            if (damagableObject != null)
            {
                if (damagableObject.isPlayer)
                {
                    collider.gameObject.GetComponent<Player>().TakeDamage(1, true);
                }
                else
                {
                    Living livingBeing = collider.gameObject.GetComponent<Living>();

                    livingBeing.TakeDamage(DistanceCheck(livingBeing.transform.position), true);

                }
            }

            Enemy_Reuse_bone_Parts enemy_Reuse_Bone_Parts = collider.GetComponent<Enemy_Reuse_bone_Parts>();

            if (enemy_Reuse_Bone_Parts != null)
            {
                enemy_Reuse_Bone_Parts.myRigidbody.AddExplosionForce(700, transform.position, 4);
            }
        }

        gameObject.SetActive(false);
    }

    private int DistanceCheck(Vector3 colliderPosition)
    {
        return Vector3.Distance(transform.position, colliderPosition) > 2 ? 1 : blastDamage;
    }

    private void OnDisable()
    {
        myRigidbody.velocity = new Vector3(0f, 0f, 0f);
        myRigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
    }
}
