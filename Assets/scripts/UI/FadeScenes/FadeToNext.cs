using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToNext : MonoBehaviour
{
     Animator animator;
	 string fadeOut = "FadeOut";
	 int levelToLoad;
	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void FadeToLevel(int levelIndex)
	{
		levelToLoad = levelIndex;
		animator.SetTrigger(fadeOut);
	}
	public void OnFadeComplete()
	{
		SceneManager.LoadScene(levelToLoad);
	}
}
