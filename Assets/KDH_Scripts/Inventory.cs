using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public static Slot[] InventorySlot=new Slot[9];
    private int _index, _inventoryCapacity;
    public Action _itemUsing;
    private Inventory()
    {
        Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
    }
    //public int TryItemGet(ItemBase item)
    //{
    //    //인벤토리 공간 꽉차거나, 아이템 개수 꽉찼을 때?
    //    if(item is null)
    //    {
    //        return -1;
    //    }
    //    int index = Array.LastIndexOf(_inventory, item);
    //    if (index!=-1)
    //    {
    //        if()
    //        {
    //            return item.Count;
    //        }
    //        else
    //        {
                
    //        }
    //    }
    //    else
    //    {
    //        _inventory[++_index] = item;
    //    }
    //}

    public void OnKeyPressed(List<KeyCode> keys)
    {
        //단축키
    }
    public void ClickItem(int mainIndex)//무엇을 어떻게? 상호작용 어케함 우리? 일단 해두는데 트리거가 없는;;
    {
        ItemBase item = InventorySlot[mainIndex].Item;
        //아무튼 저장해두었다가 드래그든 뭐든 옮긴다면 SetItem 호출해서 되면 거기로 옮겨가고 안되면 복귀
    }
    public void SetItem(ItemBase item, int mainIndex,bool xy) //직접 수집이나 그런거 외로도 획득 경로 있을까봐 빼둠
    {
        if(Check(mainIndex,item,xy)==false)
        {
            return;
        }
        InventorySlot[mainIndex].SetItem(item);
        switch (xy)
        {
            case true:
                for (int i = mainIndex; i < item.Length; i++)
                {
                    InventorySlot[i].SetIndex(mainIndex);
                }
                break;
                
            case false:
                for (int i = mainIndex; i < item.Length; i += 3)
                {
                    InventorySlot[i].SetIndex(mainIndex);
                }
                break;
        }
    }
    private bool Check(int mainIndex, ItemBase item,bool xy) //bool xy의 경우 true면 ㅡ false면 ㅣ모양
    {
        switch(xy)
        {
            case true:
                for (int i = mainIndex; i <item.Length;i++)
                    if (i/3!=mainIndex/3 &&InventorySlot[i].Item is not null)
                    {
                        return false;
                    }
                return true;
            case false:
                for (int i = mainIndex; i < item.Length; i+=3)
                    if (i>8 && InventorySlot[i].Item is not null)
                    {
                        return false;
                    }
                return true;
        }
    }
    private void ItemUse()
    {
        
    }
}
