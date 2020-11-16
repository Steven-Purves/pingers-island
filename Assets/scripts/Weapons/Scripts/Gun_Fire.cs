using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Fire : MonoBehaviour
{
    public Transform[] muzzel;
    public GameObject bullet;
    private Gun_Muzzel_Effect muzzel_Effect;

    [Header("Effects")]
    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    public float offset;

    private void Start() => muzzel_Effect = GetComponent<Gun_Muzzel_Effect>();

    public virtual void ShootBullet()
   { 
        for (int i = 0; i < muzzel.Length; i++)
        {
            PoolManager.Instance.ReuseObject(bullet, muzzel[i].position, muzzel[i].rotation * Quaternion.Euler(Random.Range(-offset, offset), Random.Range(-offset, offset), 0f));
            muzzel_Effect.EffectShow(muzzel[i]);
        }

        AudioManger.Instance.PlaySound(shootAudio, transform.position);
    }

    public void Reload()
    {
        AudioManger.Instance.PlaySound(reloadAudio, transform.position);
    }
}
