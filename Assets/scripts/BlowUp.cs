using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlowUp : MonoBehaviour
{   
    public void Break(Vector3 hitPoint, Vector3 hitDirection, float force, float radius) 
    {
        foreach (Transform child in transform)
        { 
            child.gameObject.SetActive(true);
            child.GetComponent<Rigidbody>().AddExplosionForce(force, hitPoint + hitDirection * -3, radius);
        }

        transform.DetachChildren();
    }
}
