using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeItem : MonoBehaviour
{
    public Image myImage;

    public Sprite heart;
    public Sprite Skull;

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
        if (isHurt)
        {
            myImage.sprite = Skull;
        }
        else
        {
            myImage.sprite = heart;
        }
    }
}
