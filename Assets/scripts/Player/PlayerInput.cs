using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GunController gunController;
    private Vector3 moveDirection;
    private Vector3 moveInput;
    private bool isDisabled;

    public Vector3 MoveInput => moveDirection; 

    void Start()
    {
        gunController = GetComponent<GunController>();
    }

    void Update()
    {
        MouseInput();
        KeyBoardInput();
    }

    private void KeyBoardInput()
    {
        if (isDisabled)
        {
            moveInput = Vector3.zero;
        }
        else
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

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
