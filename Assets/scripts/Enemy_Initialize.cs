using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Initialize : PoolObject
{
    EnemyStateController enemyStateController;
    Enemy_Components enemy_Components;

    public override void OnObjReuse()
    {
        Initialize();
        AudioManger.Instance.PlaySound(enemy_Components.digging, transform.position);
    }

    public void Awake()
    {
        enemyStateController = GetComponent<EnemyStateController>();
        enemy_Components = GetComponent<Enemy_Components>();

        Initialize();
    }

    public void Initialize()
    {
        enemyStateController.TransitionToState(enemy_Components.startState);

        SelectEnemyType();
        SetAnimatorController();
        ResetAttack();
        SetMaterials();
        ResetThrowingWeapons();
        SetStartingHealth();
    }

    private void SetAnimatorController() => enemy_Components.animator.runtimeAnimatorController = enemy_Components.currentEnemyData.runtimeAnimatorController;
    private void SetStartingHealth() => enemy_Components.enemyLife.SetStartingHealth(enemy_Components.currentEnemyData.startingHealth);


    private void SetMaterials()
    {
        enemy_Components.meshRendererBone.material = enemy_Components.currentEnemyData.material;
        enemy_Components.skinnedMeshRenderer.material = enemy_Components.currentEnemyData.material;
    }

    private void SelectEnemyType()
    {
        if (GamePeriodManager.currentGameData == null)
        {
            enemy_Components.currentEnemyData = enemy_Components.enemy_Data[UnityEngine.Random.Range(0, 0)];
            return;
        }

        int currentLevel = GamePeriodManager.currentGameData.currentLevel;
        int enemyRange = 0;

        if (currentLevel < 3)
        {
            enemyRange = 1;
        }
        else if (currentLevel < 5)
        {
            enemyRange = 2;
        }
        else if (currentLevel < 6)
        {
            enemyRange = 3;
        }
        else if (currentLevel > 6)
        {
            enemyRange = 4;
        }

        enemy_Components.currentEnemyData = enemy_Components.enemy_Data[UnityEngine.Random.Range(0, enemyRange)];
    }

    private void ResetThrowingWeapons()
    {
        enemy_Components.boneInHand.SetActive(false);
        enemy_Components.fireballInHand.SetActive(false);
    }

    private void ResetAttack()
    {
        foreach (BoxCollider handCollider in enemy_Components.handColliders)
        {
            handCollider.enabled = false;
        }

        foreach (TrailRenderer handTrail in enemy_Components.handTrails)
        {
            handTrail.emitting = false;
            handTrail.startColor = enemy_Components.currentEnemyData.trailColour;
        }

        enemy_Components.throwingBone.SetActive(false);
        enemy_Components.throwingFireball.SetActive(false);

    }
}
