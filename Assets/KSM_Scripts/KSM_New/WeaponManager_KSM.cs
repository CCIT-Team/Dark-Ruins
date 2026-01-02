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
    public Animation rifleAnim;
    public Animation gunAnim;
    public Animation knifeAnim;

    [Header("애니메이션 클립 이름")]
    public string switchAnimClip = "SwitchAnimation-1";
    public string rifleFireClip = "Firing";
    public string rifleReloadClip = "Reloading";
    public string gunFireClip = "Firing";
    public string gunReloadClip = "Reloading";
    public string knifeAttackClip = "Hitting";

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
    }

    public void PlayAttackAnimation()
    {
        switch (currentMode)
        {
            case WeaponMode.Rifle:
                if (rifleAnim != null) { rifleAnim.Rewind(rifleFireClip); rifleAnim.Play(rifleFireClip); }
                break;
            case WeaponMode.Gun:
                if (gunAnim != null) { gunAnim.Rewind(gunFireClip); gunAnim.Play(gunFireClip); }
                break;
            case WeaponMode.Knife:
                if (knifeAnim != null) { knifeAnim.Rewind(knifeAttackClip); knifeAnim.Play(knifeAttackClip); }
                break;
        }
    }

    public void PlayReloadAnimation()
    {
        switch (currentMode)
        {
            case WeaponMode.Rifle:
                if (rifleAnim != null) { rifleAnim.Rewind(rifleReloadClip); rifleAnim.Play(rifleReloadClip); }
                break;
            case WeaponMode.Gun:
                if (gunAnim != null) { gunAnim.Rewind(gunReloadClip); gunAnim.Play(gunReloadClip); }
                break;
        }
    }
}