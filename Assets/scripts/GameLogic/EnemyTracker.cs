using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    int enemiesLeft;
    GameScore gameScore;
    GamePeriod gamePeriod;

    void Start()
    {
        //EnemyLife.OnEnemyDeath += EnemyDeath;
        enemiesLeft = GetComponent<Spawner>().NumberOfEnemies;
        gameScore = GetComponent<GameScore>();
        gamePeriod = GetComponent<GamePeriod>();
    }
    public void StopTracking()
    {
        EnemyLife.OnEnemyDeath -= EnemyDeath;
    }
    void EnemyDeath(int _points)
    {
      
        gameScore.AddPoints(_points);
        //add kill steak here 

        enemiesLeft--;
        if(enemiesLeft == 0)
        {
            gamePeriod.levelComplete();
      
        }
    }
}
