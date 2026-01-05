using UnityEngine;

public class WeaponManager_KSM : MonoBehaviour
{
    public enum WeaponMode { None, Rifle, Gun, Knife }

    [Header("현재 상태")]
    public WeaponMode currentMode = WeaponMode.None;

    [Header("스크립트 연결")]
    public GunBase rifleScript;
    public GunBase gunScript;
    public Knife3_KSM knifeScript;

    [Header("비주얼 오브젝트")]
    public GameObject rifleObject;
    public GameObject gunObject;
    public GameObject knifeObject;

    [Header("애니메이션 컴포넌트")]
    public Animator rifleAnim;
    public Animator gunAnim;
    public Animator knifeAnim;

    [Header("애니메이션 클립 이름")]
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

        if (rifleObject) rifleObject.SetActive(false);
        if (gunObject) gunObject.SetActive(false);
        if (knifeObject) knifeObject.SetActive(false);

        SetWeaponMode(WeaponMode.None);
    }

    void Update()
    {
        if (currentMode == WeaponMode.Knife && knifeScript != null && knifeScript.isAttacking)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeaponMode(WeaponMode.Rifle);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeaponMode(WeaponMode.Gun);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeaponMode(WeaponMode.Knife);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetWeaponMode(WeaponMode.None);
    }

    void SetWeaponMode(WeaponMode mode)
    {
        if (currentMode == mode) return;
        currentMode = mode;

        MonoBehaviour nextWeaponScript = null;
        GameObject nextWeaponVisual = null;
        int typeID = 0;
        string equipSoundKey = "";

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
                nextWeaponScript = null;
                nextWeaponVisual = null;
                typeID = 0;
                break;
        }

        if (playerController != null)
        {
            playerController.StartWeaponSwap(nextWeaponScript, nextWeaponVisual, typeID);
        }

        if (!string.IsNullOrEmpty(equipSoundKey))
        {
            if (Managers_YGU.Sound != null)
                Managers_YGU.Sound.Play(equipSoundKey, Sound.UI);
        }
    }

    public void PlayAttackAnimation()
    {
        switch (currentMode)
        {
            case WeaponMode.Rifle:
                if (rifleAnim != null) rifleAnim.Play(rifleFireClip, -1, 0f);
                break;
            case WeaponMode.Gun:
                if (gunAnim != null) gunAnim.Play(gunFireClip, -1, 0f);
                break;
            case WeaponMode.Knife:
                if (knifeAnim != null)
                {
                    knifeAnim.SetTrigger("slash");
                    Managers_YGU.Sound.Play("Machete_Swing", Sound.UI);
                }
                break;
        }
    }

    public void PlayReloadAnimation()
    {
        switch (currentMode)
        {
            case WeaponMode.Rifle:
                if (rifleAnim != null) rifleAnim.Play(rifleReloadClip, -1, 0f);
                break;
            case WeaponMode.Gun:
                if (gunAnim != null) gunAnim.Play(gunReloadClip, -1, 0f);
                break;
        }
    }
}