﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Runtime.InteropServices;

public class EnemyStateController : TrackPlayer
{
    [HideInInspector] public Enemy_Components enemy_Components;
    [Space]
    public State currentState;
    public State remainState;

    [HideInInspector] public int speed = Animator.StringToHash("Speed");

    void Start() => enemy_Components = GetComponent<Enemy_Components>();
    void Update() => currentState.UpdateState(this);

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState.EndState(this);

            currentState = nextState;
            enemy_Components.EnemyAnimationEventHandler.animationHasFinished = false;
            nextState.StartState(this);
        }
    }
}