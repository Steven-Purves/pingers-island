using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate_Smash : PoolObject
{
    public float explosionForce = 50;
    public float explosionRadius = 5;

    Vector3 zero = new Vector3(0, 0, 0);
    List<Tuple<Transform, Rigidbody, Material>> boxFragments = new List<Tuple<Transform, Rigidbody, Material>>();
    readonly string fade = "Vector1_59D97A6A";
    public int itemCount;

    void Awake()
    {
        itemCount = transform.childCount;

        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            Material mat = child.GetComponent<Renderer>().material;
            boxFragments.Add(new Tuple<Transform, Rigidbody, Material>(child, rb, mat));
        }
    }
 
    IEnumerator FadeWreckage()
    {
        float transparency = 0;

        yield return new WaitForSeconds(2);

        while (transparency < 1)
        {
            transparency += 0.003f;

            for (int i = 0; i < itemCount; i++)
            {
                boxFragments[i].Item3.SetFloat(fade, transparency);
            }

            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < itemCount; i++)
        {
           boxFragments[i].Item1.gameObject.SetActive(false);
           boxFragments[i].Item3.SetFloat(fade, 0);
        }

    }
    public override void OnObjReuse()
    {
        for (int i = 0; i < itemCount; i++)
        {
            boxFragments[i].Item1.localPosition = zero;
            boxFragments[i].Item1.rotation = Quaternion.identity;
            boxFragments[i].Item1.gameObject.SetActive(true);
            boxFragments[i].Item2.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
        PoolManager.Instance.ReuseObject(Class_Pool_Manager_Create_The_Pool_Objects.Instance.particles[17], transform.position, Quaternion.identity);
        StartCoroutine(FadeWreckage());
    }  
}
