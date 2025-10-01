using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement_KSM : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;          // 이동 속도
    //[SerializeField] private float runSpeed = 12f;          // 달리기 조정 변수
    [SerializeField] private float rotationSpeed = 10f;     // 회전 속도

    void OnEnable()
    {
        InputManager_KSM.OnKeysHeld += HandleKeysHeld;
    }

    void OnDisable()
    {
        
    }

    private void HandleKeysHeld(List<KeyCode> heldKeys)
    {
        Vector3 moveDirection = Vector3.zero;

        if (heldKeys.Contains(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        }
        if (heldKeys.Contains(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }
        if (heldKeys.Contains(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        }
        if (heldKeys.Contains(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }

        if (moveDirection != Vector3.zero)
        {
            Vector3 normalizedDirection = moveDirection.normalized;

            transform.position += normalizedDirection * moveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(normalizedDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // 리스폰 컴포넌트
    private PlayerRespawn_KSM respawn;

    // 현재 이동속도
    private float currentSpeed;

    //// 점프
    //[SerializeField] private float jumpForce = 30f;
    //private bool isGrounded;

    private Rigidbody rb;

    void Update()
    {
        //if (respawn != null && respawn.isDead)
        //    return;

        //Vector3 moveDirection = Vector3.zero;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        //    transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        //    transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        //    transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        //    transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        //}

        //if (moveDirection != Vector3.zero)
        //{
        //    Vector3 normalizedDirection = moveDirection.normalized;

        //    transform.position += normalizedDirection * moveSpeed * Time.deltaTime;

        //    Quaternion targetRotation = Quaternion.LookRotation(normalizedDirection);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //}
    }
}