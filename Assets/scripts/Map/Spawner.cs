using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : TrackPlayer
{
    public GamePeriodManager gamePeriodManager;
    public GameObject enemy;
    public GameObject dirt;
    public Crate_Move crate;

    float timeBetweenSpawns = 3;
    public int startingEnemiesNumber;

    int enemiesRemaningToSpawn;
    float nextSpawnTime;

    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;
    bool isCamping;

    Vector3 _up = Vector3.up;

    private bool roundStarted;

    public MapGen map;

    void Start()
    {
        EnemyLife.OnEnemyDeath += EnemyDeath;

        enemiesRemaningToSpawn = startingEnemiesNumber;
        nextCampCheckTime = timeBetweenCampingChecks + Time.time;

        Invoke(nameof(SpawnCrate), 7);
        Invoke(nameof(StartRound), 3);
    }

    private void StartRound()
    {
        roundStarted = true;
    }

    private void EnemyDeath()
    {
        startingEnemiesNumber--;

        if(startingEnemiesNumber <= 0)
        {
            gamePeriodManager.LevelComplete();
        }
    }

    void Update()
    {
        if (GamePeriodManager.isGameOver || !roundStarted)
        {
            return;
        }

        if (Time.time > nextCampCheckTime)
        {
            nextCampCheckTime = Time.time + timeBetweenCampingChecks;

            isCamping = (Vector3.Distance(playerTransform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = playerTransform.position;
        }

        if (enemiesRemaningToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemaningToSpawn--;
            nextSpawnTime = Time.time + timeBetweenSpawns;

            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        float spawnDelay = 1;

        Vector3 spawnTile = map.GetRandomOpenTile().position;
       
        if (isCamping && enemiesRemaningToSpawn < startingEnemiesNumber - 3)
        {
            spawnTile = map.GetClosestTile(playerTransform.position);
        }

        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        PoolManager.Instance.ReuseObject(enemy, spawnTile, Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
        PoolManager.Instance.ReuseObject(dirt, spawnTile, Quaternion.identity);
    }

    private void SpawnCrate()
    { 
        Crate_Move crate_Move = Instantiate(crate);
        crate_Move.MapGen = map;
    }

    private void OnDestroy()
    {
        EnemyLife.OnEnemyDeath -= EnemyDeath;
    }

}
