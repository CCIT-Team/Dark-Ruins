using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Rifle : GunBase
{
    [SerializeField]
    private GameObject _arm;
    private Animator _anim;
    protected override void Start()
    {
        base.Start();
        _fireLength = 1.0f;
        SetData();
        _up = new Vector3(0, 0f, 0);
        _f = new Vector3(0, 0, 0);
        _anim = _arm.GetComponent<Animator>();
    }
    public override void Fire()
    {
        _anim.Play("Rifle Fire", 0, 0f);
        base.Fire();

    }

    public override void Reload(out bool ob)
    {
        base.Reload(out ob);
        if (ob == true)
        {
            _anim.SetTrigger("reloading");
        }
    }
    public override void Init()
    {
        if (transform.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            _arm.SetActive(true);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Arms"))
        {
            return;
        }

    }
    public override void OnPickUp()
    {
        if (this.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            return;
        }
        base.OnPickUp();
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
#if UNITY_EDITOR
        Debug.Log(data["AttackSpeed"]);
#endif
        _fireCooltimeSet= Convert.ToSingle(data["AttackSpeed"]);
        _maxBullet = Convert.ToInt32(data["MaxAmmo"]);
    }
}
