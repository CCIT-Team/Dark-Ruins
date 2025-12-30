using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredGlockAmmo : FiredBullet
{
    private void Start()
    {
        var data = DataLoader.Instance.FindByName("GlockAmmo");
        if (data == null)
        {
            return;
        }
        _damage = Convert.ToInt32(data["Value"]);
    }
    public override void Initialize(BulletsPool pool)
    {
        base.Initialize(pool);
        _bullet = BulletsPool.Bullets.Glock;
    }
}
