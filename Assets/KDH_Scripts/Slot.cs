using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private int _mainIndex;

    private Renderer rd;
    private MaterialPropertyBlock mpb;

    public List<int> _slots=new();
    public int _thisIndex;
    public int MainIndex { get { return _mainIndex; } }
    [SerializeField]
    private ItemBase _item=default(ItemBase);
    public ItemBase Item => Inventory.InventorySlot[_mainIndex]._item;

    public bool IsEquipped = false;
    private void Awake()
    {
        _thisIndex=int.Parse(name.Substring(name.LastIndexOf('_') + 1));
        rd = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        ColorSet(IsEquipped);
    }
    public void Clears()
    {
        _mainIndex = _thisIndex;
        _item=default(ItemBase);
        IsEquipped = false;
        ColorSet(false);
#if UNITY_EDITOR
        Debug.Log(Item);
#endif
        foreach(int s in _slots)
        {
            Inventory.InventorySlot[s].IsEquipped = false;
            Inventory.InventorySlot[s].ResetIndex();
        }
        _slots.Clear();
    }
    private void ColorSet(bool b)
    {
        rd.GetPropertyBlock(mpb);
        if (b==true)
        {
            mpb.SetColor("_Color", Color.black);
        }
        else
        {
            mpb.SetColor("_Color", Color.white);
        }
        rd.SetPropertyBlock(mpb);
    }
    public void SetItem(ItemBase item)
    {
        _item = item;
    }
    public void ResetIndex()
    {
        _mainIndex = _thisIndex;
        ColorSet(false);
    }
    public void SetIndex(int index)
    {

        if (IsEquipped==true&&Inventory.InventorySlot[index]._slots.Contains(_thisIndex) == true)
        {

            return;
        }
        ColorSet(true);
        IsEquipped = true;
        _mainIndex = index;
        if (index != _thisIndex)
        {
            Inventory.InventorySlot[index]._slots.Add(_thisIndex);
        }    
    }
}
