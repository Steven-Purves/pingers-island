using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy_State_Methods : TrackPlayer
{
    public Enemy_Components enemy_Components;
    private bool visible;
    public bool throwAttack;
    public bool Visible { set => visible = value; }


    public IEnumerator UpdateNavMeshRefreshRate()
    {
        while (enemy_Components.enemyStateController.currentState == enemy_Components.chaseState)
        {
            enemy_Components.navMeshAgent.SetDestination(enemy_Components.playerTransform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator ThrowWeaponTimer()
    {
        while (!throwAttack)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(3,5));
         
            if (visible)
            {
                throwAttack = true;
            }
        }
    }

    public void TurnTowardsPlayer()
    {
        Vector3 targetDirection = enemy_Components.playerTransform.position - transform.position;
        float singleStep = 10 * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }  

    public void BlowUp(Vector3 hitPoint, Vector3 hitDirection, float force, float radius)
    {
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.deathExplosion, transform.position, Quaternion.identity);

        this.gameObject.SetActive(false);

        Collider[] colliders = Physics.OverlapSphere(hitPoint, radius);

        foreach (Collider collider in colliders)
        {
            
            Enemy_Reuse_bone_Parts enemy_Reuse_Bone_Parts = collider.GetComponent<Enemy_Reuse_bone_Parts>();

            if (enemy_Reuse_Bone_Parts != null)
            {
                enemy_Reuse_Bone_Parts.myRigidbody.AddExplosionForce(force, hitPoint + hitDirection * -3, radius);
            }

        }

    }

    private void OnDisable()
    {
        SpawnBones();
    }

    private void SpawnBones()
    {
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.explosionBones[0], transform.position + Vector3.up * 2.5f, transform.rotation);
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.explosionBones[1], transform.position + Vector3.up * 1.5f, transform.rotation);
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.explosionBones[2], (transform.position + Vector3.up * 2.6f) + (Vector3.right * 0.6f), transform.rotation);
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.explosionBones[3], (transform.position + Vector3.up * 2.6f) + (Vector3.left * 0.6f), transform.rotation);
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.explosionBones[4], (transform.position + Vector3.up * 1.5f) + (Vector3.right * 0.3f), transform.rotation);
        PoolManager.Instance.ReuseObject(enemy_Components.currentEnemyData.explosionBones[5], (transform.position + Vector3.up * 1.5f) + (Vector3.left * 0.3f), transform.rotation);
    }

}
