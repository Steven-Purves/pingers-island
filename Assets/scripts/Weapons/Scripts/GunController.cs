using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunController : MonoBehaviour
{
	public Transform weaponHold;
    
	public Gun[] Guns;
	public GameObject[] droppedGuns;
	private Gun equippedGun;

	public enum GunType { REVOLVER, UZI, SHOTGUN, TOMMYGUN, GRENADE_LAUNCHER, BAZOOKA, SPACE_GUN }
	private GunType equippedGunType = GunType.REVOLVER;

	void Start()
	{
		if (Guns != null)
		{
			EquipGun((int)GunType.REVOLVER);
		}
	}

	public void EquipGun(int gunToEquip)
	{
		if (equippedGun != null)
		{
			DropGun();
			Destroy(equippedGun.gameObject);
		}

		equippedGun = Instantiate(Guns[gunToEquip], weaponHold.position, weaponHold.rotation);
		equippedGun.transform.parent = weaponHold;
		equippedGun.Init(this);
		equippedGunType = (GunType)gunToEquip;
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

	private void DropGun()
    {
        if (equippedGunType != GunType.REVOLVER)
        {
			PoolManager.Instance.ReuseObject(droppedGuns[(int)equippedGunType-1], weaponHold.position, Quaternion.identity);
		}
    }
}
