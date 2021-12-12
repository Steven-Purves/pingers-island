using System;
using UnityEngine;

public class Crate_Move : MonoBehaviour
{
    public Transform crateMesh;
    public MeshRenderer crateMeshRenderer;
    public Spawn_Pickup spawn_Pickup;
    [Space]
    public float fallingAcceleration = 0.02f;
    public float floatingSpeed = 0.05f;
    public float fallHeight = 10;
    public float startHeight = 30;
    [Space]
    public float timeTillRespawn = 5;
    public Script_Pick_up_Balloon balloon;
    public MapGen MapGen { set => mapGen = value; }
    MapGen mapGen;

    float acceleration;
    float velocity;

    float spinDirection;

    bool isGrounded;
    bool isBalloonPopped;
    bool gameOver;

    private void Start()
    {
        Player.OnPlayerDied += GameEnded;
        Init();
    }

    public void Init()
    {
        if(gameOver)
        {
            return;
        }

        balloon.ResetBalloon();
        Vector3 randomTile = mapGen.GetRandomOpenTile().position;

        spinDirection = UnityEngine.Random.Range(-0.15f,0.15f);

        crateMeshRenderer.enabled = true;
        isGrounded = false;

        transform.position = new Vector3(randomTile.x, startHeight, randomTile.z);

        crateMesh.rotation = Quaternion.Euler(0 + UnityEngine.Random.Range(-30f, 30f), 0, 0 + UnityEngine.Random.Range(-30f, 30f));

        acceleration = fallingAcceleration;

        isBalloonPopped = false;
    }

    private void GameEnded()
    {
        gameOver = true;
    }

    private void Update()
    {
        Movement();
        PopBalloon();
        SpinCrate();
    }

    void SpinCrate()
    {
        crateMesh.Rotate(new Vector3(0, spinDirection, 0), Space.World);
    }

    void PopBalloon()
    {
        if (!isBalloonPopped)
        {
            if (transform.position.y <= fallHeight)
            {
                balloon.PopBalloon();
                isBalloonPopped = true;
            }
        }
    }

    void Movement()
    {
        if (transform.position.y >= 0)
        {
            velocity = transform.position.y >= fallHeight ? velocity = floatingSpeed : velocity += acceleration;
            transform.position = new Vector3(transform.position.x, transform.position.y - velocity, transform.position.z);
        }
        else if (!isGrounded)
        {
            isGrounded = true;
            HitGround();
        }
    }

    void HitGround()
    {
        spawn_Pickup.SpawnPickup();

        crateMeshRenderer.enabled = false;

        acceleration = 0;
        velocity = 0;

        PoolManager.Instance.ReuseObject(Class_Pool_Manager_Create_The_Pool_Objects.Instance.crateBits, transform.position, Quaternion.identity);

        Invoke(nameof(Init), timeTillRespawn);
    }
}
