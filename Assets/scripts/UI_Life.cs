﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Life : MonoBehaviour
{
    public UI_LifeItem lifePrefab;
    public UI_LifeItem[] lifeItems;
    public Living player;

    private int health; 
    private int maxHealth;
   
    void Start()
    {
        health = player.startingHealth;
        maxHealth = player.startingHealth;
        StartCoroutine(SpawnLives());

        Player.OnPlayerHit += PlayerHit;
        Player.OnPlayerEatChicken += PlayerEatsChicken;
    }

    IEnumerator SpawnLives()
    {
        lifeItems = new UI_LifeItem[health]; 

        for (int i = 0; i < health; i++)
        {
            UI_LifeItem newLife = Instantiate(lifePrefab, transform);
            lifeItems[i] = newLife;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void PlayerHit()
    {
        health--;
        lifeItems[health].Changed(true);
    }

    private void PlayerEatsChicken()
    {
        if (health + 1 >= maxHealth)
            return;

        lifeItems[health].Changed(false);
        health++;
    }

    private void OnDestroy()
    {
        Player.OnPlayerHit -= PlayerHit;
        Player.OnPlayerEatChicken -= PlayerEatsChicken;
    }
}