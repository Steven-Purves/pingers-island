using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    PlayerInput playerInput;

    Vector3 velocity;
    public float moveSpeed = 5;
    void Start()  
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        velocity = playerInput.MoveInput * moveSpeed;
    }
    void FixedUpdate ()
	{
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
    public void LookAt(Vector3 lookPoint) // make all this 
    {
      //  Vector3 height = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
      //  transform.LookAt(height);

    }
}
