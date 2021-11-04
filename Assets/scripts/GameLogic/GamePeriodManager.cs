using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePeriodManager : MonoBehaviour
{
    public SaveManagerSession saveManagerSession;
    public static Action<int> OnAddScore;
    public static bool isGameOver;

    public TMP_Text scoreUI;
    public TMP_Text hiScorceUI;
    public TMP_Text GameResultText;

    public CanvasGroup inGameCanvasGroup;
    public CanvasGroup gameOverCanvasGroup;

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
        saveManagerSession.Save(currentGameData.currentScore);
        isGameOver = true;
        SaveManagerGame.OnResetGame?.Invoke(currentGameData);

        GameResultText.text = "Defeat!";
        GameResultText.gameObject.SetActive(true);
        
        levelToLoad = 2;  // this will be 2 later
        Invoke(nameof(FadeOut), 5);
    }

    public void LevelComplete()
    {
        SaveManagerGame.OnSaveCurrentGame?.Invoke(currentGameData);

        GameResultText.text = "Victory!";
        GameResultText.gameObject.SetActive(true);
        inGameCanvasGroup.LeanAlpha(0,0.3f);
        levelToLoad = 1;

        Invoke(nameof(ShowStats), 4);
    }

    private void ShowStats()
    {
        GameResultText.gameObject.transform.LeanScale(Vector3.zero, 0.2f);
        gameOverCanvasGroup.LeanAlpha(1, 0.3f);

        Invoke(nameof(FadeOut), 5);
        //stats
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
