using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    Enemy_Components enemy_Components;
    [HideInInspector] public bool animationHasFinished;

    Living livingScript;

    void Start() => enemy_Components = GetComponent<Enemy_Components>();
    public void AnimationFinished() => animationHasFinished = true;

    public void MeleeAttack(AnimationEvent animationEvent)
    {
        int i = animationEvent.intParameter;
        bool isHitBoxEnabled = Convert.ToBoolean(animationEvent.stringParameter);

        enemy_Components.handTrails[i].emitting = isHitBoxEnabled;
        enemy_Components.handColliders[i].enabled = isHitBoxEnabled;
    }

    public void GroundSmash(AnimationEvent animationEvent)
    {
        bool isEnabled = Convert.ToBoolean(animationEvent.stringParameter);

        foreach (TrailRenderer t in enemy_Components.handTrails)
        {
            t.emitting = isEnabled;
        }

        foreach (BoxCollider b in enemy_Components.handColliders)
        {
            b.enabled = isEnabled;
        }

        if (!isEnabled)
        {
            PoolManager.Instance.ReuseObject(enemy_Components.greenGroundStompParticle, transform.position + Vector3.forward * .5f, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, 3);
            CinemachineShake.Instance.ShakeCamera(2f, .6f);

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
                      
                        livingScript = livingScript ?? GetComponentInChildren<Living>();

                        if (livingScript != collider.gameObject.GetComponent<Living>())
                        {
                            collider.gameObject.GetComponent<Living>().TakeDamage(10, true);
                        }
                    }
                }
            }

        }
    }

    public void StopSpeed(AnimationEvent animationEvent)
    {
        bool isStopped = Convert.ToBoolean(animationEvent.stringParameter);

        enemy_Components.navMeshAgent.speed = isStopped ? 0 : enemy_Components.currentEnemyData.speed;
    }

    public void ThrowAttack(AnimationEvent animationEvent)
    {
        bool isEnabled = Convert.ToBoolean(animationEvent.stringParameter);

        if(enemy_Components.currentEnemyData.enemyType == EnemyType.Blue)
        {
            enemy_Components.fireballInHand.SetActive(isEnabled);
        }
        else
        {
            enemy_Components.boneInHand.SetActive(isEnabled);
        }

        if (!isEnabled)
        {
            PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.throwingWeapon, enemy_Components.boneInHand.transform.position, transform.rotation);
        }
    }

    public void BlowUpNow()
    {
        PoolManager.Instance.ReuseObject(enemy_Components.redExpolsionParticle,transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);

        foreach (Collider collider in colliders)
        {
            Enemy_Reuse_bone_Parts enemy_Reuse_Bone_Parts = collider.GetComponent<Enemy_Reuse_bone_Parts>();

            if (enemy_Reuse_Bone_Parts != null)
            {
                enemy_Reuse_Bone_Parts.myRigidbody.AddExplosionForce(0, transform.position, 4);
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
                    collider.gameObject.GetComponent<Living>().TakeDamage(5, true);
                }
            }
        }

        if(!GamePeriodManager.isGameOver)
            CinemachineShake.Instance.ShakeCamera(5f, 1f);

        animationHasFinished = true;
        gameObject.SetActive(false);
    }
}
