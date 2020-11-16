using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visible : MonoBehaviour
{
    public Enermy_State_Methods enermy_State_Methods;

    private void OnBecameVisible()
    {
        enermy_State_Methods.Visible = true;
    }

    private void OnBecameInvisible()
    {
        enermy_State_Methods.Visible = false;
    }
}
