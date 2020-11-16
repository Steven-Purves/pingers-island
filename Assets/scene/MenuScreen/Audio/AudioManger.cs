using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public enum AudioChannel { Master, Sfx, Music };
    
    public float MasterVoloumePercent {get; private set;}
    public float SfxVolumePercent { get; private set; }
    public float MusicVolumePercent { get; private set; }

    int activeMusicSourceIndex;

    AudioSource[] musicSources;
    AudioSource sfx2DSource;
    
    public static AudioManger Instance;
    

    public Transform audioListener;
    Transform player;

    SoundFXLibrary library;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else{
           // DontDestroyOnLoad(gameObject);
            Instance = this;
            musicSources = new AudioSource[2];

            library = GetComponent<SoundFXLibrary>();
            audioListener = GetComponentInChildren<Transform>();

			if (FindObjectOfType<Player>() != null)
			{
				player = FindObjectOfType<Player>().transform;
			}
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = this.transform;
               
            }
            GameObject newSFXSource = new GameObject("2D soundFX Source");
            sfx2DSource = newSFXSource.AddComponent<AudioSource>();
            newSFXSource.transform.parent = this.transform;

            MasterVoloumePercent = PlayerPrefs.GetFloat("master vol", 1); 
            MusicVolumePercent = PlayerPrefs.GetFloat("music vol", 1); 
            SfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1); 
        }
    }
    private void Update()
    {
        if(player != null)
        {
            audioListener.position = player.position;
        }
    }

    public void SetVoloume(float volume,AudioChannel channel)
    {
		
        switch (channel)
        {
            case AudioChannel.Master:
                MasterVoloumePercent = volume;
                break;
            case AudioChannel.Music:
                MusicVolumePercent = volume;
                break;
            case AudioChannel.Sfx:
                SfxVolumePercent = volume;
                break;
        }
        musicSources[0].volume = MusicVolumePercent * MasterVoloumePercent;
        musicSources[1].volume = MusicVolumePercent * MasterVoloumePercent;

        PlayerPrefs.SetFloat("master vol", MasterVoloumePercent); 
        PlayerPrefs.SetFloat("music vol", MusicVolumePercent); 
        PlayerPrefs.SetFloat("sfx vol", SfxVolumePercent); 
        PlayerPrefs.Save();

    }
    
    public void PlayMusic(AudioClip clip,float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;

        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(MusicCrossFade(fadeDuration));

    }
    public void PlaySfx2D(string name)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(name), SfxVolumePercent * MasterVoloumePercent);
    }

    IEnumerator MusicCrossFade(float d)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / d;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp (0, MusicVolumePercent * MasterVoloumePercent, percent); 
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp (MusicVolumePercent * MasterVoloumePercent,0 , percent);
            yield return null;
        }
    }
    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(soundName), pos);
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, SfxVolumePercent * MasterVoloumePercent);
        }
    }

}
