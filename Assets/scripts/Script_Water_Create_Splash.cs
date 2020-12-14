using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Water_Create_Splash : MonoBehaviour
{
    public GameObject bigSplash;
    public GameObject smallSplash;

    public void Awake()
    {
         PoolManager.Instance.CreatePool(smallSplash, 5);
    }

    private void OnTriggerEnter(Collider c)
    {
        IDamageable damageable = c.GetComponent<IDamageable>();

        Vector3 position = new Vector3(c.transform.position.x, transform.position.y, c.transform.position.z);

        if (damageable != null)
        {
            damageable.TakeHit(100, true);
            Instantiate(bigSplash, position, Quaternion.identity);
        }
        else
        {
            PoolManager.Instance.ReuseObject(smallSplash, position, Quaternion.identity);
            c.gameObject.SetActive(false);
        }
    }
}
