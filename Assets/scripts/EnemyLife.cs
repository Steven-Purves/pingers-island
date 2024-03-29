﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : Living
{
    public Enemy_Components enemy_Components;
    public float forceBlowBack = 100;
 
    public static event Action OnEnemyDeath;
   
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

        if (!dead)
        {
            GamePeriodManager.OnAddScore?.Invoke(15);
        }
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

        AudioManger.Instance.PlaySfx2D(enemy_Components.death);
        GamePeriodManager.OnAddScore?.Invoke(pointsOnDeath);
        OnEnemyDeath?.Invoke();
        
        enemy_Components.enermy_State_Methods.BlowUp(lastHitPoint, lastHitDirection, forceBlowBack, 20f);

        switch (enemy_Components.currentEnemyData.enemyType)
        {
            case EnemyType.White:
                RoundStatsPanel.instance.whiteVal++;
                break;
            case EnemyType.Green:
                RoundStatsPanel.instance.greenVal++;
                break;
            case EnemyType.Red:
                RoundStatsPanel.instance.redVal++;
                break;
            case EnemyType.Blue:
                RoundStatsPanel.instance.blueVal++;
                break;
        }

        if(!GamePeriodManager.isGameOver)
            CinemachineShake.Instance.ShakeCamera(2f, .3f);

    }
}
