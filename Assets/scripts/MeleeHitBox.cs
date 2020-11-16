using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBox : MonoBehaviour
{
    public Enemy_Components enemy_Components;
    public BoxCollider boxCollider;
    void OnTriggerEnter(Collider other)
    {
        Living damagableObject = other.gameObject.GetComponent<Living>();
        if (damagableObject != null)
        {
            other.gameObject.GetComponent<Player>().TakeDamage(1,true);

            PoolManager.Instance.ReuseObject(enemy_Components.hitPingersParticle, transform.position, Quaternion.identity);
            boxCollider.enabled = false;
        }
    }
}
