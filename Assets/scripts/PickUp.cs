using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp : PoolObject
{
    public enum PickUpType { CHICKEN_LEG, UZI, SHOT_GUN, TOMMY_GUN, GRENADE_LAUNCHER, BAZOOKA, SPACE_GUN, SHIELD }
    public PickUpType type;
    [Space]
    public ParticleSystem particle;
    public GameObject pickup;
    public SphereCollider myCollider;


    private void Start() => BeginCountDown();
    private void Update() => transform.Rotate(Vector3.up * 100 * Time.deltaTime, Space.World);
    private void BeginCountDown() => Invoke(nameof(ScaleOut), 5);

    public override void OnObjReuse()
    {
        myCollider.enabled = true;
        particle.Play();
        gameObject.transform.localScale = Vector3.one;
        pickup.SetActive(true);

        BeginCountDown();
    }

    private void ScaleOut()
    {
        particle.Stop();
        LeanTween.scale(gameObject, Vector3.zero, 1.5f).setEaseInBounce().setOnComplete(TurnOff);
    }

    public void OnTriggerEnter(Collider other)
    {
        GunController player = other.GetComponent<GunController>();

        if (player != null)
        {
            switch (type)
            {
                case PickUpType.CHICKEN_LEG:
                    print("yum!");
                    break;
                case PickUpType.SHIELD:
                    print("shields up!");
                    break;
                case PickUpType.UZI:
                case PickUpType.SHOT_GUN:
                case PickUpType.TOMMY_GUN:
                case PickUpType.GRENADE_LAUNCHER:
                case PickUpType.BAZOOKA:
                case PickUpType.SPACE_GUN:
                    player.EquipGun((int)type);
                    break;
                default:
                    break;
            }

            TurnOff();
        }
    }

    private void TurnOff()
    {
        particle.Stop();
        myCollider.enabled = false;
        pickup.SetActive(false);
        Invoke(nameof(SwitchOff), 2);
    }
    private void SwitchOff()
    {
        gameObject.SetActive(false);
    }
}