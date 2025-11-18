using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_KSM : CreatureController_KSM
{
    //public static event Action<Transform> OnPlayerFired;

    [Header("마우스 민감도")]
    [SerializeField] private float mouseSpeed = 5f;
    [SerializeField] private Camera playerCamera;

    [Header("이동 속도")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;

    private Rigidbody rb;
    private Vector3 targetVelocity = Vector3.zero;

    private IWeapon_KSM currentWeapon;
    private IEquipment_KSM currentEquipment;
    private FlashlightItem_KSM myFlashlight;

    private float mouseX;
    private float mouseY;
    private float fireDelay;
    private bool isFireReady = true;

    protected override void Awake()
    {
        base.Awake();

        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();

        myFlashlight = GetComponentInChildren<FlashlightItem_KSM>();
        currentWeapon = GetComponentInChildren<IWeapon_KSM>();
        currentEquipment = GetComponentInChildren<IEquipment_KSM>();
    }

    void Update()
    {
        MouseLook();

        if (!isFireReady)
        {
            fireDelay += Time.deltaTime;

            if (currentWeapon != null && currentWeapon.rate < fireDelay)
            {
                isFireReady = true;
            }
        }
    }

    void OnEnable()
    {
        Managers_KSM.Input.OnKeysHeld += HandleKeysHeld;
        Managers_KSM.Input.OnKeysPressed += HandleKeysPressed;
    }

    void OnDisable()
    {
        if (Managers_KSM.Instance != null)
        {
            Managers_KSM.Input.OnKeysHeld -= HandleKeysHeld;
            Managers_KSM.Input.OnKeysPressed -= HandleKeysPressed;
        }
    }

    #region PlayerInputKeys
    private void HandleKeysPressed(KeyCode key)
    {
        if (key == KeyCode.F)
        {
            UVflash();
        }

        if (key == KeyCode.Mouse0)
        {
            WeaponAttack();
        }
    }

    private void HandleKeysHeld(List<KeyCode> heldKeys)
    {
        float currentSpeed = moveSpeed;
        if (heldKeys.Contains(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        Vector3 direction = Vector3.zero;
        if (heldKeys.Contains(KeyCode.W)) direction += Vector3.forward;
        if (heldKeys.Contains(KeyCode.S)) direction += Vector3.back;
        if (heldKeys.Contains(KeyCode.A)) direction += Vector3.left;
        if (heldKeys.Contains(KeyCode.D)) direction += Vector3.right;
        if (direction != Vector3.zero)
        {
            Vector3 worldDirection = transform.TransformDirection(direction.normalized);
            targetVelocity = worldDirection * currentSpeed;
        }
        else
        {
            targetVelocity = Vector3.zero;
        }
    }
    #endregion

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
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
    void UVflash()
    {
        if (currentEquipment != null)
        {
            currentEquipment.Toggle();
        }
    }

    void WeaponAttack()
    {
        if (isFireReady && currentWeapon != null)
        {
            currentWeapon.Use();
            anim.SetTrigger("Attack");

            isFireReady = false;
            fireDelay = 0;
        }
    }

    public override void OnDamaged(int damage, Transform attacker, bool isWeakPoint)
    {
        base.OnDamaged(damage, attacker, false);
    }

    public override void OnDead()
    {
        Debug.Log("유다희");
    }
}