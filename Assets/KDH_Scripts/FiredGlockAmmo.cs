using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredGlockAmmo : FiredBullet
{
    private void Start()
    {
        var data = DataLoader.Instance.FindByName("Glock");
        if (data == null)
        {
            return;
        }
        _damage = Convert.ToInt32(data["Damage"]);
        _criticalDamage = Convert.ToInt32(data["CriticalDamage"]);
    }
    public override void Initialize(BulletsPool pool)
    {
        base.Initialize(pool);
        _bullet = BulletsPool.Bullets.Glock;
    }
}
