using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem_KSM : ItemBase
{
    protected override void Awake()
    {
        base.Awake();
        if (_count == 0)
        {
            _count = 1;
        }

        Length = 1;
    }

    public override void ItemUse(List<KeyCode> keys = null)
    {

    }

    public void Consume()
    {
        if (_count > 0)
        {
            _count--;
            Debug.Log($"배터리 아이템 소모. 남은 수량: {_count}");

            if (_count <= 0)
            {
                Slot mySlot = GetComponentInParent<Slot>();

                if (mySlot != null)
                {
                    mySlot.Clears();
                }

                Destroy(this.gameObject);
            }
        }
    }
}
