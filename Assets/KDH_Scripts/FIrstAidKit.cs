using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIrstAidKit : ItemBase
{
    private int _health;
    protected override void Awake()
    {
        base.Awake();
        SetData();
    }
    public override void SetData()
    {
        var data = DataLoader.Instance.FindByName(this.GetType().Name);
        if (data == null)
        {
            return;
        }
        base.SetData();
        _health= Convert.ToInt32(data["Value"]);
    }
    public override void ItemUse(List<KeyCode> keys)
    {
        if(keys.Contains(KeyCode.Mouse0))
        {
            GetComponentInParent<PlayerController_KSM>().OnDamaged(-_health,transform,false);
        }
    }
}
