using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : ItemBase
{
    private Rigidbody _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }
        _rb.useGravity=false;
        _rb.isKinematic=true;
    }

    public override void ItemUse(List<KeyCode> keys)
    {
        if(keys.Contains(KeyCode.Q))
        {
            
            DropItem();
        }
    }

    protected override void DropItem()
    {
        if(_dropped)
        {
            return;
        }
        Vector3 v = this.transform.position;
        transform.SetParent(null, false);
        this.transform.position = v;
        GetComponent<Collider>().enabled = true;
        Inventory.UsedItem();
        _dropped = true;
        _rb.useGravity=true;
        _rb.isKinematic=false;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        _rb.useGravity=false;
        _rb.isKinematic=true;
        transform.localPosition = new Vector3(0, -0.3f, 1.7f);
    }

}
