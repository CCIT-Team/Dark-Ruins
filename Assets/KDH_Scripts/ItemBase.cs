using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public abstract class ItemBase : MonoBehaviour,IDrawOutLine
{
    [SerializeField]
    private int _max;
    public int Max { get { return _max; } }
    [SerializeField]
    protected int _count=0;
    public int Count { get { return _count; } }
    public int Length;
    public bool _dropped = false,In=false;
    public Outline OutLine;

    protected virtual void Start()
    {
        OutLine = GetComponent<Outline>();
        Length = 1;
        if (OutLine == null)
        {
            OutLine = gameObject.AddComponent<Outline>();
            OutLine.OutlineMode = Outline.Mode.OutlineAll;
            OutLine.OutlineColor = new Color(1.5f, 0.2f, 1.5f, 1.0f);
            OutLine.OutlineWidth = 4f;
        }
        OutLine.enabled = false;
        //var data = DataLoader.Instance.FindByName(this.GetType().Name);
        //if (data == null)
        //{
        //    return;
        //} 

        //if (transform.parent != null)
        //{
        //    Vector3 p = transform.parent.lossyScale;
        //    transform.localScale = new Vector3(
        //        targetWorldScale.x / p.x,
        //        targetWorldScale.y / p.y,
        //        targetWorldScale.z / p.z
        //    );
        //}
        //else
        //{
        //    transform.localScale = targetWorldScale;
        //}
    }
    public void Drop()
    {
        if(_dropped==true)
        {
            Inventory.InventoryOpened = false;
            _dropped = false;
#if UNITY_EDITOR
            Debug.Log("�̰ų�Ÿ����");
#endif
        }
    }
    private void OnDestroy()
    {
        if(SceneManager.GetActiveScene().name=="GameScene")
        {
            return;
        }
        Unsubscribe();
    }
    public virtual void SetData() //�� ��� �� ������ ������ ���⼭ override�� base ����
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
        //Ű�Է� ���� �� ��� ���
        Managers_KSM.Input.OnKeysHeld -= ItemUse;
        Managers_KSM.Input.OnKeysHeld += ItemUse;
        PickUp();
    }
    public void Unsubscribe()
    {
        Managers_KSM.Input.OnKeysHeld -= ItemUse;
        OnDropped();
    }
    //public abstract void ItemUse(Ű�Է�);
    protected void PickUp()
    {
        //GetItem(); //���� ���߿� �ݱ� �߰��Ҷ� ����
        OnPickUp();
    }
    public virtual void OnPickUp()
    {
        //�������� �ֿ��� ��� �ߵ��� ������ �ִٸ� ���⿡
        _dropped = false;
    }
    protected void Dropped()
    {
        OnDropped();
    }
    public virtual void OnDropped()
    {

    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    public void OnFocused()
    {
#if UNITY_EDITOR
        Debug.Log($"{name} ���̷� ���õ�");
#endif
        OutLine.enabled = true;
    }
    public void OutFocused()
    {
        OutLine.enabled = false;
    }
    public virtual void Init()
    {

    }
    protected virtual void DropItem()
    {

        if(Input.GetKeyDown(KeyCode.Q)&&_dropped==false)
        {
            Vector3 v = this.transform.position;
            transform.SetParent(null, false);
            this.transform.position = v;
            GetComponent<Collider>().enabled = true;
            Inventory.UsedItem();
            _dropped = true;

        }

    }
}
