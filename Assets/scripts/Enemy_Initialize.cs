using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Initialize : PoolObject
{
    EnemyStateController enemyStateController;
    Enemy_Components enemy_Components;

    public override void OnObjReuse() => Initialize();

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
        // add more
        enemy_Components.currentEnemyData = enemy_Components.enemy_Data[UnityEngine.Random.Range(0, enemy_Components.enemy_Data.Length)];
        //enemy_Components.currentEnemyData = enemy_Components.enemy_Data[enemy_Components.enemy_Data.Length-1];
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
