using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Pool_Manager_Create_The_Pool_Objects : MonoBehaviour
{
    //make scriptable object next time
    public GameObject boner;
    public GameObject crateBits;
    public int bonerAmount;

    public GameObject[] particles, whiteBones, greenBones, blueBones, redBones, bullets, pickups, droppedGuns;
    public static Class_Pool_Manager_Create_The_Pool_Objects Instance;

    void Awake()
    {
        Instance = this;

        SpawnBoner(bonerAmount);

        foreach (GameObject gameObject in particles)
        {
            PoolManager.Instance.CreatePool(gameObject, 10);
        }

        foreach (GameObject gameObject in whiteBones)
        {
            PoolManager.Instance.CreatePool(gameObject, 5);
        }

        foreach (GameObject gameObject in greenBones)
        {
            PoolManager.Instance.CreatePool(gameObject, 5);
        }

        foreach (GameObject gameObject in blueBones)
        {
            PoolManager.Instance.CreatePool(gameObject, 5);
        }

        foreach (GameObject gameObject in redBones)
        {
            PoolManager.Instance.CreatePool(gameObject, 5);
        }

        foreach(GameObject gameObject in bullets)
        {
            PoolManager.Instance.CreatePool(gameObject, 3);
        }

        foreach (GameObject gameObject in pickups)
        {
            PoolManager.Instance.CreatePool(gameObject, 2);
        }

        foreach (GameObject gameObject in droppedGuns)
        {
            PoolManager.Instance.CreatePool(gameObject, 2);
        }

        PoolManager.Instance.CreatePool(crateBits, 1);
    }
  
    public void SpawnBoner(int amount)
    {
        PoolManager.Instance.CreatePool(boner, amount);
    }
}


