using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour {


	public GameObject heart;
	
	List<Heart> heartScript = new List<Heart>();

	void Start () {
		      
		StartCoroutine(InstantiateHearts());
	}

	IEnumerator InstantiateHearts()
	{
		int lives = 5;   // change to level stat etc
		yield return new WaitForSeconds(1);

		for (int i = 0; i < lives; i++)
		{
            GameObject newheart = Instantiate(heart) as GameObject;
            newheart.transform.SetParent(transform, false);
			newheart.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 75, 0);
            heartScript.Add(newheart.GetComponent<Heart>());

			yield return new WaitForSeconds(0.3f);
        }

	}
	public void AnimateHeart(int healthNum, bool direction)
	{
		heartScript[healthNum].AnimateHeartIn(direction);
	}
}
