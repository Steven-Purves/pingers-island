using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boner_Components : MonoBehaviour
{
    public Vector2 throwTime;
    public GameObject dirtPrefab;
    public GameObject rangedWeapon;
    public GameObject boneInHand;


    public void Start()
    {
        PoolManager.Instance.ReuseObject(dirtPrefab, transform.position, Quaternion.identity);
    }
}
