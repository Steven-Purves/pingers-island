using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gun : MonoBehaviour
{
    public static Action<string> OnAmmoChange;
    public static Action<GunType> OnGunChange;

    public TMP_Text weaponName;
    public TMP_Text ammoCount;
    public Image gunImage;

    public CanvasGroup canvasGroup;

    public Sprite revolver;
    public Sprite uzi;
    public Sprite shotGun;
    public Sprite tommyGun;
    public Sprite grenadeLauncher;
    public Sprite bazooka;
    public Sprite spaceGun;

    private IEnumerator courotine;

    public void Awake()
    {
        OnAmmoChange += AmmoChange;
        OnGunChange += GunChange;
    }

    private void OnDestroy()
    {
        OnAmmoChange -= AmmoChange;
        OnGunChange -= GunChange;
    }

    private void AmmoChange(string value)
    {
        ammoCount.text = value;
    }

    private void GunChange(GunType gunType)
    {
       if(courotine != null)
       {
            StopCoroutine(courotine);
       }

        courotine = SwapGun(gunType);
        StartCoroutine(courotine);
    }

    private IEnumerator SwapGun(GunType gunType)
    {
        canvasGroup.LeanAlpha(0, 0.2f);

        yield return new WaitForSeconds(0.2f);

        switch (gunType)
        {
            case (GunType.BAZOOKA):
                gunImage.sprite = bazooka;
                weaponName.text = "bazooka";
                break;
            case (GunType.GRENADE_LAUNCHER):
                gunImage.sprite = grenadeLauncher;
                weaponName.text = "launcher";
                break;
            case (GunType.REVOLVER):
                gunImage.sprite = revolver;
                weaponName.text = "revolver";
                break;
            case (GunType.TOMMYGUN):
                gunImage.sprite = tommyGun;
                weaponName.text = "tommy gun";
                break;
            case (GunType.SPACE_GUN):
                gunImage.sprite = spaceGun;
                weaponName.text = "laser";
                break;
            case (GunType.UZI):
                gunImage.sprite = uzi;
                weaponName.text = "uzi";
                break;
            case (GunType.SHOTGUN):
                gunImage.sprite = shotGun;
                weaponName.text = "shotgun";
                break;
        }

        canvasGroup.LeanAlpha(1, 0.2f);
    }
}
