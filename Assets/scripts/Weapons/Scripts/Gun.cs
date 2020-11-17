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
	float nextShotTime;

	bool triggerReleasedSinceLastShot;
    int shotsRemainingInBurst;

    private Gun_Fire gun_Fire;
    private GunController gunController;

	public void Init(GunController gunController)
    {
        this.gunController = gunController;

        gun_Fire = GetComponent<Gun_Fire>();
        gun_Fire.Reload();

        shotsRemainingInBurst = burstCount;
        triggerReleasedSinceLastShot = true;
    }

    void Shoot()
    {
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

            if (shotsLeft == 0 && !infiniteAmmo)
            {
                gunController.EquipGun((int)GunController.GunType.REVOLVER);
                return;
            }

            gun_Fire.ShootBullet();
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

    public void Aim(Vector3 point)
    {
        transform.LookAt(point);
    }
}
