using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : ItemBase
{
    [SerializeField]
    private int _loadedBullet = 0, _maxBullet=6;
    private float _fireCooltime = 0f;
    private float _fireCooltimeSet=0.5f;
    private bool _zoomOuted = true;
    protected BulletsPool _bulletsPool;
    [SerializeField]
    private readonly Vector3 _up = new Vector3(0, 1.0f, 0);
    private void Awake()
    {
        Length = 2; //³ªÁß¿¡ ±âÈ¹ ³ª¿À¸é Àû¿ëÇÏ¸é µÊ ¤·¤·
        _bulletsPool =FindObjectOfType<BulletsPool>().GetComponent<BulletsPool>();
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
        _loadedBullet--;
        _bulletsPool.Summon().GetComponent<FiredBullet>().FireSet(this.gameObject.transform.position+this.gameObject.transform.forward*1.5f,this.gameObject.transform.forward);//À§Ä¡±âÁØ ³ªÁß¿¡ ÇÏ°í È°¼ºÈ­³ª ±âÅ¸µîµî ºÎºÐ Àú±â¼­ Ãß°¡
    }
    public void Reload()
    {
        BulletItem b= (BulletItem)GetComponentInParent<Inventory>()?.CheckItem<BulletItem>();
        if ( b is null)
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
    public void Zoom(bool b)
    {

    }
}
