using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredRifleAmmo : FiredBullet
{
    private void Start()
    {
        var data = DataLoader.Instance.FindByName("RifleAmmo");
        if (data == null)
        {
            return;
        }
        _damage = Convert.ToInt32(data["Value"]);
    }
    public override void Initialize(BulletsPool pool)
    {
        base.Initialize(pool);
        _bullet = BulletsPool.Bullets.Rifle;
    }
}
