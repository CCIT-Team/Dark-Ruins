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
            Drop();
            transform.SetParent(null);
        }
    }

}
