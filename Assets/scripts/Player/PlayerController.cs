using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Transform jimmyPingersModel;
    public float moveSpeed = 5;

    private Rigidbody rb;
    private PlayerInput playerInput;

    private Vector3 velocity;
    private PlayerAnimations playerAnimations;

    void Start()  
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    void Update() 
    {
        velocity = playerInput.MoveInput * moveSpeed;

        print($"{jimmyPingersModel.forward} , {jimmyPingersModel.TransformVector(Vector3.zero)}");

        playerAnimations.AnimateWalk(Vector3.Dot(jimmyPingersModel.forward, velocity), Vector3.Dot(jimmyPingersModel.right, velocity));
    }

    void FixedUpdate () => rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 height = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        jimmyPingersModel.LookAt(height);
    }
}
