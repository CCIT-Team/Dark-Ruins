using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock : GunBase
{
    protected override void Start()
    {
        base.Start();
        _fireLength = 1.5f;
        _up = new Vector3(0, 0.0f, 0);
        _f = new Vector3(-90f, 0f, -90f);
    }
    public override void Init()
    {
        transform.localRotation = Quaternion.Euler(_f);
        transform.localPosition = new Vector3(0.5f, 0, 0) + transform.localPosition;
    }
    public override void SetData()
    {
        var data = DataLoader.Instance.FindByName(this.GetType().Name);
        if (data == null)
        {
            return;
        }
        base.SetData();
        _fireCooltimeSet= Convert.ToSingle(data["AttackSpeed"]);
        _maxBullet = Convert.ToInt32(data["MaxAmmo"]);
    }
}
