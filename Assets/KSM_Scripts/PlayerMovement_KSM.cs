using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement_KSM : MonoBehaviour
{
    [Header("마우스 민감도")]
    [SerializeField] private float mouseSpeed = 5f;

    [Header("플레이어 이동")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f; 

    private float mouseX;
    private PlayerRespawn_KSM respawn;

    private Animator anim;

    bool isFireReady = true;

    Knife_KSM equipWeapon;
    float fireDelay;

    private void Awake()
    {
        equipWeapon = GetComponent<Knife_KSM>();
    }

    void OnEnable()
    {
        Managers_KSM.Input.OnKeysHeld += HandleKeysHeld;
    }

    void OnDisable()
    {
        // 조건문 추가해서 코드 감싸기 (빨간색 쳐내기 뛰발)
        if (Managers_KSM.Instance != null)
        {
            Managers_KSM.Input.OnKeysHeld -= HandleKeysHeld;
        }
    }

    private void HandleKeysHeld(List<KeyCode> heldKeys)
    {
        Vector3 direction = Vector3.zero;
        if (heldKeys.Contains(KeyCode.W)) direction += Vector3.forward;
        if (heldKeys.Contains(KeyCode.S)) direction += Vector3.back;
        if (heldKeys.Contains(KeyCode.A)) direction += Vector3.left;
        if (heldKeys.Contains(KeyCode.D)) direction += Vector3.right;

        if (direction != Vector3.zero)
        {
            Vector3 worldDirection = transform.TransformDirection(direction.normalized);

            transform.position += worldDirection * moveSpeed * Time.deltaTime;

            if(heldKeys.Contains(KeyCode.LeftShift))
            {
                transform.position += worldDirection * runSpeed * Time.deltaTime;
            }
        }


        if (heldKeys.Contains(KeyCode.Mouse0) && isFireReady)
        {
            equipWeapon.Use();
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
    }

    void Update()
    {
        // 마우스 회전
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        // 사망 체크
        if (respawn != null && respawn.isDead)
        {
            return;
        }

        FireDelayCalc();
    }

    void FireDelayCalc()
    {
        if (equipWeapon == null)
        {
            return;
        }
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;
    }
}