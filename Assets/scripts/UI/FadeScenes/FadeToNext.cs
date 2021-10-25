using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToNext : MonoBehaviour
{
	public static Action<int> OnFadeToLevel;
    public Animator animator;
	private const string fadeOut = "FadeOut";
	private int levelToLoad;


	private void Start()
	{
		OnFadeToLevel += FadeToLevel;
	}

    private void OnDestroy()
    {
		OnFadeToLevel -= FadeToLevel;
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
