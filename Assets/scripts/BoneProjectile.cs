using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] public Rigidbody rb;
    public void Launch()
    {
        Vector3 initialVelocityNeeded = CalculateLaunchVelocity(Player.pTransform.position);

        if (float.IsNaN(initialVelocityNeeded.x))
        {   
            gameObject.SetActive(false);
            return;
        }

        rb.velocity = initialVelocityNeeded;

        rb.maxAngularVelocity = 10;
        rb.AddTorque(transform.right * 1000);
    }

    Vector3 CalculateLaunchVelocity(Vector3 target)
    {
        float height = target.y + 0.3f;
        float gravity = -25;

        float displacementY = (target.y - rb.position.y);
        Vector3 displacementXZ = new Vector3(target.x - rb.position.x, 0, target.z - rb.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-5 * height / gravity) + Mathf.Sqrt(5 * (displacementY - height) / gravity));
        return velocityXZ + velocityY;
    }
    private void OnDisable()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }
}
