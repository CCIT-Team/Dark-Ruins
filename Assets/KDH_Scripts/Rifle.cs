using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Rifle : GunBase
{
    protected override void Awake()
    {
        base.Awake();
        _fireLength = 1.0f;
        SetData();
        _up = new Vector3(0, 0f, 0);
        _f = new Vector3(0, 0, 0);
    }
    public override void Init()
    {
        transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        transform.localPosition=new Vector3(0.5f,-0.2f,0)+transform.localPosition;

    }
    public override void SetData()
    {
        var data = DataLoader.Instance.FindByName(this.GetType().Name);
        if (data == null)
        {
            return;
        }
        base.SetData();
#if UNITY_EDITOR
        Debug.Log(data["AttackSpeed"]);
#endif
        _fireCooltimeSet= Convert.ToSingle(data["AttackSpeed"]);
        _maxBullet = Convert.ToInt32(data["MaxAmmo"]);
    }
}
