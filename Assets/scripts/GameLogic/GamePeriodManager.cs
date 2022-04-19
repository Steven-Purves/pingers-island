using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GamePeriodManager : MonoBehaviour
{
    public SaveManagerSession saveManagerSession;
    public static Action<int> OnAddScore;
    public static Action<int> OnUpdateCurrentLevel;
    public static Action<int> OnUpdateEnemyCount;
    public static bool isGameOver;

    public AudioClip win, lose,swooshIn,swooshOut;
    public SlowDown slowdown;
    public TMP_Text scoreUI;
    public TMP_Text hiScorceUI;
    public TMP_Text GameResultText;

    public CanvasGroup inGameCanvasGroup;

    public static CurrentGameSaveObject currentGameData;

    private const string scoreString = "Score";
    private const string hiScoreString = "Hi Score";

    private int levelToLoad;

    private readonly (string cry, string cry2)[] BattleShouts = new (string cry, string cry2)[]
    {
        ("Let Battle", "Commence!"),
        ("Let Battle", "Begin!"),
        ("Destory Them", "All!"),
        ("Ready", "Begin!"),
        ("Ready", "Go!"),
    };

    private readonly string[] LoseShouts = new string[]
    {
        "Defeat!",
        "Lost!",
        "You Lose!",
        "Game Over!"
    };

    private readonly string[] VictoryShouts = new string[]
    {
        "Victory!",
        "You Win!",
        "Well Done!",
        "Round Completed"
    };

    void Awake()
    {
        isGameOver = false;
        Player.OnPlayerDied += GameOver;
        SaveManagerGame.OnSendCurrentSavedData += Init;
        Spawner.OnPlayerWin += LevelComplete;
        OnAddScore += AddScore;
    }

    private void OnDestroy()
    {
        Spawner.OnPlayerWin -= LevelComplete;
        Player.OnPlayerDied -= GameOver;
        SaveManagerGame.OnSendCurrentSavedData -= Init;
        OnAddScore -= AddScore;
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(0.75f);
        GameResultText.text = $"Round {currentGameData.currentLevel}";
        GameResultText.gameObject.SetActive(true);
        AudioManger.Instance.PlaySfx2D(swooshIn);
        yield return new WaitForSeconds(1.5f);
        AudioManger.Instance.PlaySfx2D(swooshOut);
        GameResultText.gameObject.transform.LeanScale(Vector3.zero, 0.2f).setEaseOutCubic();
        yield return new WaitForSeconds(0.2f);
        GameResultText.gameObject.SetActive(false);

        (string cry, string cry2) = BattleShouts[UnityEngine.Random.Range(0, BattleShouts.Length)];
        GameResultText.text = cry;
        GameResultText.gameObject.SetActive(true);
        AudioManger.Instance.PlaySfx2D(swooshIn);
        yield return new WaitForSeconds(1.5f);
        GameResultText.gameObject.transform.LeanScale(Vector3.zero, 0.2f).setEaseOutCubic();
        AudioManger.Instance.PlaySfx2D(swooshOut);
        yield return new WaitForSeconds(0.2f);
        GameResultText.gameObject.SetActive(false);
        GameResultText.text = cry2;
        AudioManger.Instance.PlaySfx2D(swooshIn);
        GameResultText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameResultText.gameObject.transform.LeanScale(Vector3.zero, 0.2f).setEaseOutCubic();
        AudioManger.Instance.PlaySfx2D(swooshOut);
        yield return new WaitForSeconds(0.2f);
        GameResultText.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        saveManagerSession.Save(currentGameData.currentScore);
        
        isGameOver = true;
        SaveManagerGame.OnResetGame?.Invoke(currentGameData);
        AudioManger.Instance.PlayMusic(lose);
        GameResultText.text = LoseShouts[UnityEngine.Random.Range(0, VictoryShouts.Length)];
        GameResultText.gameObject.SetActive(true);

        levelToLoad = 2;  

        Invoke(nameof(OnFadeOut), 5);
    }

    public void LevelComplete()
    {
        slowdown.SlowdownTime();
        SaveManagerGame.OnSaveCurrentGame?.Invoke(currentGameData);
        GameResultText.text = VictoryShouts[UnityEngine.Random.Range(0, VictoryShouts.Length)];
        GameResultText.gameObject.SetActive(true);
        inGameCanvasGroup.LeanAlpha(0,0.3f);
        levelToLoad = 1;
        Invoke(nameof(PlayWinSong), 1);
        Invoke(nameof(ShowStats), 4);
    }
    private void PlayWinSong()
    {
        AudioManger.Instance.PlayMusic(win);
    }

    private void ShowStats()
    {
        StatsMenu.OnShowStats?.Invoke();
        GameResultText.gameObject.transform.LeanScale(Vector3.zero, 0.2f);
    }

    public void OnFadeOut()
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

        currentGameData.currentLevel++;
        OnUpdateCurrentLevel?.Invoke(currentGameData.currentLevel);
        OnUpdateEnemyCount?.Invoke(currentGameData.enemiesCount);
        StartCoroutine(StartRound());
    }
}
