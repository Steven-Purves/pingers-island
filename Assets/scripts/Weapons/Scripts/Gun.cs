using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public enum FireMode { Auto, Burst, Single };
    public FireMode fireMode;

	public bool infiniteAmmo;

	public float msBetweenShots = 100;
    public int burstCount;

	public int shotsLeft;
	private float nextShotTime;

	private bool triggerReleasedSinceLastShot;
    private int shotsRemainingInBurst;

    private Gun_Fire gun_Fire;
    private GunController gunController;

    private bool isAiming;
    private bool isDead;

	public void Init(GunController gunController)
    {
        isAiming = true;
        this.gunController = gunController;

        gun_Fire = GetComponent<Gun_Fire>();
        gun_Fire.Reload();

        shotsRemainingInBurst = burstCount;
        triggerReleasedSinceLastShot = true;

        Player.OnPlayerDied += PlayerDead;
        Player.OnPlayerHit += playerHit;
    }

    private void PlayerDead()
    {
        isAiming = false; 
        isDead = true;
    }

    private void playerHit()
    {
        isAiming = false; 
        Invoke(nameof(IsAimingSwitch), 0.75f);
    }

    void Shoot()
    {
        if (isDead)
        {
            return;
        }

        if (Time.time > nextShotTime)
        {
            if (fireMode == FireMode.Burst)
            {
                if (shotsRemainingInBurst == 0)
                {
                    gun_Fire.Reload();
                    return;
                }

                shotsRemainingInBurst--;

            }
            else if (fireMode == FireMode.Single)
            {
                if (!triggerReleasedSinceLastShot)
                {
                    return;
                }
            }

            nextShotTime = Time.time + msBetweenShots / 1000;
            
            shotsLeft--;

            gun_Fire.ShootBullet();

            if (gunController.equippedGunType == GunController.GunType.REVOLVER)
            {
                isAiming = false;

                LeanTween.rotateX(gameObject, -17, .15f).setOnComplete(() =>
                {
                    LeanTween.rotateX(gameObject, 0, .15f);

                    if (!isDead)
                    {
                        isAiming = true;
                    }
                });
            }

            if (shotsLeft == 0 && !infiniteAmmo)
            {
                gunController.EquipGun((int)GunController.GunType.REVOLVER);
                return;
            }

            LeanTween.moveLocalY(gameObject, -.17f, .05f).setOnComplete(() => LeanTween.moveLocal(gameObject, Vector3.zero, .05f));
        }
    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }

    private void IsAimingSwitch() => isAiming = true;

    public void Aim(Vector3 point)
    {
        if (isAiming && !isDead)
        {
            transform.LookAt(point);
        }
    }
    
    public void OnDestroy()
    {
        Player.OnPlayerDied -= PlayerDead;
        Player.OnPlayerHit -= playerHit;
    }
}
