using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;

    private Queue<GameObject> _bullet = new Queue<GameObject>(10);
    protected BulletsPool()
    {


    }
    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<FiredBullet>().Initialize(this);
            bullet.SetActive(false);
            _bullet.Enqueue(bullet);
        }
    }
    public GameObject Summon() //이러면 대충 필드에서 어떤 총알을 꺼낼지 지정됨
    {
        GameObject bullet;
        if (_bullet.Count == 0)
        {
            bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<FiredBullet>().Initialize(this);
        }
        else
        {
            bullet = _bullet.Dequeue();
        }
        bullet.SetActive(true);
        return bullet;
    }
    public void Return(GameObject bullet)
    {
        //대충 disable도 해주고 어쩌고 저쩌고
        bullet.SetActive(false);
        _bullet.Enqueue(bullet);
    }
}
