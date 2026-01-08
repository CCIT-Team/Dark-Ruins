using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock : GunBase
{
    [SerializeField]
    private GameObject _arm;
    private Animator _anim;
    protected override void Start()
    {
        base.Start();
        _fireLength = 1.5f;
        _up = new Vector3(0, 0.0f, 0);
        _f = new Vector3(0f, 90f, 90f);
        _anim=_arm.GetComponent<Animator>();
        _bandong = 1.2f;
    }
    public override void Fire()
    {
        _anim.Play("gun shot", 0, 0f);
        Managers_YGU.Sound.Play("Handgun_Shot",eSound.UI);
        base.Fire();
    }

    public override void Reload(out bool ob)
    {
        base.Reload(out ob);
        if(ob==true)
        {
            Managers_YGU.Sound.Play("Handgun_Reload", eSound.UI);
            _anim.SetTrigger("reloading");
        }
    }
    public override void Init()
    {
        if(this.transform.gameObject.layer==LayerMask.NameToLayer("Arms"))
        {
            return;
        }
        _arm.SetActive(true);
    }
    public override void OnPickUp()
    {
        if (this.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            return;
        }
        base.OnPickUp();
        Managers_YGU.Sound.Play("Handgun_Equip",eSound.UI);
    }

    public override void OnDropped()
    {
        if (this.transform.gameObject.layer == LayerMask.NameToLayer("Arms"))
        {
            return;
        }
        base.OnDropped();
        _arm.SetActive(false);
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
