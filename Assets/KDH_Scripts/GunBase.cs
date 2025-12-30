using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : ItemBase,IWeapon_KSM
{
    [SerializeField]
    protected int _loadedBullet=0, _maxBullet=8;
    protected float _fireCooltime = 0f;
    protected float _fireCooltimeSet=0.5f;
    protected bool _zoomOuted = true;
    
    protected float _fireLength;
    protected BulletsPool _bulletsPool;

    protected BulletsPool.Bullets _bullet;

    protected Vector3 _up = new Vector3(0, 0, 0);
    protected Vector3 _f=new Vector3(0, 0,0);

    public float rate { get =>_fireCooltimeSet; }
    public void Use()
    {
        ItemUse(default(List<KeyCode>));
    }
    protected override void Start()
    {
        base.Start();
        Length = 2; //³ªÁß¿¡ ±âÈ¹ ³ª¿À¸é Àû¿ëÇÏ¸é µÊ ¤·¤·
        _bulletsPool =FindObjectOfType<BulletsPool>().GetComponent<BulletsPool>();
        _bullet = (BulletsPool.Bullets)Enum.Parse(typeof(BulletsPool.Bullets), this.GetType().Name);
#if UNITY_EDITOR
        Debug.Log(this.GetType().Name);
#endif
        //Managers_KSM.Input.OnKeysHeld += ItemUse;
    }
    public void FixedUpdate()
    {
        if (_fireCooltime > 0)
        {
            _fireCooltime -= Time.fixedDeltaTime;
        }
    }
    public override void ItemUse(List<KeyCode> keys)
    {

        if (_fireCooltime <= 0.0f &&_loadedBullet>0&& keys.Contains(KeyCode.Mouse0))
        {
            _fireCooltime = _fireCooltimeSet;
            Fire();
        }
#if UNITY_EDITOR
        if(_fireCooltime<=0.0f&&Input.GetKey(KeyCode.Mouse1))
        {
            _fireCooltime = _fireCooltimeSet;
            Fire();
        }
#endif
        if (keys.Contains(KeyCode.Mouse1))
        {
            Zoom(true);
        }
        else if (_zoomOuted == true) //ÁÜÀÌ ¾Æ´Ï¸é Ãà¼Ò ¾ÈÇÏ°Ô else if·Î
        {
            Zoom(false);
        }
        //        if (keys.Contains(KeyCode.R))
        //        {
        //#if UNITY_EDITOR
        //            Debug.Log("Ã¶ÄÀ2");
        //#endif
        //            if (_loadedBullet<_maxBullet)
        //            {
        //#if UNITY_EDITOR
        //                Debug.Log("Ã¶ÄÀ3");
        //#endif
        //                Reload();
        //            }
        //        }

        if (Input.GetKey(KeyCode.R))
        {
#if UNITY_EDITOR
            Debug.Log("Ã¶ÄÀ2");
#endif
            if (_loadedBullet < _maxBullet)
            {
#if UNITY_EDITOR
                Debug.Log("Ã¶ÄÀ3");
#endif
                Reload();
            }
        }
    }
    public void Fire()
    {
#if UNITY_EDITOR
        Debug.Log("¹ß»ç");
#endif
        _loadedBullet--;
        _bulletsPool.Summon(_bullet).GetComponent<FiredBullet>().FireSet(this.gameObject.transform.parent.position+this.gameObject.transform.parent.forward*_fireLength, transform.parent.forward);//À§Ä¡±âÁØ ³ªÁß¿¡ ÇÏ°í È°¼ºÈ­³ª ±âÅ¸µîµî ºÎºÐ Àú±â¼­ Ãß°¡
    }
    public void Reload()
    {
        BulletItem b=null;
        switch ((int)_bullet)
        {
            case 0:
                b = (BulletItem)GetComponentInParent<Inventory>().CheckItem<GlockAmmo>();
                break;
            case 1:
                b = (BulletItem)GetComponentInParent<Inventory>().CheckItem<RifleAmmo>();
                break;
        }
        
        if (b is null)
        {
#if UNITY_EDITOR
            Debug.Log("Ã¶ÄÀ4");
#endif
            return;
        }
        else
        {
            for (int i=_loadedBullet; i<_maxBullet;i++)
            {
                if (b.Count > 0)
                {
                    b.Re();
                    _loadedBullet++;
                }
                else 
                {
#if UNITY_EDITOR
                    Debug.Log("Ã¶ÄÀ5");
#endif
                    return;
                }
            }
        }
        //ÀÎº¥Åä¸®¿¡¼­ ÃÑ¾Ë ¼Ò¸ðµµ Ãß°¡
    }
    public int GetAmmos { get => _loadedBullet; }
    public void Zoom(bool b)
    {

    }
}
