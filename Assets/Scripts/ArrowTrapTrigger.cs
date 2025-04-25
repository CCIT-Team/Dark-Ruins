using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform shootPoint;        // 화살 발사 위치
    public Vector3 shootDirection = Vector3.forward; // 발사 방향
    public float arrowSpeed = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Quaternion rot = Quaternion.Euler(0, 0, -90);
        GameObject arrow = Instantiate(ArrowPrefab, shootPoint.position, rot);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        rb.velocity = shootDirection.normalized * arrowSpeed;
    }
}
