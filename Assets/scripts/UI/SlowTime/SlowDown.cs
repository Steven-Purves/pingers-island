using System.Collections;
using UnityEngine;

public class SlowDown : MonoBehaviour
{

	public float slowdownFactor = 0.5f;

	public float slowdownLength = 2f;



	public void Start()
	{
		//FindObjectOfType<Spawner>().OnPlayerWin += SlowdownTime;

	}
    
	void SlowdownTime()
	{
		StartCoroutine(ScaleTime(0.2f, 1f,1));

	}

	IEnumerator ScaleTime(float start, float end, float time)
	{
		AudioSource[] audios = FindObjectsOfType<AudioSource>();
	      
		float lastTime = Time.realtimeSinceStartup;
		Time.timeScale = start;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

		float timer = 0.0f;
		foreach (AudioSource audioo in audios)
        {
            audioo.pitch = 0.5f;
        }
		yield return new WaitForSeconds(0.75f);

		while (timer < time)
		{
			
		
			Time.timeScale = Mathf.Lerp(start, end, timer / time);
		
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
			timer += (Time.realtimeSinceStartup - lastTime);
			lastTime = Time.realtimeSinceStartup;
			foreach (AudioSource audioo in audios)
            {
				if(audioo != null)audioo.pitch = Time.timeScale;
            }
		
			yield return null;
		}
		foreach (AudioSource audiom in audios)
        {
            if (audiom != null)
            {
                audiom.pitch = 1;
            }
        }
        
		Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
	}
}