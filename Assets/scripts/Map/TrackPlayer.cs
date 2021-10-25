using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackPlayer : MonoBehaviour
{
    Transform playerT;
    public Transform playerTransform { get => playerT; }
    void Awake()
    {
        playerT = FindObjectOfType<Player>().transform;
    }
}
