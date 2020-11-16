using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    GunController gunController;
    Vector3 moveDirection;

    public Vector3 MoveInput => moveDirection; 
    void Start()
    {
        gunController = GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput();
        KeyBoardInPut();
      
    }

    private void KeyBoardInPut()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = moveInput.normalized;
    }

    private void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();

        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerRelease();
        }
    }
}
