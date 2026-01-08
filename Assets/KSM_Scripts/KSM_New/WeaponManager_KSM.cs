using UnityEngine;
using System.Collections.Generic; // 리스트 사용을 위해 필요

public class WeaponManager_KSM : MonoBehaviour
{
    public enum WeaponMode { None, Rifle, Gun, Knife }

    [Header("현재 상태")]
    public WeaponMode currentMode = WeaponMode.None;

    [Header("설정")]
    [SerializeField] private string rifleVisualName = "소총"; // 찾을 아이템 이름

    [Header("소지 여부 (인벤토리 자동 체크)")]
    public bool hasRifle = false;
    public bool hasGun = true;
    public bool hasKnife = true;

    [Header("오브젝트 연결")]
    public GameObject rifleObject;
    public GameObject gunObject;
    public GameObject knifeObject;

    [Header("스크립트 연결")]
    public GunBase rifleScript;
    public GunBase gunScript;
    public Knife3_KSM knifeScript;

    [Header("애니메이션")]
    public Animator rifleAnim;
    public Animator gunAnim;
    public Animator knifeAnim;

    [Header("애니메이션 클립")]
    public string rifleFireClip = "Firing";
    public string rifleReloadClip = "Reloading";
    public string gunFireClip = "Firing";
    public string gunReloadClip = "Reloading";
    public string knifeAttackClip = "slash";

    public PlayerController_KSM playerController;

    // [추가] CheckItem 함수를 쓰기 위해 인벤토리 스크립트 참조 필요
    private Inventory inventory;

    void Start()
    {
        if (playerController == null)
            playerController = GetComponentInParent<PlayerController_KSM>();

        // [추가] 씬에 있는 Inventory 스크립트를 찾아옵니다.
        inventory = FindObjectOfType<Inventory>();

        ConnectRifleVisuals();

        if (rifleObject != null) rifleObject.SetActive(false);
        if (gunObject != null) gunObject.SetActive(false);
        if (knifeObject != null) knifeObject.SetActive(false);

        SetWeaponMode(WeaponMode.None);
    }

    void Update()
    {
        // 인벤토리 체크
        CheckInventoryForRifle();

        if (currentMode == WeaponMode.Knife && knifeScript != null && knifeScript.isAttacking)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            if (hasRifle) SetWeaponMode(WeaponMode.Rifle);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            if (hasGun) SetWeaponMode(WeaponMode.Gun);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            if (hasKnife) SetWeaponMode(WeaponMode.Knife);

        if (Input.GetKeyDown(KeyCode.Alpha4)) SetWeaponMode(WeaponMode.None);
    }

    void LateUpdate()
    {
        if (currentMode != WeaponMode.Rifle && rifleObject != null && rifleObject.activeSelf)
            rifleObject.SetActive(false);

        if (currentMode != WeaponMode.Gun && gunObject != null && gunObject.activeSelf)
            gunObject.SetActive(false);

        if (currentMode != WeaponMode.Knife && knifeObject != null && knifeObject.activeSelf)
            knifeObject.SetActive(false);
    }

    // [변경된 핵심 함수] Inventory의 CheckItems<T>를 활용
    void CheckInventoryForRifle()
    {
        if (inventory == null) return;

        // 1. 인벤토리에서 GunBase 타입을 가진 모든 아이템을 가져옵니다.
        // (단수형 CheckItem<T>는 첫번째만 반환하므로, 권총이 앞에 있으면 소총을 못 찾을 수 있어 복수형 CheckItems를 씁니다)
        List<ItemBase> guns = inventory.CheckItems<GunBase>();

        bool found = false;

        // 2. 가져온 총기 목록 중에 내가 찾는 이름(rifleVisualName)이 있는지 확인
        foreach (var gun in guns)
        {
            if (gun is Rifle) 
            {
                found = true;
                break;
            }
        }

        // 3. 결과 반영
        hasRifle = found;

        // 4. 소총을 들고 있다가 버렸으면 맨손으로 전환
        if (!hasRifle && currentMode == WeaponMode.Rifle)
        {
            SetWeaponMode(WeaponMode.None);
        }
    }

    void ConnectRifleVisuals()
    {
        GunBase[] allGuns = transform.root.GetComponentsInChildren<GunBase>(true);

        foreach (var gun in allGuns)
        {
            if (gun.name.Contains(rifleVisualName))
            {
                rifleObject = gun.gameObject;
                rifleScript = gun;
                rifleAnim = gun.GetComponent<Animator>();
                return;
            }
        }
    }

    void SetWeaponMode(WeaponMode mode)
    {
        if (currentMode == mode) return;
        currentMode = mode;

        MonoBehaviour nextWeaponScript = null;
        GameObject nextWeaponVisual = null;
        int typeID = 0;
        string equipSoundKey = "";

        if (rifleObject) rifleObject.SetActive(false);
        if (gunObject) gunObject.SetActive(false);
        if (knifeObject) knifeObject.SetActive(false);

        switch (mode)
        {
            case WeaponMode.Rifle:
                nextWeaponScript = rifleScript;
                nextWeaponVisual = rifleObject;
                typeID = 1;
                break;
            case WeaponMode.Gun:
                nextWeaponScript = gunScript;
                nextWeaponVisual = gunObject;
                typeID = 2;
                break;
            case WeaponMode.Knife:
                nextWeaponScript = knifeScript;
                nextWeaponVisual = knifeObject;
                typeID = 3;
                equipSoundKey = "Machete_Equip";
                break;
            case WeaponMode.None:
                typeID = 0;
                break;
        }

        if (nextWeaponVisual != null) nextWeaponVisual.SetActive(true);

        if (playerController != null)
            playerController.StartWeaponSwap(nextWeaponScript, nextWeaponVisual, typeID);

        if (!string.IsNullOrEmpty(equipSoundKey) && Managers_YGU.Sound != null)
            Managers_YGU.Sound.Play(equipSoundKey, Sound.UI);
    }

    public void PlayAttackAnimation()
    {
        switch (currentMode)
        {
            case WeaponMode.Rifle: if (rifleAnim) rifleAnim.Play(rifleFireClip, -1, 0f); break;
            case WeaponMode.Gun: if (gunAnim) gunAnim.Play(gunFireClip, -1, 0f); break;
            case WeaponMode.Knife: if (knifeAnim) { knifeAnim.SetTrigger("slash"); Managers_YGU.Sound.Play("Machete_Swing", Sound.UI); } break;
        }
    }
    public void PlayReloadAnimation()
    {
        switch (currentMode)
        {
            case WeaponMode.Rifle: if (rifleAnim) rifleAnim.Play(rifleReloadClip, -1, 0f); break;
            case WeaponMode.Gun: if (gunAnim) gunAnim.Play(gunReloadClip, -1, 0f); break;
        }
    }
}