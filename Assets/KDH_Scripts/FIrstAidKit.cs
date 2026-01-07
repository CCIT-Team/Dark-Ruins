using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : ItemBase
{
    private int _health;
    private bool _used = false;
    protected override void Start()
    {
        base.Start();
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
        if(Input.GetKey(KeyCode.Mouse1)&&_used==false)
        {
            _used = true;
            GetComponentInParent<PlayerController_KSM>().OnDamaged(-_health,transform,false);
            GetComponentInParent<Inventory>().UsedItem();
            DestroySelf();
        }
    }
}
