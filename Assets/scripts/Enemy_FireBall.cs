using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FireBall : ReuseBone
{
    public GameObject particleExplosion;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        PoolManager.Instance.ReuseObject(particleExplosion, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);

        foreach (Collider collider in colliders)
        {
            Enemy_Reuse_bone_Parts enemy_Reuse_Bone_Parts = collider.GetComponent<Enemy_Reuse_bone_Parts>();

            if (enemy_Reuse_Bone_Parts != null)
            {
                enemy_Reuse_Bone_Parts.myRigidbody.AddExplosionForce(0, transform.position, 3);
            }

            Living damagableObject = collider.gameObject.GetComponent<Living>();
            if (damagableObject != null)
            {
                if (damagableObject.isPlayer)
                {
                    collider.gameObject.GetComponent<Player>().TakeDamage(1, true);
                }
                else
                {
                    collider.gameObject.GetComponent<Living>().TakeDamage(10,true);
                }
            }
        }

        if (!GamePeriodManager.isGameOver)
            CinemachineShake.Instance.ShakeCamera(2f, .6f);

        gameObject.SetActive(false);
    }
}
