using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Pickup : MonoBehaviour
{
    public GameObject[] pickups;
    private Pickup_Tier pickup_Tier;

    private enum Pickup_Tier 
    {
        FIRST,
        SECOND,
        THIRD,
        FORTH
    }

    private void Start()
    {
        int level = GamePeriodManager.currentGameData.currentLevel;

        if(level >= 5)
        {
            pickup_Tier = Pickup_Tier.FORTH;
        }
        else if(level >= 4)
        {
            pickup_Tier = Pickup_Tier.THIRD;
        }
        else if (level >= 2)
        {
            pickup_Tier = Pickup_Tier.SECOND;
        }
        else 
        {
            pickup_Tier = Pickup_Tier.FIRST;
        }
    }

    public void SpawnPickup()
    {
        switch (pickup_Tier)
        {
            case Pickup_Tier.FIRST:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, 4)], transform.position, Quaternion.identity);
                break;
            case Pickup_Tier.SECOND:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, pickups.Length)], transform.position, Quaternion.identity);
                break;
            case Pickup_Tier.THIRD:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, pickups.Length)], transform.position, Quaternion.identity);
                break;
            case Pickup_Tier.FORTH:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, pickups.Length)], transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
