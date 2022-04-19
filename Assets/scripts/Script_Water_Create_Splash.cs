using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Water_Create_Splash : MonoBehaviour
{
    public GameObject bigSplash;
    public GameObject smallSplash;
    public AudioClip splash;

    private bool gameOver;

    public void Awake()
    {
        PoolManager.Instance.CreatePool(smallSplash, 5);

        Player.OnPlayerDied += GameEnded;
        Spawner.OnPlayerWin += PlayerDied;
    }

    private void PlayerDied()
    {
        gameOver = true;
    }

    private void GameEnded()
    {
        gameOver = true;
    }

    private void OnTriggerEnter(Collider c)
    {
        IDamageable damageable = c.GetComponent<IDamageable>();

        Vector3 position = new Vector3(c.transform.position.x, transform.position.y, c.transform.position.z);

        if (!gameOver)
        {
            AudioManger.Instance.PlaySound(splash, c.transform.position);
        }

        if (damageable != null)
        {
            Instantiate(bigSplash, position, Quaternion.identity);

            damageable.Splash();
        }
        else
        {
            PoolManager.Instance.ReuseObject(smallSplash, position, Quaternion.identity);
            c.gameObject.SetActive(false);
        }
    }
}
