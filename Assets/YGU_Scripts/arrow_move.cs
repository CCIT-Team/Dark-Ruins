using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_move : MonoBehaviour
{
    [SerializeField] private float force_0 = 600f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ShootArrow()
    {
        Debug.Log("화살 발사!");

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(transform.up * force_0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
