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
        _rb.useGravity = false;
        _rb.isKinematic=true;
    }

    public override void ItemUse(List<KeyCode> keys)
    {
        if(keys.Contains(KeyCode.Mouse0))
        {
            Drop();
            transform.SetParent(null);
            _rb.useGravity = true;
            _rb.isKinematic=false;
        }
    }

    public override void OnPickUp()
    {
        _rb.useGravity = false;
        _rb.isKinematic=true;
    }
}
