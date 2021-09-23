using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Living
{
    public static event Action OnPlayerDied;
    public static event Action OnPlayerHit;

    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public Player_Particles player_Particles;
    public bool isVulnerable;

    protected override void Start()
    {
        isVulnerable = true;
        base.Start();
        isPlayer = true;
    }

    public override void TakeDamage(int damage, bool notUsed)
    {
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
        OnPlayerDied?.Invoke();
        base.Die();
    }

    public void EatChicken()
    {
        player_Particles.heal_Particle.Play();
        base.TakeDamage(-1);
    }

    public override void Splash()
    {
        Invoke(nameof(Disappear), 0.5f);
    }

    private void Disappear()
    {
        SkinnedMeshRenderer.enabled = false;
    }
}
