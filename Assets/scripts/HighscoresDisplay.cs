using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class HighscoresDisplay : MonoBehaviour
{
	public Score[] highscoreFields;
	Highscores highscoresManager;

	void Start()
	{
		highscoresManager = GetComponent<Highscores>();
		StartCoroutine("RefreshHighscores");
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList)
	{
		for (int i = 0; i < highscoreFields.Length; i++)
		{
			
			if (i < highscoreList.Length)
			{
				highscoreFields[i].namePlayer.text = highscoreList[i].username;
				highscoreFields[i].score.text = $"{highscoreList[i].score}";
			}
		}
	}

	IEnumerator RefreshHighscores()
	{
		while (true)
		{
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}