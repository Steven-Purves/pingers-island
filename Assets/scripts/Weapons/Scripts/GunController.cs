using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunController : MonoBehaviour
{
	public Transform weaponHold;
    
	public Gun[] Guns;
	Gun equippedGun;

	void Start()
	{
		if (Guns != null)
		{
			EquipGun(Guns[0]);
		}
	}

	public void EquipGun(Gun gunToEquip)
	{
		if (equippedGun != null)
		{
			Destroy(equippedGun.gameObject);
		}

		equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
		equippedGun.transform.parent = weaponHold;
		equippedGun.Init(this);
	}

	public void OnTriggerHold()
	{
		if (equippedGun != null)
		{
			equippedGun.OnTriggerHold();
		}
	}

	public void OnTriggerRelease()
	{
		if (equippedGun != null)
		{
			equippedGun.OnTriggerRelease();
		}
	}

	public void Aim(Vector3 point)
	{
		if (equippedGun != null)
		{
			equippedGun.Aim (point);
		}
	}
}
