using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory :MonoBehaviour
{
    public static Slot[] InventorySlot=new Slot[25];
    private int _index, _inventoryCapacity;
    public Action _itemUsing;
    private bool _inventoryOpened = false,_drag=false;
    private Transform _inventoryView,_dragItem;
    private float _inventoryCooltime = 0.0f,_dragCooltime=0.0f;
    private Camera cam;
    //private Inventory()
    //{
    //    Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
    //}
#if UNITY_EDITOR
    private void TestSample()
    {

        foreach(Transform t in _inventoryView)
        {
            int n = int.Parse(t.name.Substring(t.name.LastIndexOf('_') + 1));
            InventorySlot[n] =t.GetComponent<Slot>();
            InventorySlot[n].SetIndex(n);
        }
    }
#endif
    private void Awake()
    {
        Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
        _inventoryView = GameObject.Find("InventoryView").transform;
        cam = GetComponentInChildren<Camera>();
#if UNITY_EDITOR
        TestSample();
#endif
    }
    public void FixedUpdate()
    {
        if (_inventoryCooltime > 0)
        {
            _inventoryCooltime -= Time.fixedDeltaTime;
        }
        if(_dragCooltime>0)
        {
            _dragCooltime -= Time.fixedDeltaTime;
        }
    }
    public void OnKeyPressed(List<KeyCode> keys)
    {
        if(_inventoryCooltime<=0&&keys.Contains(KeyCode.I))
        {
            _inventoryCooltime = 2.0f;
            OpenOrCloseInventory();
        }
        if(_inventoryView==true&& _dragCooltime<=0&&keys.Contains(KeyCode.Mouse0))
        {
            _dragCooltime = 1.0f;
            DragItem();
        }
        //단축키 및 아이템 사용
        ItemUse(keys);
    }
    private void DragItem()
    {
        if (_drag==false)
        {
            //바닥에서 줍기도? 아니다, 분할하자
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit) && hit.transform.TryGetComponent<Slot>(out Slot slot)&&slot.Item is not null)
            {
                _dragItem = slot.Item.transform;
                ClickItem(int.Parse(slot.transform.name.Substring(slot.transform.name.LastIndexOf('_') + 1)));
                _drag = true;
                StartCoroutine(SubCriber());

                _dragItem.GetComponent<Collider>().enabled = false;
            }
        }
        else
        {
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit) && hit.transform.TryGetComponent<Slot>(out Slot slot))
            {
                SetItem(int.Parse(slot.transform.name.Substring(slot.transform.name.LastIndexOf('_') + 1)));
            }
        }
    }
    IEnumerator SubCriber()
    {
        yield return new WaitForSeconds(1.0f);
        _dragItem.GetComponent<ItemBase>().Subscribe();
    }
    public void OpenOrCloseInventory()
    {
        if(_inventoryOpened==true)
        {
            _inventoryOpened = false;
            _inventoryView.position = this.gameObject.transform.position+this.gameObject.transform.forward * 3f;
            _inventoryView.gameObject.SetActive(true);
        }
        else
        {
            _inventoryOpened = true;
            _inventoryView.gameObject.SetActive(false);
        }
    }
    public void ClickItem(int mainIndex)//무엇을 어떻게? 상호작용 어케함 우리? 일단 해두는데 트리거가 없는;;
    {
        _dragItem.SetParent(this.transform.Find("Main Camera"), false);
        _dragItem.localPosition =new Vector3(0,-0.3f,1.3f);
        _dragItem.localRotation = Quaternion.identity;

        //아무튼 저장해두었다가 드래그든 뭐든 옮긴다면 SetItem 호출해서 되면 거기로 옮겨가고 안되면 복귀
    }
    public void SetItem(int mainIndex,bool xy=true) //직접 수집이나 그런거 외로도 획득 경로 있을까봐 빼둠, 일단 가로형만
    {
        ItemBase item=_dragItem.GetComponent<ItemBase>();
        if (Check(mainIndex,item,xy)==false)
        {
            return;
        }
        InventorySlot[mainIndex].SetItem(item);
        _dragItem.SetParent(InventorySlot[mainIndex].transform, false);
        _dragItem.localPosition = new Vector3(0, 0.65f, 0);
        _dragItem.localRotation=Quaternion.identity;
        item.Unsubscribe();
        _drag = false;
        if(xy==true)
        {
            for (int i = mainIndex; i < mainIndex + item.Length; i++)
            {
                InventorySlot[i].SetIndex(mainIndex);
                //_dragItem.GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            for (int i = mainIndex; i < mainIndex + item.Length; i += 3)
            {
                InventorySlot[i].SetIndex(mainIndex);
                //_dragItem.GetComponent<Collider>().enabled = true;
            }
        }
        _dragItem = null;
    }
    private bool Check(int mainIndex, ItemBase item,bool xy) //bool xy의 경우 true면 ㅡ false면 ㅣ모양
    {
        if(xy==true)
        {
            for (int i = mainIndex; i < mainIndex + item.Length; i++)
            {
                if (i / 5 != mainIndex / 5 && InventorySlot[i].Item is not null)
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = mainIndex; i < mainIndex + item.Length; i += 5)
            {
                if (i > 24 && InventorySlot[i].Item is not null)
                {
                    return false;
                }
            }
        }
        return true;
        //switch (xy)
        //{
        //    case true:
        //        for (int i = mainIndex; i < mainIndex + item.Length; i++)
        //        {
        //            if (i / 5 != mainIndex / 5 && InventorySlot[i].Item is not null)
        //            {
        //                return false;
        //            }
        //        }

        //        return true;
        //    case false:
        //        for (int i = mainIndex; i < mainIndex + item.Length; i += 5)
        //        {
        //            if (i > 24 && InventorySlot[i].Item is not null)
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //}
    }
    private void ItemUse(List<KeyCode> keys)
    {
        _itemUsing?.Invoke();
    }
}
