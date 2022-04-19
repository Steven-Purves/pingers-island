using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrosshairController : MonoBehaviour
{
    public static CrosshairController Instance;

    public Camera viewCamera;
    public Transform crosshairs;
    public Texture2D cursorTex;
    public MeshRenderer meshRenderer;

    public GunController gunController;
    public PlayerController playerController;

    private Vector3 point;
    private Ray ray;

    void Awake()
    {
        Instance = this;

        meshRenderer.enabled = false;
     
        Cursor.visible = false;

        Player.OnPlayerDied += SetCursorVisible;
        Spawner.OnPlayerWin += SetCursorVisible;

        Invoke(nameof(SetCrossHair), 3);
    }

    private void SetCursorVisible()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.ForceSoftware);

        crosshairs.gameObject.SetActive(false);
    }

    private void SetCrossHair()
    {
        meshRenderer.enabled = true;
    }

    void FixedUpdate()
    {
        ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.weaponHold.position.y);

        if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 4 && !Cursor.visible)
        {
            gunController.Aim(point);
        }
     
        if (groundPlane.Raycast(ray, out float rayDistance))
            point = ray.GetPoint(rayDistance);
      

        crosshairs.Rotate(Vector3.up * 60 * Time.deltaTime);
        crosshairs.position = point + Vector3.up * 0.3f;

        playerController.LookAt(point);
    }

    private void OnDestroy()
    {
        Player.OnPlayerDied -= SetCursorVisible;
        Spawner.OnPlayerWin -= SetCursorVisible;
    }
}
