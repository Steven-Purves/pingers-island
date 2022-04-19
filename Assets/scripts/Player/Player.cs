using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Living
{
    public static Transform pTransform;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerHit;
    public static event Action OnPlayerEatChicken;

    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public Player_Particles player_Particles;
    public bool isVulnerable;

    public AudioClip hit;
    public AudioClip die;
    public AudioClip eatChicken;

    private bool drowned;
    private bool playerWin;

    private void Awake()
    {
        pTransform = transform;
    }

    protected override void Start()
    {
        isVulnerable = true;
        base.Start();
        isPlayer = true;

        Spawner.OnPlayerWin += PlayerWin;
    }

    private void OnDestroy()
    {
        Spawner.OnPlayerWin -= PlayerWin;
    }

    private void PlayerWin()
    {
        playerWin = true;
    }

    public override void TakeDamage(int damage, bool notUsed)
    {
        if(damage > 0)
        {
            AudioManger.Instance.PlaySfx2D(hit);
        }

        if (isVulnerable)
        {
            OnPlayerHit?.Invoke();
            base.TakeDamage(damage);
            isVulnerable = false;
            Invoke(nameof(IsVurnerableSwitch),1);
        }
    }

    private bool IsVurnerableSwitch() => isVulnerable = true;

    public override void Die(bool notUsed)
    {
        if (!playerWin)
        {
            if (!drowned)
            {
                AudioManger.Instance.PlaySfx2DWithDelay(die, .2f);
            }
            OnPlayerDied?.Invoke();
            base.Die();
        }
    }

    public void EatChicken()
    {
        AudioManger.Instance.PlaySfx2D(eatChicken);
        player_Particles.heal_Particle.Play();
        OnPlayerEatChicken?.Invoke();
        base.TakeDamage(-1);
    }

    public override void Splash()
    {
        drowned = true;
        Invoke(nameof(Disappear), 0.5f);
        Die(false);
    }

    private void Disappear()
    {
        SkinnedMeshRenderer.enabled = false;
    }
}
