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
                    GamePeriodManager.OnAddScore?.Invoke(100);
                    other.GetComponent<Player>().EatChicken();
                    break;
                case PickUpType.SHIELD:
                    GamePeriodManager.OnAddScore?.Invoke(150);
                    other.GetComponentInChildren<Force_Field>().ShieldsUp();
                    break;
                case PickUpType.UZI:
                    player.EquipGun((int)type);
                    GamePeriodManager.OnAddScore?.Invoke(175);
                    break;
                case PickUpType.SHOT_GUN:
                    player.EquipGun((int)type);
                    GamePeriodManager.OnAddScore?.Invoke(200);
                    break;
                case PickUpType.TOMMY_GUN:
                    GamePeriodManager.OnAddScore?.Invoke(250);
                    player.EquipGun((int)type);
                    break;
                case PickUpType.GRENADE_LAUNCHER:
                    GamePeriodManager.OnAddScore?.Invoke(300);
                    player.EquipGun((int)type);
                    break;
                case PickUpType.BAZOOKA:
                    GamePeriodManager.OnAddScore?.Invoke(400);
                    player.EquipGun((int)type);
                    break;
                case PickUpType.SPACE_GUN:
                    GamePeriodManager.OnAddScore?.Invoke(500);
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