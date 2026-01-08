using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : ItemBase,IWeapon_KSM
{
    [SerializeField]
    protected int _loadedBullet=0, _maxBullet=8, _haveBullet=0,v2=0;
    protected float _fireCooltime=0,_reloadCooltime=0;
    protected float _fireCooltimeSet=0.5f,_reloadCooltimeSet=2f;
    protected bool _zoomOuted = true, _zoomIng = false, e = false,v=false;
    
    protected float _fireLength;
    protected BulletsPool _bulletsPool;

    protected BulletsPool.Bullets _bullet;

    protected Vector3 _up = new Vector3(0, 0, 0);
    protected Vector3 _f=new Vector3(0, 0,0);
    private ParticleSystem _particleSystem;
    public float rate { get =>_fireCooltimeSet; }
    public void Use()
    {
        ItemUse(default(List<KeyCode>));
    }
    protected override void Start()
    {
        base.Start();
        _reloadCooltimeSet = 2f;
        Length = 2; //나중에 기획 나오면 적용하면 됨 ㅇㅇ
        _bulletsPool =FindObjectOfType<BulletsPool>().GetComponent<BulletsPool>();
        _bullet = (BulletsPool.Bullets)Enum.Parse(typeof(BulletsPool.Bullets), this.GetType().Name);
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _camera = Camera.main;
        _uir = GameObject.Find("@UI_Root").GetComponent<UI_GameScene>();
#if UNITY_EDITOR
        Debug.Log(this.GetType().Name);
        Debug.Log(_camera.transform.name);
#endif
        //Managers_KSM.Input.OnKeysHeld += ItemUse;
    }
    public void FixedUpdate()
    {
        if(e==false)
        {
            return;
        }
        if (_fireCooltime > 0)
        {
            _fireCooltime -= Time.fixedDeltaTime;
        }
        if(_reloadCooltime>0)
        {
            _reloadCooltime-=Time.fixedDeltaTime;
        }
        if (_zoomIng == false)
        {
            if (_camera.fieldOfView >= 60)
            {
                _camera.fieldOfView = 60;
                _zoomOuted = false;
                return;
            }

            _camera.fieldOfView += 2;
            return;
        }
        else if (_camera.fieldOfView >= 20 && _zoomOuted == false)
        {
            _camera.fieldOfView -= 2;
        }
        else
        {
            _zoomOuted = true;
            _camera.fieldOfView = 20;
        }
        if (v==false&&v2 > 0)
        {

            v2--;
        }
        e = false;
    }
    public override void ItemUse(List<KeyCode> keys)
    {
        e = true;
        if (Input.GetKey(KeyCode.Mouse0)&&_fireCooltime <= 0.0f && _reloadCooltime <= 0.0f && _loadedBullet>0)
        {
#if UNITY_EDITOR
            Debug.Log(_reloadCooltimeSet);
#endif
            _fireCooltime = _fireCooltimeSet;
            Fire();
        }
        else
        {
            v = false;
        }
#if UNITY_EDITOR
        if (_fireCooltime <= 0.0f && _reloadCooltime <= 0.0f && Input.GetKey(KeyCode.Mouse1))
        {
            _fireCooltime = _fireCooltimeSet;
            _loadedBullet = _maxBullet;
            //Fire();
        }
#endif
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Zoom(true);
        }
        else if (_zoomOuted == true) //줌이 아니면 축소 안하게 else if로
        {
            Zoom(false);
        }

        if (Input.GetKey(KeyCode.R))
        {
#if UNITY_EDITOR
            Debug.Log("철컥2");
#endif
            if (_loadedBullet < _maxBullet&&_reloadCooltime<=0.0f)
            {
#if UNITY_EDITOR
                Debug.Log("철컥3");
#endif
                _fireCooltime = _reloadCooltime;
                _reloadCooltime = _reloadCooltimeSet;
                Reload(out _);
            }
        }

    }
    public virtual void Fire()
    {
#if UNITY_EDITOR
        Debug.Log("발사");
#endif
        v = true;
        v2 ++;
        _loadedBullet--;
        _bulletsPool.Summon(_bullet).GetComponent<FiredBullet>().FireSet(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _fireLength)), Camera.main.transform.forward);//위치기준 나중에 하고 활성화나 기타등등 부분 저기서 추가
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particleSystem.Simulate(0f, true, true);
        _particleSystem.Play();
        //Managers_YGU.Sound.Play("", Sound.Effect);

        _uir.BulletUISet(true, _loadedBullet, HaveBullet());
    }
    public int HaveBullet()
    {
        int q = 0;
        switch ((int)_bullet)
        {
            case 0:
                List<ItemBase> lib = GetComponentInParent<Inventory>().CheckItems<GlockAmmo>();
                foreach (ItemBase g in lib)
                {
                    q += g.Count;
#if UNITY_EDITOR
                    Debug.Log(g);
#endif
                }
                break;
            case 1:
                List<ItemBase> LIB = GetComponentInParent<Inventory>().CheckItems<RifleAmmo>();
                foreach (ItemBase gg in LIB)
                {
                    q += gg.Count;
#if UNITY_EDITOR
                    Debug.Log(gg);
#endif
                }
                break;
        }

        return q;
    }
    public virtual void Reload(out bool ob)
    {
        BulletItem b=null;
        ob = false;
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
            Debug.Log("철컥4");
#endif
            Managers_YGU.Sound.Play("Gun_Empty", Sound.UI);
            return;
        }
        else
        {
            for (int i=_loadedBullet; i<_maxBullet;i++)
            {
                if (b.Count > 0&&_loadedBullet<_maxBullet)
                {
#if UNITY_EDITOR
                    Debug.Log(HaveBullet());
#endif
                    b.Re();
                    _uir.Delay();
                    _loadedBullet++;
                    ob = true;
                }
                else 
                {
#if UNITY_EDITOR
                    Debug.Log("철컥5");
#endif
                    return;
                }
                _uir.BulletUISet(true, _loadedBullet, HaveBullet());
            }
        }
        //인벤토리에서 총알 소모도 추가
    }
    public int GetAmmos { get => _loadedBullet; }

    [SerializeField]
    protected Camera _camera;
    public void Zoom(bool b)
    {
#if UNITY_EDITOR
        Debug.Log(_camera.transform.localPosition);
#endif
        _zoomIng = b;

    }
    [SerializeField]
    private UI_GameScene _uir;
    public override void OnPickUp()
    {
#if UNITY_EDITOR
        Debug.Log("작동테스트");
        Debug.Log(_uir.transform.name);
#endif
        _uir.BulletUISet(true,_loadedBullet,HaveBullet());
    }

    public override void OnDropped()
    {
        _uir.BulletUISet(false);
    }
    private void OnDisable()
    {
        _camera.fieldOfView = 60;
    }
}
