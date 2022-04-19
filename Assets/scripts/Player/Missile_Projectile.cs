using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Projectile : Projectile
{
    public AudioClip boom;
    public int blastDamage; 

    public override void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damagableObject = c.GetComponent<Collider>().GetComponent<IDamageable>();

        if (damagableObject != null)
        {
            damagableObject.TakeHit(damage, hitPoint, transform.forward);
        }

        Explode();

        PoolManager.Instance.ReuseObject(impact, hitPoint, transform.rotation);

        gameObject.SetActive(false);
    }

    private void Explode()
    {
        AudioManger.Instance.PlaySfx2D(boom);

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
                enemy_Reuse_Bone_Parts.myRigidbody.AddExplosionForce(200, transform.position, 4);
            }
        }

        if (!GamePeriodManager.isGameOver)
            CinemachineShake.Instance.ShakeCamera(2f, .6f);
    }

    private int DistanceCheck(Vector3 colliderPosition)
    {
        return Vector3.Distance(transform.position, colliderPosition) > 2 ? 100 : blastDamage;
    }
}
