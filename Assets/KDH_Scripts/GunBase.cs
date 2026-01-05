using DG.Tweening;
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
    private ParticleSystem _particleSystem;
    public float rate { get =>_fireCooltimeSet; }
    public void Use()
    {
        ItemUse(default(List<KeyCode>));
    }
    protected override void Start()
    {
        base.Start();
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
        if (_fireCooltime > 0)
        {
            _fireCooltime -= Time.fixedDeltaTime;
        }
    }
    public override void ItemUse(List<KeyCode> keys)
    {

        if (_fireCooltime <= 0.0f &&_loadedBullet>0&& Input.GetKey(KeyCode.Mouse0))
        {
            _fireCooltime = _fireCooltimeSet;
            Fire();
        }
#if UNITY_EDITOR
        if(_fireCooltime<=0.0f&&Input.GetKey(KeyCode.Mouse1))
        {
            _fireCooltime = _fireCooltimeSet;
            _loadedBullet++;
            Fire();
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
            if (_loadedBullet < _maxBullet)
            {
#if UNITY_EDITOR
                Debug.Log("철컥3");
#endif
                Reload(out _);
            }
        }
    }
    public virtual void Fire()
    {
#if UNITY_EDITOR
        Debug.Log("발사");
#endif
        _loadedBullet--;
        _bulletsPool.Summon(_bullet).GetComponent<FiredBullet>().FireSet(Camera.main.transform.position+ Camera.main.transform.forward *_fireLength, Camera.main.transform.forward);//위치기준 나중에 하고 활성화나 기타등등 부분 저기서 추가
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particleSystem.Simulate(0f, true, true);
        _particleSystem.Play();
        _uir.BulletUISet(true, _loadedBullet, _maxBullet);
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
            return;
        }
        else
        {
            for (int i=_loadedBullet; i<_maxBullet;i++)
            {
                if (b.Count > 0)
                {
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
                _uir.BulletUISet(true, _loadedBullet, _maxBullet);
            }
        }
        //인벤토리에서 총알 소모도 추가
    }
    public int GetAmmos { get => _loadedBullet; }

    protected Camera _camera;
    public void Zoom(bool b)
    {
#if UNITY_EDITOR
        Debug.Log(_camera.transform.localPosition);
#endif
        if (b==false)
        {
            if (_camera.fieldOfView >= 60)
            {
                _camera.fieldOfView = 60;
                _zoomOuted = false;
                return;
            }

            _camera.fieldOfView += 1;
            return;
        }
        else if(_camera.fieldOfView >= 20&&_zoomOuted==false)
        {
            _camera.fieldOfView -= 1;
        }
        else
        {
            _zoomOuted = true;
            _camera.fieldOfView = 20;
        }

    }
    [SerializeField]
    private UI_GameScene _uir;
    public override void OnPickUp()
    {
        _uir.BulletUISet(true,_loadedBullet,_maxBullet);
    }

    public override void OnDropped()
    {
        _uir.BulletUISet(false);
    }
}
