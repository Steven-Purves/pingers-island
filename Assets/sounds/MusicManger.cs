using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManger : MonoBehaviour {

	public AudioClip startSong;
	public AudioClip[] songs;

	private void Start()
	{
		AudioManger.PickNewSong += PickRandomSong;

		if (startSong)
		{
			AudioManger.Instance.PlayMusic(startSong);
		}
        else
        {
			PickRandomSong();
		}
	}

	private void PickRandomSong()
    {
		AudioManger.Instance.PlayMusic(songs[Random.Range(0,songs.Length-1)]);
	}
}
