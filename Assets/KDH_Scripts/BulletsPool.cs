using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;

    private Dictionary<Bullets, Queue<GameObject>> _bullet = new Dictionary<Bullets, Queue<GameObject>>();

    public enum Bullets
    {
        Glock=0,
        Rifle=1
    }
    protected BulletsPool()
    {


    }
    private void Awake()
    {
        _bullet[Bullets.Glock] = new Queue<GameObject>(3);
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<FiredBullet>().Initialize(this);
            bullet.SetActive(false);
            _bullet[Bullets.Glock].Enqueue(bullet);
        }
        _bullet[Bullets.Rifle] = new Queue<GameObject>(5);
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet2 = Instantiate(bulletPrefab2);
            bullet2.GetComponent<FiredBullet>().Initialize(this);
            bullet2.SetActive(false);
            _bullet[Bullets.Rifle].Enqueue(bullet2);
        }
    }
    public GameObject Summon(Bullets i=Bullets.Glock) //이러면 대충 필드에서 어떤 총알을 꺼낼지 지정됨
    {
        GameObject bullet=default(GameObject);

        if (_bullet[i].Count == 0)
        {
            switch(i)
            {
                case Bullets.Glock:
                    bullet = Instantiate(bulletPrefab);
                    break;
                case Bullets.Rifle:
                    bullet = Instantiate(bulletPrefab2);
                    break;
            }

            bullet.GetComponent<FiredBullet>().Initialize(this);
        }
        else
        {
            bullet= _bullet[i].Dequeue();
        }
        bullet.SetActive(true);
        return bullet;
    }
    public void Return(GameObject bullet, Bullets i = Bullets.Glock)
    {
        //대충 disable도 해주고 어쩌고 저쩌고
        bullet.SetActive(false);
        _bullet[i].Enqueue(bullet);
    }
}
