using LYS_Work.Manager;
using LYS_Work.Token;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_KSM : CreatureController_KSM
{
    [Header("참조")]
    public WeaponManager_KSM weaponManager;

    [Header("설정")]
    [SerializeField] private float mouseSpeed = 5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] private LayerMask interactionLayer;

    [Header("이동 속도")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;

    [Header("현재 무기 상태")]
    [SerializeField] private MonoBehaviour currentWeaponContext;
    [SerializeField] private GameObject currentWeaponObject;

    private Rigidbody rb;
    private Vector3 targetVelocity = Vector3.zero;

    private IWeapon_KSM currentWeapon;
    private IEquipment_KSM currentEquipment;
    private FlashlightItem_KSM myFlashlight;

    private float mouseX, mouseY;
    private bool isSwapping = false;
    private bool isFireReady = true;
    private bool isKnockedBack = false;
    private int currentWeaponTypeID = 0;

    private Coroutine swapCoroutine;

    protected override void Awake()
    {
        base.Awake();

        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
        if (weaponManager == null) weaponManager = GetComponentInChildren<WeaponManager_KSM>();
        myFlashlight = GetComponentInChildren<FlashlightItem_KSM>();
        currentEquipment = GetComponentInChildren<IEquipment_KSM>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }

    public void StartWeaponSwap(MonoBehaviour newWeaponScript, GameObject newWeaponObj, int typeID)
    {
        if (currentWeaponTypeID == typeID) return;

        if (swapCoroutine != null) StopCoroutine(swapCoroutine);

        swapCoroutine = StartCoroutine(Co_WeaponSwap(newWeaponScript, newWeaponObj, typeID));
    }

    IEnumerator Co_WeaponSwap(MonoBehaviour newWeaponScript, GameObject newWeaponObj, int typeID)
    {
        isSwapping = true;
        isFireReady = false;

        if (currentWeaponObject != null)
        {
            currentWeaponObject.SetActive(false);
        }

        currentWeaponContext = newWeaponScript;
        currentWeaponObject = newWeaponObj;
        currentWeaponTypeID = typeID;

        if (currentWeaponObject != null)
        {
            currentWeaponObject.SetActive(true);
            yield return null;
        }

        isSwapping = false;
        isFireReady = true;
        swapCoroutine = null;
    }

    void Update()
    {
        MouseLook();
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        }
    }

    void WeaponAttack()
    {
        if (!isFireReady || isSwapping || currentWeaponContext == null) return;
        if (currentWeaponObject != null && !currentWeaponObject.activeInHierarchy) return;

        if (weaponManager != null)
        {
            bool canPlayAnim = true;
            if (currentWeaponContext is Knife3_KSM knife)
            {
                if (knife.isAttacking) canPlayAnim = false;
            }

            if (canPlayAnim) weaponManager.PlayAttackAnimation();
        }

        if (currentWeaponContext is IWeapon_KSM weapon)
        {
            weapon.Use();
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


    #region 퍼즐
    private Token pToken = null;
    [SerializeField]
    private Camera playerCam;
    private void DoPuzzle()
    {
        Vector3 rayOrigin = playerCam.transform.position;
        Vector3 rayDirection = playerCam.transform.forward;
        RaycastHit hit;
        RotatablePuzzleManager pmgr;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, float.PositiveInfinity) == false)
        {
            return;
        }

        pmgr = hit.collider.GetComponentInParent<RotatablePuzzleManager>();
        if (pmgr == null)
        {
            return;
        }

        if (pToken == null)
        {
            pmgr.DoPuzzle(null, 2, ref pToken);
        }
        else
        {
            pmgr.EndPuzzle(pToken);
            pToken = null;
        }
    }
    #endregion


    #region PlayerInputKeys
    private void HandleKeysPressed(KeyCode key)
    {
        if (key == KeyCode.F) UVflash();
        if (key == KeyCode.Mouse0) WeaponAttack();
        if (key == KeyCode.E) TryInteract();
        if (key == KeyCode.I) DoPuzzle();
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