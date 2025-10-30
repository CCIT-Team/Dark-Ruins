using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private int _mainIndex;

    public List<int> _slots=new();
    public int _thisIndex;
    public int MainIndex { get { return _mainIndex; } }
    [SerializeField]
    private ItemBase _item;
    public ItemBase Item => Inventory.InventorySlot[_mainIndex]._item;

    public bool IsEquipped = false;
    private void Awake()
    {
        _thisIndex=int.Parse(name.Substring(name.LastIndexOf('_') + 1));
    }
    public void Clears()
    {
        _mainIndex = _thisIndex;
        _item=default(ItemBase);
        IsEquipped = false;
        foreach(int s in _slots)
        {
            Inventory.InventorySlot[s].IsEquipped = false;
            Inventory.InventorySlot[s].SetIndex(Inventory.InventorySlot[s]._thisIndex);
        }
        _slots.Clear();
    }
    public void SetItem(ItemBase item)
    {
        _item = item;
    }
    public void SetIndex(int index)
    {
        
        if (IsEquipped==true&&Inventory.InventorySlot[index]._slots.Contains(_thisIndex) == true)
        {
            return;
        }
        IsEquipped = true;
        _mainIndex = index;
        if (index != _thisIndex)
        {
            Inventory.InventorySlot[index]._slots.Add(_thisIndex);
        }    
    }
}
