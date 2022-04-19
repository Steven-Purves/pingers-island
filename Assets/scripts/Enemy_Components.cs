using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Components : MonoBehaviour
{
    [Space]
    public EnemyStateController enemyStateController;
    public EnemyAnimationEventHandler EnemyAnimationEventHandler;
    public Enermy_State_Methods enermy_State_Methods;
    public EnemyLife enemyLife;
    [Space]
    public Enemy_Data currentEnemyData;
    public State startState;
    public State chaseState;
    [Space]
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public MeshRenderer meshRendererBone;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public GameObject boneInHand, fireballInHand, throwingBone, throwingFireball, greenGroundStompParticle, redExpolsionParticle, dirtParticle, hitPingersParticle, hitBonerParticle, deathParticle;
    [Space]
    public AudioClip digging;
    public AudioClip swipe, blowUp, throwing, growl, throwingFireBall, groundSmash;
    public AudioClip death;
    [Space]
    public BoxCollider[] handColliders;
    public TrailRenderer[] handTrails;
    public Enemy_Data[] enemy_Data;
}
