using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingUI : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 2.5f) * 10), Time.time);
    }
}
