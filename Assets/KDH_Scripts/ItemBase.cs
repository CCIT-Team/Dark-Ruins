using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]
    private int _max;
    public int Max { get { return _max; } }
    [SerializeField]
    protected int _count=0;
    public int Count { get { return _count; } }
    public int Length;
    public bool _dropped = false;
    public Outline Outline;

    protected virtual void Start()
    {
        Outline = GetComponent<Outline>();

        if (Outline == null)
        {
            Outline = gameObject.AddComponent<Outline>();
            Outline.OutlineMode = Outline.Mode.OutlineAll;
            Outline.OutlineColor = new Color(1.5f, 0.2f, 1.5f, 1.0f);
            Outline.OutlineWidth = 4f;
        }
        Outline.enabled = false;
    }
    public void Drop()
    {
        if(_dropped==true)
        {
            Inventory.InventoryOpened = false;
            _dropped = false;
#if UNITY_EDITOR
            Debug.Log("이거나타났움");
#endif
        }
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    public virtual void SetData() //더 들고 올 데이터 있으면 여기서 override랑 base 쓰기
    {
        var data=DataLoader.Instance.FindByName(this.GetType().Name);
        if (data==null)
        {
            return;
        }
        _max= Convert.ToInt32(data["MAX"]);
        _count = Convert.ToInt32(data["COUNT"]);
    }
    public void GetItem(int value)
    {
        _max += value;
    }
    public abstract void ItemUse(List<KeyCode> keys);

    public void Subscribe()
    {
        //키입력 구독 및 취소 기능
        Managers_KSM.Input.OnKeysHeld += ItemUse;
    }
    public void Unsubscribe()
    {
        Managers_KSM.Input.OnKeysHeld -= ItemUse;
    }
    //public abstract void ItemUse(키입력);
    public void PickUp()
    {
        //GetItem(); //대충 나중에 줍기 추가할때 대입
        OnPickUp();
    }
    public void OnPickUp()
    {
        //아이템이 주웠을 경우 발동할 로직이 있다면 여기에
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    public void OnFocused()
    {
#if UNITY_EDITOR
        Debug.Log($"{name} 레이로 선택됨");
#endif
        Outline.enabled = true;
    }
    public void OutFocused()
    {
        Outline.enabled = false;
    }
    public virtual void Init()
    {

    }
}
