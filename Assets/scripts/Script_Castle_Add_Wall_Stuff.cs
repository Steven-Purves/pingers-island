using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Castle_Add_Wall_Stuff : MonoBehaviour
{
    public GameObject torch;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        float[] CastleWallStuffDirections = { 90, 180, 270 ,0};

        GameObject objectToSpawn = Random.value > 0.5f ? torch : shield;

        Instantiate(objectToSpawn, transform.position, Quaternion.Euler(0, CastleWallStuffDirections[Random.Range(0, CastleWallStuffDirections.Length)], 0), transform);
    }
}
