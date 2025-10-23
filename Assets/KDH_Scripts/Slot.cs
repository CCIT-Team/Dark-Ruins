using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private int mainIndex;
    public int MainIndex { get { return mainIndex; } }
    [SerializeField]
    private ItemBase _item;
    public ItemBase Item => Inventory.InventorySlot[mainIndex]._item;

    public void Clear()
    {
        mainIndex = -1;
    }
    public void SetItem(ItemBase item)
    {
        _item = item;
    }
    public void SetIndex(int index)
    {
        mainIndex = index;
    }
}
