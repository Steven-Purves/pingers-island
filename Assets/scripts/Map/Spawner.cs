using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SpawnerUtilities
{
    public static Action OnPlayerWin;
    public GameObject enemy;
    public GameObject dirt;
    public Crate_Move crate;

    public float timeBetweenSpawns = 5;
    public int startingEnemiesNumber;

    public int enemiesRemaningToSpawn;
    float nextSpawnTime;

    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;
    bool isCamping;
    
    Vector3 _up = Vector3.up;

    private bool roundStarted;
    private bool overrideLevel;

    public MapGen map;

    private void Awake()
    {
        nextCampCheckTime = timeBetweenCampingChecks + Time.time;
        GamePeriodManager.OnUpdateEnemyCount += GetStartingNumber;
        GamePeriodManager.OnUpdateCurrentLevel += OnOverride;
        EnemyLife.OnEnemyDeath += EnemyDeath;
    }

    private void OnDestroy()
    {
        GamePeriodManager.OnUpdateCurrentLevel -= OnOverride;
        GamePeriodManager.OnUpdateEnemyCount -= GetStartingNumber;
        EnemyLife.OnEnemyDeath -= EnemyDeath;
    }

    private void StartRound()
    {
        roundStarted = true;
    }

    private void OnOverride(int levelCount)
    {
       
        if (levelCount < 5)
        {
            overrideLevel = true;
            timeBetweenSpawns = 8; 

            int newValue = GetSpawnNumber(levelCount);
            startingEnemiesNumber = newValue;
            enemiesRemaningToSpawn = newValue;

            GamePeriodManager.currentGameData.enemiesCount = newValue;

            Invoke(nameof(SpawnCrate), 2);
            Invoke(nameof(StartRound), 4);
        }
    }

    private void GetStartingNumber(int value)
    { 
        if (!overrideLevel)
        {
            timeBetweenSpawns = 4;
            startingEnemiesNumber = value;
            enemiesRemaningToSpawn = startingEnemiesNumber;

            int newValue = value + 10 +(value /4);
            GamePeriodManager.currentGameData.enemiesCount = newValue;

            Invoke(nameof(SpawnCrate), 7);
            Invoke(nameof(StartRound), 4);
        }
    }

    private void EnemyDeath()
    {
        startingEnemiesNumber--;

        if(startingEnemiesNumber <= 0)
        {
            OnPlayerWin?.Invoke();
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

            isCamping = (Vector3.Distance(Player.pTransform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = Player.pTransform.position;
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
       
        if (isCamping)
        {
            spawnTile = map.GetClosestTile(Player.pTransform.position);
        }

        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        PoolManager.Instance.ReuseObject(enemy, spawnTile, Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360.0f), 0.0f));
        PoolManager.Instance.ReuseObject(dirt, spawnTile, Quaternion.identity);
    }

    private void SpawnCrate()
    { 
        Crate_Move crate_Move = Instantiate(crate);
        crate_Move.MapGen = map;
    }
}
