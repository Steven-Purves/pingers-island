using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Living
{ 
    public bool isVulnerable;
    public static event Action OnPlayerDied = delegate { };
    protected override void Start()
    {
        base.Start();
        isPlayer = true;
    }
    public override void TakeDamage(int damage, bool notUsed)
    {
        if (isVulnerable)
        {
            base.TakeDamage(damage);
        }
    }
    public override void Die(bool notUsed)
    {
        OnPlayerDied();
        base.Die();
        print("pingers is dead");
    }
}
