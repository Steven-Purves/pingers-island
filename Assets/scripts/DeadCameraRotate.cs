﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCameraRotate : TrackPlayer
{
    private void Update() => transform.Rotate(10 * Time.deltaTime * Vector3.up, Space.World);

    private void OnEnable()
    {
        transform.position = playerTransform.position;
    }
}