using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinLetters : MonoBehaviour
{
    public RectTransform rectTrans;
    public float speed;
    public float amp;
    public float delay;
    // Update is called once per frame
    void Update()
    { 
        rectTrans.anchoredPosition = new Vector2(rectTrans.anchoredPosition.x, amp * Mathf.Sin(Time.time * speed + delay));
    }
}
