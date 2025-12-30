using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_KSM : CreatureController_KSM
{
    [Header("설정")]
    [SerializeField] private float mouseSpeed = 5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] private LayerMask interactionLayer;

    [Header("이동 속도")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;

    [Header("애니메이션 연결")]
    public Animation handParentsAnim;
    public string switchAnimName = "SwitchAnimation";
    public string holsterAnimName = "Holster";

    [Header("현재 무기 상태")]
    [SerializeField] private MonoBehaviour currentWeaponContext;
    [SerializeField] private GameObject currentWeaponObject;

    private Rigidbody rb;
    private Vector3 targetVelocity = Vector3.zero;

    private IWeapon_KSM currentWeapon;
    private IEquipment_KSM currentEquipment;
    private FlashlightItem_KSM myFlashlight;

    private float mouseX, mouseY;
    private float fireDelay;
    private bool isSwapping = false;
    private bool isFireReady = true;
    private bool isKnockedBack = false;

    protected override void Awake()
    {
        base.Awake();

        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        myFlashlight = GetComponentInChildren<FlashlightItem_KSM>();
        currentEquipment = GetComponentInChildren<IEquipment_KSM>();
    }

    public void ChangeWeapon(IWeapon_KSM newWeapon)
    {
        currentWeapon = newWeapon;
        isFireReady = true;
        fireDelay = 0;
    }

    public void StartWeaponSwap(MonoBehaviour newWeaponScript, GameObject newWeaponObj, int typeID)
    {
        if (isSwapping) return;
        if (currentWeaponContext == newWeaponScript) return;

        StartCoroutine(Co_WeaponSwap(newWeaponScript, newWeaponObj, typeID));
    }

    IEnumerator Co_WeaponSwap(MonoBehaviour newWeaponScript, GameObject newWeaponObj, int typeID)
    {
        isSwapping = true;
        isFireReady = false;

        if (currentWeaponObject != null)
        {
            if (handParentsAnim != null)
            {
                handParentsAnim.Rewind(holsterAnimName);
                handParentsAnim.Play(holsterAnimName);
            }

            yield return new WaitForSeconds(0.5f);

            currentWeaponObject.SetActive(false);
        }

        currentWeaponContext = newWeaponScript;
        currentWeaponObject = newWeaponObj;

        if (currentWeaponObject != null)
        {
            currentWeaponObject.SetActive(true);

            if (handParentsAnim != null)
            {
                handParentsAnim.Rewind(switchAnimName);
                handParentsAnim.Play(switchAnimName);
            }

            yield return new WaitForSeconds(0.5f);
        }

        isSwapping = false;
        isFireReady = true;
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
        if (Managers_KSM.Input != null)
        {
            Managers_KSM.Input.OnKeysHeld += HandleKeysHeld;
            Managers_KSM.Input.OnKeysPressed += HandleKeysPressed;
        }
    }

    void OnDisable()
    {
        if (Managers_KSM.Instance != null && Managers_KSM.Input != null)
        {
            Managers_KSM.Input.OnKeysHeld -= HandleKeysHeld;
            Managers_KSM.Input.OnKeysPressed -= HandleKeysPressed;
        }
    }

    #region PlayerInputKeys
    private void HandleKeysPressed(KeyCode key)
    {
        if (key == KeyCode.F) UVflash();
        if (key == KeyCode.Mouse0) WeaponAttack();
        if (key == KeyCode.E) TryInteract();
    }

    private void HandleKeysHeld(List<KeyCode> heldKeys)
    {
        float currentSpeed = heldKeys.Contains(KeyCode.LeftShift) ? runSpeed : moveSpeed;

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
        if (!isKnockedBack)
        {
            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        }
    }

    private void MouseLook()
    {
        if (playerCamera == null) return;
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);

        transform.localEulerAngles = new Vector3(0, mouseX, 0);
        playerCamera.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }

    void UVflash()
    {
        if (currentEquipment != null) currentEquipment.Toggle();
    }

    void WeaponAttack()
    {
        if (currentWeapon == null) return;

        if (currentWeapon is MonoBehaviour weaponMono && !weaponMono.gameObject.activeInHierarchy)
            return;

        if (isFireReady)
        {
            currentWeapon.Use();

            if (anim != null) anim.SetTrigger("Attack");

            isFireReady = false;
            fireDelay = 0;
        }
    }

    void TryInteract()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            CandleScript_KSM candle = hit.collider.GetComponentInParent<CandleScript_KSM>();
            if (candle != null)
            {
                candle.Interact();
                return;
            }
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

    public void ApplyKnockback(Vector3 force, float duration)
    {
        StartCoroutine(KnockbackRoutine(force, duration));
    }

    IEnumerator KnockbackRoutine(Vector3 force, float duration)
    {
        isKnockedBack = true;
        rb.AddForce(force, ForceMode.Impulse);

        yield return new WaitForSeconds(duration);

        isKnockedBack = false;
    }
}