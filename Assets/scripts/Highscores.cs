using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
	const string privateCode = "iKcAOz0IRkis2U0TwXc8rAV_KPurRbG0-AcOAvnZHnyw";
    const string publicCode = "6179ab7c8f40bba8b4fcdb91";
    const string webURL = "https://www.dreamlo.com/lb/";

    public GameObject updatingPage;
    public GameObject scoresDisplayPage;
    public GameObject inputNamePage;
    public CanvasGroup canvasGroup;

    public SaveManagerSession sessionSave;
    public int sessionScore;

    HighscoresDisplay highscoreDisplay;
	public Highscore[] highscoresList;
	static Highscores instance;

	void Awake()
	{
		highscoreDisplay = GetComponent<HighscoresDisplay>();
		instance = this;
    }

    private void Start()
    {
        sessionScore = sessionSave.LoadScore();
        canvasGroup.LeanAlpha(1, 1);
    }

    public static void AddNewHighscore(string username, int score)
	{
		instance.StartCoroutine(instance.UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score)
	{
        using UnityWebRequest webRequest = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                //Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                //Debug.LogError("HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                break;
        }
    }

	public void DownloadHighscores()
	{
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
        using UnityWebRequest webRequest = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                //Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                //Debug.LogError("HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                FormatHighscores(webRequest.downloadHandler.text);
                highscoreDisplay.OnHighscoresDownloaded(highscoresList);

                if (updatingPage.activeInHierarchy)
                {
                    updatingPage.SetActive(false);

                    if(highscoresList.Length == 0 && sessionScore != 0)
                    {
                        inputNamePage.SetActive(true);
                        sessionScore = 0;
                        break;
                    }

                    int amount = highscoresList.Length < 10 ?  highscoresList.Length : 10;

                    for (int i = 0; i < amount; i++)
                    {
                        if(highscoresList[i].score < sessionScore)
                        {
                            inputNamePage.SetActive(true);
                            sessionScore = 0;
                            break;
                        }
                    }
                   
                    scoresDisplayPage.SetActive(!inputNamePage.activeInHierarchy);
                }

                break;
        }

    }

	void FormatHighscores(string textStream)
	{
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
        }
    }
}

public struct Highscore
{
	public string username;
	public int score;

	public Highscore(string _username, int _score)
	{
		username = _username;
		score = _score;
	}
}


