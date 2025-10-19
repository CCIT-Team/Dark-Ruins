using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_KSM : MonoBehaviour
{
    public int HealthMax;
    public int currentHealth;

    [Header("마우스 민감도")]
    [SerializeField] private float mouseSpeed = 5f;

    [Header("플레이어 이동")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;

    private Animator anim;
    private Knife_KSM knife;

    private float mouseX;
    private float fireDelay;
    private bool isFireReady = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        knife = GetComponent<Knife_KSM>();
    }

    void Update()
    {
        // 마우스 회전
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        if (!isFireReady)
        {
            fireDelay += Time.deltaTime;
            if (knife != null && knife.rate < fireDelay)
            {
                isFireReady = true;
            }
        }
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
        //Move
        Vector3 direction = Vector3.zero;
        if (heldKeys.Contains(KeyCode.W)) direction += Vector3.forward;
        if (heldKeys.Contains(KeyCode.S)) direction += Vector3.back;
        if (heldKeys.Contains(KeyCode.A)) direction += Vector3.left;
        if (heldKeys.Contains(KeyCode.D)) direction += Vector3.right;
        if (direction != Vector3.zero)
        {
            Vector3 worldDirection = transform.TransformDirection(direction.normalized);
            float currentSpeed = moveSpeed;
            if (heldKeys.Contains(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            }
            transform.position += worldDirection * currentSpeed * Time.deltaTime;
        }

        //Attack
        if (heldKeys.Contains(KeyCode.Mouse0))
        {
            KnifeAttack();
        }
    }

    void KnifeAttack()
    {
        if (isFireReady && knife != null)
        {
            knife.Use();
            if (anim != null)
            {
                anim.SetTrigger("Attack");
            }

            isFireReady = false;
            fireDelay = 0;
        }
    }
}