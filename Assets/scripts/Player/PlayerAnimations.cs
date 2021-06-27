using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private bool pingersDead;
    public Animator myAnimator;
    private GunController.GunType currentGun;

    private void Start()
    {
        Player.OnPlayerDied += PlayerDied;
        Player.OnPlayerHit += PlayerHit;
        GunController.OnGunSwap += GunSwap; 
    }

    public void AnimateWalk(float forward, float side)
    {
        myAnimator.SetFloat("Forward", forward);
        myAnimator.SetFloat("Side", side); ;
    }

    private void GunSwap(GunController.GunType gunType)
    {
        if (pingersDead)
        {
            return;
        }

        myAnimator.SetLayerWeight(1, 1f);

        currentGun = gunType;

        switch (gunType)
        {
            case (GunController.GunType.BAZOOKA):
                myAnimator.SetLayerWeight(1, 0.5f);
                myAnimator.SetTrigger("Bazooka");
                break;
            case (GunController.GunType.GRENADE_LAUNCHER):
                myAnimator.SetLayerWeight(1, 0.5f);
                myAnimator.SetTrigger("Bazooka");
                break;
            case (GunController.GunType.REVOLVER):
                myAnimator.SetTrigger("Pistol");
                break;
            case (GunController.GunType.TOMMYGUN):
                myAnimator.SetTrigger("Rifle");
                break;
            case (GunController.GunType.SPACE_GUN):
                myAnimator.SetTrigger("Pistol");
                break;
            case (GunController.GunType.UZI):
                myAnimator.SetTrigger("Pistol");
                break;
            case (GunController.GunType.SHOTGUN):
                myAnimator.SetTrigger("Rifle");
                break;
        }    
    }

    private void PlayerDied()
    {
        pingersDead = true;
        myAnimator.SetLayerWeight(1, 0);
        myAnimator.SetTrigger("Died");
    }
    private void PlayerHit()
    {
        myAnimator.SetTrigger("Hit");

        StartCoroutine(BeenHit());
    }

    private IEnumerator BeenHit()
    {
        yield return new WaitForSeconds(0.75f);
        GunSwap(currentGun);
    }
}
