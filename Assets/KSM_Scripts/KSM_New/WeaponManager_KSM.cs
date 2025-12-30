using UnityEngine;

public class WeaponManager_KSM : MonoBehaviour
{
    public enum WeaponMode { None, Rifle, Gun, Knife }

    [Header("현재 상태")]
    public WeaponMode currentMode = WeaponMode.Knife;

    [Header("스크립트 연결")]
    public GunBase gunScript;
    public Knife3_KSM knifeScript;

    public PlayerController_KSM playerController;

    void Start()
    {
        if (playerController == null)
            playerController = GetComponentInParent<PlayerController_KSM>();

        SetWeaponMode(WeaponMode.None);
    }

    void Update()
    {
        if (currentMode == WeaponMode.Knife && knifeScript != null && knifeScript.isAttacking)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeaponMode(WeaponMode.Rifle);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeaponMode(WeaponMode.Gun);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeaponMode(WeaponMode.Knife);
    }

    void SetWeaponMode(WeaponMode mode)
    {
        if (currentMode == mode) return;
        currentMode = mode;

        if (gunScript != null) gunScript.gameObject.SetActive(false);
        if (knifeScript != null) knifeScript.gameObject.SetActive(false);

        IWeapon_KSM newWeapon = null;

        switch (mode)
        {
            case WeaponMode.Rifle:
                if (gunScript != null)
                {

                }
                break;

            case WeaponMode.Gun:
                if (gunScript != null)
                {
                    gunScript.gameObject.SetActive(true);
                    newWeapon = null;
                }
                break;

            case WeaponMode.Knife:
                if (knifeScript != null)
                {
                    knifeScript.gameObject.SetActive(true);
                    newWeapon = knifeScript;
                }
                break;
        }

        if (playerController != null)
        {
            playerController.ChangeWeapon(newWeapon);
        }
    }
}