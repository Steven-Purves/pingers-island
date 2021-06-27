using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : Living
{
    public Enemy_Components enemy_Components;
    public float forceBlowBack = 100;
 
    public static event Action<int> OnEnemyDeath;
   
    Vector3 lastHitPoint;
    Vector3 lastHitDirection;

    public void SetStartingHealth(int startingHealth)
    {
        health = startingHealth;
        dead = false;
    }

    protected override void Start() {  }
    public override void TakeHit(int damage, Vector3 _hitPoint, Vector3 _hitDirection)
    {
        PoolManager.Instance.ReuseObject(enemy_Components.hitBonerParticle, _hitPoint, Quaternion.identity);
        lastHitPoint = _hitPoint;
        lastHitDirection = _hitDirection;
        base.TakeHit(damage, _hitPoint, _hitDirection);  
    }

    public override void Die(bool enemyCausedDeath)
    {
        base.Die();

        int pointsOnDeath = enemy_Components.currentEnemyData.pointsOnKill;

        if (enemyCausedDeath)
        {
            pointsOnDeath = 0;
            lastHitPoint = transform.position;
            lastHitDirection = Vector3.zero;
        }

        CinemachineShake.Instance.ShakeCamera(2f, .3f);

        OnEnemyDeath?.Invoke(pointsOnDeath);
        enemy_Components.enermy_State_Methods.BlowUp(lastHitPoint, lastHitDirection, forceBlowBack, 20f);
    }
}
