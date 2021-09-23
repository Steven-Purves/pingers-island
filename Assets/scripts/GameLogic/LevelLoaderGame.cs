using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoaderGame : MonoBehaviour
{
    public EnemyTracker enemyTracker;

    public void LoadALevel(int levelToLoad)
    {
        enemyTracker.StopTracking();
        SceneManager.LoadScene(levelToLoad);
    } 
}
