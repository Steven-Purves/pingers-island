using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Texture2D cursorTex;
	public GameObject mainMenu;
	public GameObject optionsMenu;

	public Slider[] sliders;
 	public int[] screenWidths;

	public SaveManagerSession saveSession;

    private void Start()
	{
		saveSession.Save(0);
		Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.ForceSoftware);
		Cursor.visible = true;

		sliders[0].value = AudioManger.Instance.MasterVoloumePercent;
		sliders[1].value = AudioManger.Instance.MusicVolumePercent;
		sliders[2].value = AudioManger.Instance.SfxVolumePercent;

	}
    
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
