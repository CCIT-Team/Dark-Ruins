using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : ItemBase
{
    private int _loadedBullet = 0;
    private float _fireCooltime = 0f;
    private float _fireCooltimeSet=0.5f;
    private bool _zoomOuted = true;
    protected BulletsPool _bulletsPool;
    [SerializeField]
    private readonly Vector3 _up = new Vector3(0, 1.0f, 0);
    private void Awake()
    {
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

        if (_fireCooltime <= 0.0f && keys.Contains(KeyCode.Mouse0))
        {
            _fireCooltime = _fireCooltimeSet;
            Fire();
        }
        if (keys.Contains(KeyCode.Mouse1))
        {
            Zoom(true);
        }
        else if (_zoomOuted == true) //줌이 아니면 축소 안하게 else if로
        {
            Zoom(false);
        }

    }
    public void Fire()
    {
        _bulletsPool.Summon().GetComponent<FiredBullet>().FireSet(this.gameObject.transform.position+this.gameObject.transform.forward*1.5f,this.gameObject.transform.forward);//위치기준 나중에 하고 활성화나 기타등등 부분 저기서 추가
    }
    public void Reload()
    {
        //_loadedBullet +=;
        //인벤토리에서 총알 소모도 추가
    }
    public void Zoom(bool b)
    {

    }
}
