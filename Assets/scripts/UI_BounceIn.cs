using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BounceIn : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        transform.LeanScale(Vector3.one, 1f).setEaseOutBounce();
    }
}
