using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : PoolObject
{

	Rigidbody rb;
	public float forceMin;
	public float forceMax;

	float lifeTime = 15;
	float fadeTime = 2;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public override void OnObjReuse()
	{
		float force = Random.Range(forceMin, forceMax);

		rb.AddTorque(Random.insideUnitSphere * force);
		rb.AddForce(transform.right * force);
	}

	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(lifeTime);

		float percent = 0;
		float fadeSpeed = 1 / fadeTime;
		Material mat = GetComponent<Renderer>().material;
		Color intialColour = mat.color;

		while (percent < 1)
		{
            
			percent += Time.deltaTime * fadeSpeed;
			mat.color = Color.Lerp(intialColour, Color.clear, percent);
			yield return null;

		}

		gameObject.SetActive(false);
				  
	}
}
