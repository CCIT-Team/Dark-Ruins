using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_arrow : MonoBehaviour, ITrap
{
    private float force_0 = 500f;

    [SerializeField] private Rigidbody rb;

    public void ActivateTrap()
    {
        ShootArrow();
    }

    public void DeactivateTrap()
    {
        //함정 해제 시 동작 없음
    }

    void ShootArrow()
    {
        Debug.Log("화살 발사!");


        // 기존 속도 초기화 (필수!)
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 화살의 앞 방향(Transform.forward)으로 힘을 줌
        rb.AddForce(transform.forward * force_0);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

}
