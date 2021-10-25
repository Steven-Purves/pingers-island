using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePeriodManager : MonoBehaviour
{
    public static Action<int> OnAddScore;
    public static bool isGameOver;

    public TMP_Text scoreUI;
    public TMP_Text hiScorceUI;
    public GameObject gameOver;

    private CurrentGameSaveObject currentGameData;

    private const string scoreString = "Score";
    private const string hiScoreString = "Hi Score";

    private int levelToLoad;

    void Awake()
    {
        Player.OnPlayerDied += GameOver;
        SaveManagerGame.OnSendCurrentSavedData += Init;
        OnAddScore += AddScore;
    }

    private void OnDestroy()
    {
        Player.OnPlayerDied -= GameOver;
        SaveManagerGame.OnSendCurrentSavedData -= Init;
        OnAddScore -= AddScore;
    }

    private void GameOver()
    {
        isGameOver = true;
        SaveManagerGame.OnResetGame?.Invoke(currentGameData);
        gameOver.SetActive(true);
        levelToLoad = 1;
        Invoke(nameof(FadeOut), 5);
    }

    public void LevelComplete()
    {
        SaveManagerGame.OnSaveCurrentGame?.Invoke(currentGameData);
        levelToLoad = 1;
        Invoke(nameof(FadeOut), 5);
    }

    private void FadeOut()
    {
        FadeToNext.OnFadeToLevel(levelToLoad);
    }

    private void AddScore(int points)
    {
        currentGameData.currentScore += points;
        UpdateScore(scoreUI, currentGameData.currentScore, scoreString);

        if(currentGameData.currentScore >= currentGameData.currentHighScore)
        {
            currentGameData.currentHighScore = currentGameData.currentScore;
            UpdateScore(hiScorceUI, currentGameData.currentHighScore, hiScoreString);
        }
    }

    void UpdateScore(TMP_Text score, int scoreCurrent, string scoreType)
    {
        score.text = currentGameData.currentScore > 999999 ? $"{scoreType} \n999999" : $"{scoreType} \n" + scoreCurrent.ToString("D6");

        if(scoreCurrent == 0)
        {
            score.text = scoreType;
        }
    }

    private void Init(CurrentGameSaveObject savedGameData)
    {
        currentGameData = savedGameData;
        UpdateScore(hiScorceUI, currentGameData.currentHighScore, hiScoreString);
        UpdateScore(scoreUI, currentGameData.currentScore, scoreString);
    }
}
