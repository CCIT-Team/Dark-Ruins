using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class FiredBullet : MonoBehaviour
{
    public Vector3 _axis, startPos;
    protected bool _isFire;
    protected BulletsPool _pool;
    protected BulletsPool.Bullets _bullet;
    protected const float _speed = 40.0f, _maxDistance=20.0f;
    protected int _damage;
    public virtual void Initialize(BulletsPool pool)
    {
        _pool = pool;
    }
    public void FireSet(Vector3 position,Vector3 axis)
    {
        //여기다 자리 지정할 것
        _axis = axis;
        if (_axis == default(Vector3))
        {
            _isFire = false;
        }
        else
        {
            _isFire = true;
            //이제 날아가는거 이벤트로 구독 박아버리면 될 예정
            //StartCoroutine(Fired());
            startPos = position;
            this.gameObject.transform.position = startPos;
            this.gameObject.transform.forward = _axis;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Debug.Log(other.transform.name);
#endif
        if (other.isTrigger==true)
        {
            return;
        }
        if(other.TryGetComponent<IDamageable_KSM>(out IDamageable_KSM damageable)==true) //만약 여기서 널레퍼런스 띄우면 김대한에게 문의
        {
            damageable.OnDamaged(_damage, transform.root, other.TryGetComponent<WeakPoint_KSM>(out var _));

        }
        _isFire = false;
    }
    /*IEnumerator Fired()
    {
        this.gameObject.transform.forward = _axis;
        Vector3 startPos = transform.position;
        while (_isFire)
        {
            this.gameObject.transform.position += _axis * _speed * Time.deltaTime;
            if (Vector3.Distance(startPos, this.gameObject.transform.position) > _maxDistance)
            {
                _isFire = false;
            }
            yield return null;
        }
        _pool.Return(this.gameObject);
    }*/
    private void FixedUpdate()
    {
        this.gameObject.transform.position += _axis * _speed * Time.fixedDeltaTime;
        if (_isFire==false||Vector3.Distance(startPos, this.gameObject.transform.position) > _maxDistance)
        {
            _isFire = false;
            _pool.Return(this.gameObject,_bullet);
        }
    }
}
