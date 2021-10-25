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

    private bool isLooking = true;

    void Start()  
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimations = GetComponent<PlayerAnimations>();

        Player.OnPlayerDied += IsLooking; 
    }

    private void OnDestroy()
    {
        Player.OnPlayerDied -= IsLooking;
    }

    private void IsLooking() 
    {
        isLooking = false;
    } 

    void Update() 
    {
        velocity = playerInput.MoveInput * moveSpeed;

        playerAnimations.AnimateWalk(Vector3.Dot(jimmyPingersModel.forward, velocity), Vector3.Dot(jimmyPingersModel.right, velocity));
    }

    void FixedUpdate () => rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    public void LookAt(Vector3 lookPoint)
    {
        if (isLooking)
        {
            Vector3 height = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            jimmyPingersModel.LookAt(height);
        }
    }
}
