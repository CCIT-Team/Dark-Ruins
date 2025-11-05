using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : ItemBase
{
    private void Awake()
    {
        Length = 1;
        SetData();
    }
    public override void ItemUse(List<KeyCode> keys=null)
    {
        
    }
    public void Re()
    {
        _count--;
        if(_count == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
