using UnityEngine;

public class SpawnBarrel : MonoBehaviour
{
    public GameObject barrel;
    void Start()
    {
        if (Random.value > 0.7f) 
        {
            Instantiate(barrel, transform.position + Vector3.up * 4, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
        }
    }
}
