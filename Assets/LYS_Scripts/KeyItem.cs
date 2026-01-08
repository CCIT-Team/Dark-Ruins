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
    }

    public override void ItemUse(List<KeyCode> keys)
    {
        if(keys.Contains(KeyCode.Mouse0))
        {
            DropItem();
            transform.position = gameObject.transform.parent.position;
            transform.SetParent(null);
        }
    }


    public override void DropItem()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
    }


    public override void OnPickUp()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

}
