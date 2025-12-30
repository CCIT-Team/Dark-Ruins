using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockAmmo : BulletItem
{
    public override void SetData()
    {
        var data = DataLoader.Instance.FindByName(this.GetType().Name);
        if (data == null)
        {
            return;
        }
        base.SetData();
        Bullet = BulletsPool.Bullets.Glock;
    }
}
