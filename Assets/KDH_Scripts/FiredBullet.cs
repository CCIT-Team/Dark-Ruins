using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FiredBullet : ItemBase
{
    public Vector3 _axis, startPos;
    private bool _isFire;
    private BulletsPool _pool;
    private const float _speed = 10.0f, _maxDistance=20.0f;
    public void Initialize(BulletsPool pool)
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
    public override void ItemUse(List<KeyCode> keys = default(List<KeyCode>)) //일단... 얘는 키입력 안받긴 한데...
    {

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Destroy(collider.gameObject);
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
            _pool.Return(this.gameObject);
        }
    }
}
