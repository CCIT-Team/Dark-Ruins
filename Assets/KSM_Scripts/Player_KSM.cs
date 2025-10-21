using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_KSM : MonoBehaviour
{
    [Header("마우스 민감도")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSpeed = 5f;

    [Header("플레이어 이동")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;

    public int HealthMax;
    public int currentHealth;

    private Animator anim;
    private Knife_KSM knife;

    private float mouseX;
    private float mouseY;
    private float fireDelay;
    private bool isFireReady = true;

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        anim = GetComponent<Animator>();
        knife = GetComponent<Knife_KSM>();
    }

    void Update()
    {
        MouseLook();

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

        if (heldKeys.Contains(KeyCode.Mouse0))
        {
            KnifeAttack();
        }
    }
    private void MouseLook()
    {
        if (playerCamera == null) return;

        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        playerCamera.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
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