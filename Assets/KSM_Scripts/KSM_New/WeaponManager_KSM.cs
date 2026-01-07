using UnityEngine;

public class WeaponManager_KSM : MonoBehaviour
{
    public enum WeaponMode { None, Rifle, Gun, Knife }

    [Header("현재 상태")]
    public WeaponMode currentMode = WeaponMode.None;

    [Header("자동 감지 설정")]
    [SerializeField] private string detectName = "소총";

    [Header("소지 여부")]
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

    void Start()
    {
        if (playerController == null)
            playerController = GetComponentInParent<PlayerController_KSM>();

        FindRifleGlobal();

        if (rifleObject != null) rifleObject.SetActive(false);
        if (gunObject != null) gunObject.SetActive(false);
        if (knifeObject != null) knifeObject.SetActive(false);

        SetWeaponMode(WeaponMode.None);
    }

    void Update()
    {
        if (hasRifle == false)
        {
            FindRifleGlobal();
        }

        if (currentMode == WeaponMode.Knife && knifeScript != null && knifeScript.isAttacking)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hasRifle) SetWeaponMode(WeaponMode.Rifle);
        }

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

    void FindRifleGlobal()
    {
        GunBase[] allGuns = transform.root.GetComponentsInChildren<GunBase>(true);

        foreach (var gun in allGuns)
        {
            if (gun.name.Contains(detectName))
            {
                hasRifle = true;
                return;
            }
        }
    }

    public void AcquireRifle() { hasRifle = true; }
    public void AcquireGun() { hasGun = true; }
    public void AcquireKnife() { hasKnife = true; }

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