using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Pickup : MonoBehaviour
{
    public int level = 1;

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
        print("remeber to set level for pick ups!");
       
        if(level >= 20)
        {
            pickup_Tier = Pickup_Tier.FORTH;
        }
        else if(level >= 15)
        {
            pickup_Tier = Pickup_Tier.THIRD;
        }
        else if (level >= 10)
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
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, 2)], transform.position, Quaternion.identity);
                break;
            case Pickup_Tier.SECOND:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, 4)], transform.position, Quaternion.identity);
                break;
            case Pickup_Tier.THIRD:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, 6)], transform.position, Quaternion.identity);
                break;
            case Pickup_Tier.FORTH:
                PoolManager.Instance.ReuseObject(pickups[Random.Range(0, pickups.Length)], transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
