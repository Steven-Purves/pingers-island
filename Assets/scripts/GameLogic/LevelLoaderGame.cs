using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoaderGame : MonoBehaviour
{
    EnemyTracker enemyTracker;
    public void Start()
    {
        enemyTracker = GetComponent<EnemyTracker>();
    }
    public void LoadALevel(int levelToLoad)
    {
        enemyTracker.StopTracking();
        SceneManager.LoadScene(levelToLoad);
    } 
}
