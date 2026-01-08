using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_move : MonoBehaviour
{
    [SerializeField] private float force = 600f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ShootArrow()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(transform.up * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            if (this.gameObject.name == "ArrowDamage")
            {
                Destroy(this.gameObject);
            }
            rb.velocity = Vector3.zero;
        }
    }
}
