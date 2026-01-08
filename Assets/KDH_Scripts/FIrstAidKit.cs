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
            Managers_YGU.Sound.Play("User_heal", Sound.UI);
            GetComponentInParent<PlayerController_KSM>().OnHealed(-_health);
            GetComponentInParent<Inventory>().UsedItem();
            DestroySelf();
        }
    }
    public override void OnPickUp()
    {
        this.transform.localRotation= Quaternion.Euler(0f, 270f, 30f);
    }
    public override void OnDropped()
    {
        this.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
