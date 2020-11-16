using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]

public class Player : Living
{ 
    public bool hitable;
    public static event Action OnPlayerDied = delegate { };
    protected override void Start()
    {
        base.Start();
        isPlayer = true;
    }
    public override void TakeDamage(int damage, bool notUsed)
    {
        if (hitable)
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
