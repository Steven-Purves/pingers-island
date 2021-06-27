using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePeriod : MonoBehaviour
{
    LevelLoaderGame levelLoaderGame;
    RoundUI roundUI;

    public static bool isGameOver;

    public MyData data;  
    void Awake()
    {
        roundUI = GetComponent<RoundUI>();
        levelLoaderGame = GetComponent<LevelLoaderGame>();
        Player.OnPlayerDied += GameOver;

        StartGame();
    }
    private void StartGame()
    {
        if (data.roundNumber == 0)  // move to round ui at start
        {
            data.roundNumber++;
        }
    }
    public void levelComplete()
    {
        data.roundNumber++;
        levelLoaderGame.LoadALevel(1);
    }
    private void GameOver()
    {
        isGameOver = true;
        roundUI.SetGameObjectUI(data);
    }

}
