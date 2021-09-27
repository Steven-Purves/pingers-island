using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LevelConstructor : ScriptableObject
{
    public Material skyboxType;
    public GameObject waterType;
    public GameObject[] weatherType;
    public GameObject[] groundTypes;
    public GameObject[] obsticles;
    public GameObject[] obsticlesWithLayers;
    public GameObject[] natureObjects;
}