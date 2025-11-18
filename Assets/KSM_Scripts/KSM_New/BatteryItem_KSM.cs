using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem_KSM : ItemBase
{
    private void Start()
    {
        if (_count == 0)
        {
            _count = 1;
        }

        Length = 1;
    }

    public override void ItemUse(List<KeyCode> keys = null)
    {
        if (_count > 0)
        {
            _count--;
            Debug.Log("배터리 사용, 남은 개수: " + _count);
        }
    }
}
