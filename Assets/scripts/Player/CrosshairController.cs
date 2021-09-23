using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public Transform crosshairs;
    #pragma warning disable 649
    GunController gunController;
    [SerializeField] LayerMask targetMask;
    [SerializeField] Camera viewCamera;
    [SerializeField] Color highLight;
    [SerializeField] Color original;
    private SpriteRenderer dot;
    private Vector3 point;
    private Ray ray;

    private PlayerController playerController;

    public static CrosshairController Instance;

    void Awake()
    {
        Instance = this;
        gunController = GetComponent<GunController>();
        Cursor.visible = false;
        dot = crosshairs.GetComponentInChildren<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

        Player.OnPlayerDied += PlayerDied;
    }

    private void PlayerDied()
    {
        Cursor.visible = true;
        crosshairs.gameObject.SetActive(false);
    }

    void Update()
    {
        ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.weaponHold.position.y);

        if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 4)
        {
            gunController.Aim(point);
            
        }
     
        if (groundPlane.Raycast(ray, out float rayDistance))
            point = ray.GetPoint(rayDistance);
       
        if (Physics.Raycast(ray, 100, targetMask))
            dot.color = highLight;
        else
            dot.color = original;

        crosshairs.Rotate(Vector3.up * 60 * Time.deltaTime);
        crosshairs.position = point + Vector3.up * 0.3f;

        playerController.LookAt(point);
    }
}
