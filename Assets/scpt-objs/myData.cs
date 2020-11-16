using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MyData : ScriptableObject
{
    public int roundNumber;
    public int CurrentScore;
    public int playerHighScore;
    public string playerHighScoreName;
    public bool ReachedHighScore;
   
}
