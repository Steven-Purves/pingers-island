using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour {

    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * 60 * -Time.deltaTime);
      //  transform.position = TrackMouse.point;
	}
}
