using System;
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
    public static Action PickNewSong;

    public Transform audioListener;
    Transform player;

    SoundFXLibrary library;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
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

            MasterVoloumePercent = PlayerPrefs.GetFloat("master vol", 0.5f); 
            MusicVolumePercent = PlayerPrefs.GetFloat("music vol", 0.5f); 
            SfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 0.5f); 
        }
    }
    private void Update()
    {
        if(player != null)
        {
            audioListener.position = player.position;
        }

        if (!musicSources[0].isPlaying && !musicSources[1].isPlaying)
        {
            PickNewSong?.Invoke();
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
    
    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;

        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(MusicCrossFade(fadeDuration));
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
            musicSources[1 - activeMusicSourceIndex].Stop();
        }
    }

    public void PlaySfx2D(string name)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(name), SfxVolumePercent * MasterVoloumePercent);
    }

    public void PlaySfx2D(AudioClip clip)
    {
        if (clip != null)
        {
            sfx2DSource.PlayOneShot(clip, SfxVolumePercent * MasterVoloumePercent);
        }
    }

    public void PlaySfx2DWithDelay(AudioClip clip, float delayTime)
    {
        StartCoroutine(Delay(clip, delayTime));
    }

    public IEnumerator Delay(AudioClip clip, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        PlaySfx2D(clip);
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

    public void PlayMenuSound(AudioClip clip)
    {
        if (clip != null)
        {
            sfx2DSource.PlayOneShot(clip, SfxVolumePercent * MasterVoloumePercent);
        }
    }
}
