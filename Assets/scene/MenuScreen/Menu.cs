using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject optionsMenu;

	public Toggle fullScreen;
	public Slider[] sliders;
	public Toggle[] toggles;

 	public int[] screenWidths;

	public SaveManagerSession saveSession;

	private void Start()
	{
		saveSession.Save(0);

		Cursor.visible = true;
		activeScreenResIndex = PlayerPrefs.GetInt("screen res index");


		bool isFullscreen = true;
		if (PlayerPrefs.HasKey("fullscreen"))
		{
			
			isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
		}


		sliders[0].value = AudioManger.Instance.MasterVoloumePercent;
		sliders[1].value = AudioManger.Instance.MusicVolumePercent;
		sliders[2].value = AudioManger.Instance.SfxVolumePercent;

		for (int i = 0; i < toggles.Length; i ++) 
		{
			toggles[i].isOn = i == activeScreenResIndex;
		}

		fullScreen.isOn = isFullscreen;
		SetFullScreen(isFullscreen);
	}

	int activeScreenResIndex;
    
	public void Play()
	{
		SceneManager.LoadScene(1);
	}
	public void HighScores()
    {
		SceneManager.LoadScene(2);
    }
	public void Quit()
	{
		Application.Quit();
	}
	public void OptionsMenu()
	{
		mainMenu.SetActive(false);
		optionsMenu.SetActive(true);
	}
	public void MainMenu()
	{
		mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
	}
	public void SetScreenRes(int i)
	{
		if(toggles[i].isOn)
		{
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			Screen.SetResolution(screenWidths[i],(int)(screenWidths[i] / aspectRatio), false);
			PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
			PlayerPrefs.Save();
		}
		
	}
	public void SetFullScreen(bool b)
    {
		
		for (int i = 0; i < toggles.Length; i ++)
		{
			toggles[i].interactable = !b; 
		}
		if (b)
		{
			Resolution[] ScrResolutions = Screen.resolutions;
			Resolution maxRes = ScrResolutions[ScrResolutions.Length - 1];
			Screen.SetResolution(maxRes.width, maxRes.height, true);
		}
		else
		{
			SetScreenRes(activeScreenResIndex);
		}
		PlayerPrefs.SetInt("fullscreen", ((b) ? 1 : 0));
		PlayerPrefs.Save();
    }
	public void SetMasterVol(float value)
	{
		AudioManger.Instance.SetVoloume(value, AudioManger.AudioChannel.Master);
	}
	public void SetMusicVol(float value)
    {
		AudioManger.Instance.SetVoloume(value, AudioManger.AudioChannel.Music);
    }
	public void SetSfxVol(float value)
    {
		AudioManger.Instance.SetVoloume(value, AudioManger.AudioChannel.Sfx);
    }
}
