using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private ItemBase[] _inventory;
    private int _index, _inventoryCapacity;
    public Action _itemUsing;
    private Inventory()
    {
        Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
    }
    public bool TryItemGet(out int value,ItemBase item)
    {
        //인벤토리 공간 꽉차거나, 아이템 개수 꽉찼을 때?
        value = -1;
        if(item is null)
        {
            return false;
        }
        int index = Array.LastIndexOf(_inventory, item);
        if (index!=-1)
        {
            value = index;
            return true;
        }
        return false;
    }

    public void OnKeyPressed(List<KeyCode> keys)
    {
        //단축키
    }

    private void ItemUse()
    {
        
    }
}
