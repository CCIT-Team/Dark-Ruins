using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletItem : ItemBase
{
    protected Slot _slot;
    public BulletsPool.Bullets Bullet;
    protected override void Start()
    {
        base.Start();
        Length = 1;
        SetData();
        if (transform.parent != null && transform.parent.TryGetComponent<Slot>(out Slot s))
        {
            _slot = s;
        }
    }
    public override void ItemUse(List<KeyCode> keys=null)
    {
        
    }
    public void Re()
    {
        _count--;
        if(_count == 0)
        {
            _slot.Clears();
            Destroy(this.gameObject);
        }
    }
    public override void Init()
    {
        base.Init();
        if (transform.parent.TryGetComponent<Slot>(out Slot s))
        {
            _slot = s;
#if UNITY_EDITOR
            Debug.Log(_slot.name);
#endif
        }
        else
        {
            _slot = default(Slot);
        }
    }
}
