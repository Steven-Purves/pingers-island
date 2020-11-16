using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public MyData data;
    void Start()
    {
        data.CurrentScore = 0;
        data.roundNumber = 0;
        data.playerHighScore = 0;
    }
}
