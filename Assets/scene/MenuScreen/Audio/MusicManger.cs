using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManger : MonoBehaviour {

	public AudioClip mainTheme;
	public AudioClip menuTheme;

	private void Start()
	{
		AudioManger.Instance.PlayMusic(mainTheme, 2);
	}


}
