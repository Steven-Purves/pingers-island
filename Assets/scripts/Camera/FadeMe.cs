using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMe : MonoBehaviour {


//	float lifeTime = 15;
    float fadeTime = .5f;

	private void Start()
	{
		StartCoroutine(FadeOut());
	}

	public IEnumerator FadeOut()
    {
      
    float percent = 0;
    float fadeSpeed = 1 / fadeTime;
    Material mat = GetComponent<Renderer>().material;
   
		mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		mat.SetInt("_ZWrite", 0);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.EnableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = 3000;

    Color intialColour = mat.color;
        
        while (percent < 1f)
        {
            percent += Time.deltaTime* fadeSpeed;
            mat.color = Color.Lerp(intialColour, Color.clear, percent);
            yield return null;

        }

		yield return new WaitForSeconds(3);

		percent = 0;
		while (percent < 1f)
        {
            percent += Time.deltaTime * fadeSpeed;
			mat.color = Color.Lerp(Color.clear,intialColour, percent);
            yield return null;

        }
		Destroy(this);
    }
}
