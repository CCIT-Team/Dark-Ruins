using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;
using static UnityEditor.Progress;

public class Inventory :MonoBehaviour
{
    public static Slot[] InventorySlot=new Slot[12];
    //private int _index, _inventoryCapacity; //나중에 쓰게 될 것
    public Action _itemUsing;
    private bool _drag=false;
    public static bool InventoryOpened = false;
    private Transform _inventoryView,_dragItem;
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
            InventorySlot[n].ResetIndex();
        }
    }
    private void Start()
    {
        Managers_KSM.Input.OnKeysHeld -= OnKeyPressed;
        Managers_KSM.Input.OnKeysHeld += OnKeyPressed;
        _inventoryView = GameObject.Find("InventoryView").transform;
        cam = Camera.main;
        BindSlots();
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
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenOrCloseInventory();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (DragItem() == false && _drag == false)
            {
                PickUp();
            }
            else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 4.0f, 1 << LayerMask.NameToLayer("Item")) && hit.transform.TryGetComponent<ItemBase>(out ItemBase item)&&item.In==false)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (InventorySlot[i].Item is default(ItemBase))
                    {
                        if (SetItem(i, item) == true)
                        {
                            break;
                        }
                    }
                }

            }
        }
        //단축키 및 아이템 사용
        ItemUse(keys);
    }
    public void UsedItem()
    {
        _drag = false;
        _dragItem = null;
    }
    public void PickUp() //필드에서 줍기
    {
#if UNITY_EDITOR
        Debug.Log("쐈움");
#endif
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit,4.0f, 1 << LayerMask.NameToLayer("Item")) &&hit.transform.TryGetComponent<ItemBase>(out ItemBase item))
        {
#if UNITY_EDITOR
            Debug.Log("주울 수 있움");
#endif
            if(item is BulletItem || item is GunBase)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (InventorySlot[i].Item is default(ItemBase))
                    {
                        if (SetItem(i, item) == true)
                        {
                            return;
                        }
                    }
                }
            }
            _dragItem = item.transform;

            _dragItem.SetParent(this.transform.Find("Main Camera"), false);
            _dragItem.localPosition = new Vector3(0, -0.3f, 1.3f);
            _dragItem.localRotation = Quaternion.identity;

            _drag = true;
            item.OutFocused();
            StartCoroutine(SubCriber());
            //_dragItem.gameObject.SetActive(false);
            _dragItem.GetComponent<ItemBase>().Init();
            _dragItem.GetComponent<ItemBase>().In = false;
            DragFalse();
            _dragItem.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void DragFalse()
    {
        if(_dragItem.CompareTag("Arms")==false)
        {
            return;
        }
        //_dragItem.GetComponent<ItemBase>().Unsubscribe();
        _dragItem.gameObject.SetActive(false);
    }
    public void Changer<T>(ItemBase item=default(ItemBase))
    {
        ItemBase t=default(ItemBase);
        for (int i = 0; i < 12; i++)
        {
            if (InventorySlot[i].Item is T)
            {
                t = InventorySlot[i].Item;
                InventorySlot[i].Clears();
                ItemBase ib = _dragItem.GetComponent<ItemBase>();
                if(item is not Knife3_KSM)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (InventorySlot[j].Item is default(ItemBase))
                        {
                            if (SetItem(j, ib) == true)
                            {
                                break;
                            }
                        }
                    }
                    _dragItem = t.transform;
                    _dragItem.SetParent(this.transform.Find("Main Camera"), false);
                    _dragItem.localPosition = new Vector3(0, -0.3f, 1.3f);
                    _dragItem.localRotation = Quaternion.identity;
                }    
            }
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
                _dragItem.GetComponent<ItemBase>().In = false;
                //_dragItem.gameObject.SetActive(false);
                DragFalse();
                _dragItem.gameObject.GetComponent<Collider>().enabled = false;
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
                if(SetItem(int.Parse(slot.transform.name.Substring(slot.transform.name.LastIndexOf('_') + 1)),default(ItemBase))==true)
                {
                    _drag = false;
                    _dragItem = null;
                }
                _inventoryView.gameObject.GetChild<Transform>("UI_Root").gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }
    IEnumerator SubCriber()
    {
        yield return new WaitForSeconds(1.0f);
        if(_dragItem !=null&&_dragItem.gameObject.activeSelf==true)
        {
            _inventoryView.gameObject.GetChild<Transform>("UI_Root").gameObject.SetActive(true);
            _inventoryView.gameObject.GetChild<Transform>("UI_Root").GetComponent<LookPlayer>().On(_dragItem.GetComponent<ItemBase>());
            //_dragItem.GetComponent<ItemBase>().Unsubscribe();
            _dragItem.GetComponent<ItemBase>().Subscribe();
        }
    }
    public void OpenOrCloseInventory()
    {
        if(InventoryOpened==true)
        {
            Managers_YGU.Sound.Play("Zipper_Close", Sound.UI);
            InventoryOpened = false;
            _inventoryView.position = new Vector3(0,1.5f,0)+this.gameObject.transform.position+this.gameObject.transform.forward * 2.5f;
            //_inventoryView.transform.forward = cam.transform.position - _inventoryView.transform.position;
            _inventoryView.transform.rotation = Quaternion.LookRotation(cam.transform.position - _inventoryView.transform.position) * Quaternion.Euler(0f, 40f, 0f);

            _inventoryView.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            Managers_YGU.Sound.Play("Zipper_Open", Sound.UI);
            InventoryOpened = true;
            _inventoryView.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
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
    public bool SetItem(int mainIndex,ItemBase item,bool xy=true) //직접 수집이나 그런거 외로도 획득 경로 있을까봐 빼둠, 일단 가로형만
    {
        if(item is default(ItemBase))
        {
            item = _dragItem.GetComponent<ItemBase>();
        }
        if (Check(mainIndex,item,xy)==false)
        {
            return false;
        }
        InventorySlot[mainIndex].SetItem(item);
        item.In = true;
        item.transform.SetParent(InventorySlot[mainIndex].transform, false);
        item.transform.localPosition = new Vector3(0, 0.65f, 0);
        item.transform.localRotation =Quaternion.identity;
        item.Init();
        item.gameObject.SetActive(true);
        item.gameObject.GetComponent<Collider>().enabled = true;
        item.Unsubscribe();

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
            for (int i = mainIndex; i < mainIndex + item.Length*3; i += 3)
            {
                InventorySlot[i].SetIndex(mainIndex);
                //_dragItem.GetComponent<Collider>().enabled = true;
            }
        }

        return true;
    }
    private bool Check(int mainIndex, ItemBase item,bool xy) //bool xy의 경우 true면 ㅡ false면 ㅣ모양
    {
        if (xy==true)
        {
            if (mainIndex%3+item.Length>3)
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
            if (mainIndex+item.Length*3-3>=12)
            {
                return false;
            }
            for (int i = mainIndex; i < mainIndex + item.Length; i += 3)
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
            if(s.Item is default(ItemBase))
            {
                continue; 
            }
            else if (s.Item is T)
            {
                return s.Item;
            }
        }
        return null;
    }
    public List<ItemBase> CheckItems<T>()
    {
        List<ItemBase> ib=new List<ItemBase>();
        foreach (Slot s in InventorySlot)
        {
            if (s.Item is default(ItemBase))
            {
                continue;
            }
            else if (s.Item is T && ib.Contains(s.Item)==false)
            {
                ib.Add(s.Item);
            }
        }
        return ib;
    }
    private void ItemUse(List<KeyCode> keys)
    {
        _itemUsing?.Invoke();
    }
}
