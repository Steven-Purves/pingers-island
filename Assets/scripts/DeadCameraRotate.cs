using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCameraRotate : TrackPlayer
{
    private void Update() => transform.Rotate(Vector3.up * 20 * Time.deltaTime, Space.World);

    private void OnEnable()
    {
        transform.position = playerTransform.position;
    }
}
