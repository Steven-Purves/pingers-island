using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzelFlash : Gun_Muzzel_Effect
{
	public GameObject flashHolder;
	public GameObject Shell;
	public Transform shellEject;
	public Sprite[] flashSprites;

	public SpriteRenderer[] spriteRenderers;
	public float flashtime;

	void Start()
	{
		Deactivate();
	}

	void Deactivate()
	{

		flashHolder.SetActive(false);
	}

	public override void EffectShow(Transform t)
	{
		flashHolder.SetActive(true);

		int flashSpriteIndex = Random.Range(0, flashSprites.Length);
		for (int i = 0; i < spriteRenderers.Length; i++)
		{
			spriteRenderers[i].sprite = flashSprites[flashSpriteIndex];
		}

		PoolManager.Instance.ReuseObject(Shell, shellEject.position, transform.rotation);
		Invoke("Deactivate", flashtime);
	}
}
