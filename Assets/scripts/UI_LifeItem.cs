using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeItem : MonoBehaviour
{
    public Image myImage;

    public Sprite heart;
    public Sprite Skull;

    private IEnumerator couroute; 

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        transform.LeanScale(Vector3.one, 1).setEaseOutBounce();
    }

    public void Changed(bool isHurt)
    {
        if(couroute != null)
        {
            StopCoroutine(couroute);
        }

        couroute = SwitchImage(isHurt? Skull : heart);
        StartCoroutine(couroute);
    }

    private IEnumerator SwitchImage(Sprite sprite)
    {
        float time = 0.2f;
        transform.LeanScale(Vector3.zero, time).setEaseInCubic();
        yield return new WaitForSeconds(time);
        myImage.sprite = sprite;
        transform.LeanScale(Vector3.one, time).setEaseOutCubic();
    }
}
