using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy_Data")]
public class Enemy_Data : ScriptableObject
{
    public EnemyType enemyType;
    public int startingHealth;
    public float speed;
    public int pointsOnKill;
    public Material material;
    public RuntimeAnimatorController runtimeAnimatorController;
    public GameObject throwingWeapon;
    public Color trailColour;
    public GameObject[] explosionBones;
    public GameObject deathExplosion;
}

public enum EnemyType
{
    White,
    Green,
    Blue,
    Red
}
