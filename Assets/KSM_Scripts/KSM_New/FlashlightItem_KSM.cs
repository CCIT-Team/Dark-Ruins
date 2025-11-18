using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlashlightItem_KSM : MonoBehaviour, IEquipment_KSM
{
    [Header("Lights")]
    public Light regularLight;
    public Light uvLight;

    [Header("Battery Settings")]
    [Tooltip("현재 배터리 잔량")]
    [SerializeField]public float currentBattery = 100.0f;
    [Tooltip("최대 배터리 용량")]
    [SerializeField] public float maxBattery = 100.0f;
    [Tooltip("일반 빛 초당 소모량")]
    [SerializeField] public float regularLightDrain = 1.0f;
    [Tooltip("UV 빛 초당 소모량")]
    [SerializeField] public float uvLightDrain = 5.0f;
    [Tooltip("배터리 아이템 1개당 재충전되는 양")]
    [SerializeField] public float batteryReloadAmount = 50.0f;

    [Header("Detection Settings")]
    [Tooltip("UV 라이트 감지 거리")]
    [SerializeField] private float detectionRange = 15f;
    [Tooltip("약점 부위가 설정된 레이어")]
    [SerializeField] private LayerMask weakPointLayer;
    [Tooltip("감지 범위 반지름")]
    [SerializeField] private float detectionRadius = 1.0f;

    public static bool isUVLightActive = false;
    public static event System.Action<bool> OnUVLightToggled;
    public static Transform uvLightTransform { get; private set; }

    private Inventory playerInventory;
    private WeakPoint_KSM currentLookingWeakPoint;

    void Start()
    {
        if (regularLight == null || uvLight == null) return;

        regularLight.enabled = true;
        uvLight.enabled = false;
        isUVLightActive = false;

        uvLightTransform = uvLight.transform;

        if (playerInventory == null)
        {
            playerInventory = transform.root.GetComponent<Inventory>();
        }
    }

    void Update()
    {
        DrainBattery();

        if (isUVLightActive && currentBattery > 0)
        {
            DetectWeakPoint();
        }
        else
        {
            ClearCurrentWeakPoint();
        }
    }

    private void DetectWeakPoint()
    {
        Ray ray = new Ray(uvLightTransform.position, uvLightTransform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, detectionRadius, out hit, detectionRange, weakPointLayer))
        {
            WeakPoint_KSM weakPoint = hit.collider.GetComponent<WeakPoint_KSM>();

            if (weakPoint != null)
            {
                if (currentLookingWeakPoint != weakPoint)
                {
                    ClearCurrentWeakPoint();
                    currentLookingWeakPoint = weakPoint;
                    currentLookingWeakPoint.SetDetectedByUV(true);
                }
                return;
            }
        }
        ClearCurrentWeakPoint();
    }

    private void ClearCurrentWeakPoint()
    {
        if (currentLookingWeakPoint != null)
        {
            currentLookingWeakPoint.SetDetectedByUV(false);
            currentLookingWeakPoint = null;
        }
    }

    void DrainBattery()
    {
        if (uvLight.enabled)
        {
            currentBattery -= uvLightDrain * Time.deltaTime;
        }

        if (currentBattery <= 0 && (uvLight.enabled || regularLight.enabled))
        {
            ReloadBattery();

            if (currentBattery <= 0)
            {
                currentBattery = 0;

                if (uvLight.enabled)
                {
                    TurnOffUV();
                }
            }
        }
    }

    public void ReloadBattery()
    {
        if (currentBattery >= maxBattery) return;

        if (playerInventory == null)
            playerInventory = transform.root.GetComponent<Inventory>();

        if (playerInventory == null) return;

        BatteryItem_KSM battery = playerInventory.CheckItem<BatteryItem_KSM>() as BatteryItem_KSM;

        if (battery != null)
        {
            if (battery.Count > 0)
            {
                battery.Consume();

                currentBattery += batteryReloadAmount;
                if (currentBattery > maxBattery)
                {
                    currentBattery = maxBattery;
                }

                Debug.Log($"배터리 갈아끼기: {currentBattery}, 남은 배터리 수량: {battery.Count}");
            }
            else
            {
                Debug.LogWarning("승민씨의 병신같은 코드1");
            }
        }
        else
        {
            Debug.Log("너 배터리가 없다고");
        }
    }

    public void Toggle()
    {
        if (currentBattery <= 0 && !isUVLightActive)
        {
            Debug.Log("배터리 부족, UV 라이트를 못켜.");
            return;
        }

        isUVLightActive = !isUVLightActive;
        regularLight.enabled = !isUVLightActive;
        uvLight.enabled = isUVLightActive;

        OnUVLightToggled?.Invoke(isUVLightActive);
    }

    void TurnOffUV()
    {
        uvLight.enabled = false;
        isUVLightActive = false;
        regularLight.enabled = true;

        OnUVLightToggled?.Invoke(isUVLightActive);

        ClearCurrentWeakPoint();
    }
}