using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

public class Inventory :MonoBehaviour
{
    public static Slot[] InventorySlot=new Slot[25];
    //private int _index, _inventoryCapacity; //나중에 쓰게 될 것
    public Action _itemUsing;
    private bool _drag=false;
    public static bool InventoryOpened = false;
    private Transform _inventoryView,_dragItem;
    private float _inventoryCooltime = 0.0f,_dragCooltime=0.0f;
    private Camera cam;
    //private Inventory()
    //{
    //    Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
    //}
    private void BindSlots()
    {

        foreach(Transform t in _inventoryView)
        {
            if(t.name=="UI_Root")
            {
                continue;
            }
            int n = int.Parse(t.name.Substring(t.name.LastIndexOf('_') + 1));
            InventorySlot[n] =t.GetComponent<Slot>();
            InventorySlot[n].SetIndex(n);
        }
    }
    private void Start()
    {
        Managers_KSM.Input.OnKeysHeld -= OnKeyPressed;
        Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
        _inventoryView = GameObject.Find("InventoryView").transform;
        cam = GetComponentInChildren<Camera>();
        BindSlots();
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
    public void Sub()
    {
        Managers_KSM.Input.OnKeysHeld -= OnKeyPressed;
        Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
    }
    public void UnSub()
    {
        Managers_KSM.Input.OnKeysHeld -= OnKeyPressed;
    }
    public void OnKeyPressed(List<KeyCode> keys)
    {
        if(_inventoryCooltime<=0&&keys.Contains(KeyCode.I))
        {
            _inventoryCooltime = 2.0f;
            OpenOrCloseInventory();
        }
        if(InventoryOpened==false&& _dragCooltime<=0&&keys.Contains(KeyCode.Mouse0))
        {
            _dragCooltime = 1.0f;
            if (DragItem() == false && _drag == false)
            {
                PickUp();
            }
        }
        //단축키 및 아이템 사용
        ItemUse(keys);
    }
    public void PickUp() //필드에서 줍기
    {
#if UNITY_EDITOR
        Debug.Log("쐈움");
#endif
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit)&&hit.transform.TryGetComponent<ItemBase>(out ItemBase item))
        {
#if UNITY_EDITOR
            Debug.Log("주울 수 있움");
#endif
            _dragItem = item.transform;
            _dragItem.SetParent(this.transform.Find("Main Camera"), false);
            _dragItem.localPosition = new Vector3(0, -0.3f, 1.3f);
            _dragItem.localRotation = Quaternion.identity;
            _dragItem.GetComponent<ItemBase>().Init();
            _drag = true;
            item.OutFocused();
            StartCoroutine(SubCriber());
            _dragItem.GetComponent<Collider>().enabled = false;
        }
    }
    private bool DragItem() 
    {
        if (_drag==false) //인벤토리에서 꺼내기
        {
            //바닥에서 줍기도? 아니다, 분할하자
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit,8f, 1 << LayerMask.NameToLayer("Slot")) && hit.transform.TryGetComponent<Slot>(out Slot slot)&&slot.Item is not null)
            {
                _dragItem = slot.Item.transform;
                _dragItem.GetComponent<ItemBase>().OutFocused();
                ClickItem(Inventory.InventorySlot[int.Parse(slot.transform.name.Substring(slot.transform.name.LastIndexOf('_') + 1))].MainIndex);
                //ClickItem();
                _drag = true;
                StartCoroutine(SubCriber());
                _dragItem.GetComponent<ItemBase>().Init();
                _dragItem.GetComponent<Collider>().enabled = false;
                return true;
            }
        }
        else //인벤토리에 집어넣기
        {
#if UNITY_EDITOR
            Debug.DrawRay(
                cam.transform.position,
                cam.transform.forward,
                Color.red,
                4f
            );
#endif
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 8f, 1 << LayerMask.NameToLayer("Slot")) && hit.transform.TryGetComponent<Slot>(out Slot slot))
            {
                SetItem(int.Parse(slot.transform.name.Substring(slot.transform.name.LastIndexOf('_') + 1)));
                _inventoryView.gameObject.GetChild<Transform>("UI_Root").gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }
    IEnumerator SubCriber()
    {
        yield return new WaitForSeconds(1.0f);
        if(_dragItem !=null)
        {
            _inventoryView.gameObject.GetChild<Transform>("UI_Root").gameObject.SetActive(true);
            _inventoryView.gameObject.GetChild<Transform>("UI_Root").GetComponent<LookPlayer>().On(_dragItem.GetComponent<ItemBase>());
            _dragItem.GetComponent<ItemBase>().Unsubscribe();
            _dragItem.GetComponent<ItemBase>().Subscribe();
        }
    }
    public void OpenOrCloseInventory()
    {
        if(InventoryOpened==true)
        {
            InventoryOpened = false;
            _inventoryView.position = new Vector3(0,1.5f,0)+this.gameObject.transform.position+this.gameObject.transform.forward * 3f;
            _inventoryView.gameObject.SetActive(true);
        }
        else
        {
            InventoryOpened = true;
            _inventoryView.gameObject.SetActive(false);
        }
    }
    public void ClickItem(int mainIndex)//무엇을 어떻게? 상호작용 어케함 우리? 일단 해두는데 트리거가 없는;;
    {
        _dragItem.SetParent(this.transform.Find("Main Camera"), false);
        _dragItem.localPosition =new Vector3(0,-0.3f,1.3f);
        _dragItem.localRotation = Quaternion.identity;
        InventorySlot[mainIndex].Clears();
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
        _dragItem.GetComponent<ItemBase>().Init();
        _dragItem.GetComponent<Collider>().enabled = true;
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
            for (int i = mainIndex; i < mainIndex + item.Length*5; i += 5)
            {
                InventorySlot[i].SetIndex(mainIndex);
                //_dragItem.GetComponent<Collider>().enabled = true;
            }
        }
        _dragItem = null;
    }
    private bool Check(int mainIndex, ItemBase item,bool xy) //bool xy의 경우 true면 ㅡ false면 ㅣ모양
    {
        if (xy==true)
        {
            if (mainIndex%5+item.Length>5)
            {
                return false;
            }
            for (int i = mainIndex; i < mainIndex + item.Length; i++)
            {
#if UNITY_EDITOR
                Debug.Log(InventorySlot[i].Item);
#endif
                if (InventorySlot[i].Item !=default(ItemBase))
                {
                    return false;
                }
            }
        }
        else
        {
            if (mainIndex+item.Length*5>24)
            {
                return false;
            }
            for (int i = mainIndex; i < mainIndex + item.Length; i += 5)
            {
                if (InventorySlot[i].Item != default(ItemBase))
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
    public ItemBase CheckItem<T>()
    {
        foreach(Slot s in InventorySlot)
        {
            if (s.Item is T)
            {
                return s.Item;
            }
        }
        return null;
    }
    private void ItemUse(List<KeyCode> keys)
    {
        _itemUsing?.Invoke();
    }
}
