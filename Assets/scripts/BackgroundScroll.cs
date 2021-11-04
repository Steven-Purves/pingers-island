using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public Sprite[] gradent;
    public Image backGround;


    public float scrollSpeed;
    public Image img;

    private void Start()
    {
        backGround.sprite = gradent[Random.Range(0, gradent.Length)];
    }

    void Update()
    {
        img.material.mainTextureOffset = img.material.mainTextureOffset + new Vector2(Time.deltaTime * (-scrollSpeed / 5), Time.deltaTime * (-scrollSpeed / 10));
    }
}