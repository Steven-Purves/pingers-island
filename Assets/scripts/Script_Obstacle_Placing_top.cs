using UnityEngine;

public class Script_Obstacle_Placing_top : MonoBehaviour
{
    public float heightToSpawnObstacle;
    public GameObject[] obstaclesToSpawn;
    public GameObject flagPole;
    public GameObject[] castleWallObjects;
    public GameObject[] flowers;
    public float flowerOffSet;

    public float ShiftObjectOnSpawn;

    public bool isCastle;
        
    public bool hasTopObstacle;

    void Start()
    {
        SpawnObstacleOnTop();

        if (isCastle)
        {
            SpawnCastleStuff();
        }
        else
        {
            SpawnFlowers();
        }
    }

    void SpawnObstacleOnTop()
    {
        if (Random.value > 0.5f)
        {
            float yDir = Random.Range(0, 360);

            if (isCastle)
            {
                float[] yDirChoice = { 0, 90, 180, 270 };

                yDir = yDirChoice[Random.Range(0, yDirChoice.Length)];
            }

            Instantiate(obstaclesToSpawn[Random.Range(0,obstaclesToSpawn.Length)],
                new Vector3(transform.position.x + Random.Range(-ShiftObjectOnSpawn, ShiftObjectOnSpawn),
                heightToSpawnObstacle, transform.position.z + Random.Range(-ShiftObjectOnSpawn, ShiftObjectOnSpawn)),
                Quaternion.Euler(0, yDir, 0), transform);

            hasTopObstacle = true;
        }
    }

    void SpawnCastleStuff()
    {
        if (hasTopObstacle)
        {
            bool spawnFlag = (Random.value > 0.65f);

            if (spawnFlag)
            {
                Instantiate(flagPole, transform.position + Vector3.up * (heightToSpawnObstacle + 0.5f), Quaternion.identity, transform);
            }

        }

        bool spawnWallStuff = (Random.value > 0.65f);

        if (spawnWallStuff)
        {
            float[] CastleWallStuffDirections = { 90, 180, 270 };

            Instantiate(castleWallObjects[Random.Range(0, castleWallObjects.Length)], transform.position, Quaternion.Euler(0, CastleWallStuffDirections[Random.Range(0, CastleWallStuffDirections.Length)], 0), transform);
        }
    }

    void SpawnFlowers()
    {
        bool spawnFlowers = (Random.value > 0.015f);

        int randomAmount = Random.Range(0, 4);

        if (spawnFlowers)
        { 
            for (int i = 0; i < randomAmount; i++)
            {
                bool spawnFlower = (Random.value > 0.15f);

                if (spawnFlower && !hasTopObstacle)
                {
                  
                    Instantiate(flowers[Random.Range(0, flowers.Length)], new Vector3(transform.position.x + Random.Range(-flowerOffSet, flowerOffSet), heightToSpawnObstacle, transform.position.z + Random.Range(-flowerOffSet, flowerOffSet)), Quaternion.Euler(0, Random.Range(0, 360), 0), transform);  
                }
            }
        }
    }
}
