using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlashlightItem_KSM : MonoBehaviour, IEquipment_KSM
{
    [Header("Lights")]
    public Light regularLight;
    public Light uvLight;

    [Header("베터리 설정")]
    [Tooltip("현재 배터리 잔량")]
    [SerializeField]public float currentBattery = 100.0f;
    [SerializeField] public float maxBattery = 100.0f;
    [SerializeField] public float regularLightDrain = 1.0f;
    [SerializeField] public float uvLightDrain = 5.0f;
    [SerializeField] public float batteryReloadAmount = 100.0f;

    [Header("감지 설정")]
    [Tooltip("감지 최대 거리")]
    [SerializeField] private float detectionRange = 20f;
    [Tooltip("감지 시야각")]
    [SerializeField] private float detectionAngle = 50f;
    [Tooltip("약점 부위가 설정된 레이어")]
    [SerializeField] private LayerMask weakPointLayer;

    public static bool isUVLightActive = false;
    public static event System.Action<bool> OnUVLightToggled;
    public static Transform uvLightTransform { get; private set; }

    private Inventory playerInventory;
    private WeakPoint_KSM currentLookingWeakPoint;
    private HashSet<WeakPoint_KSM> activeWeakPoints = new HashSet<WeakPoint_KSM>();

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
            DetectMultipleWeakPoints();
        }
        else
        {
            ClearAllWeakPoints();
        }
    }

    private void DetectMultipleWeakPoints()
    {
        HashSet<WeakPoint_KSM> detectedThisFrame = new HashSet<WeakPoint_KSM>();

        Collider[] targets = Physics.OverlapSphere(uvLightTransform.position, detectionRange, weakPointLayer);

        foreach (Collider col in targets)
        {
            Vector3 directionToTarget = (col.transform.position - uvLightTransform.position).normalized;
            float angleToTarget = Vector3.Angle(uvLightTransform.forward, directionToTarget);

            if (angleToTarget < detectionAngle / 2f)
            {
                WeakPoint_KSM wp = col.GetComponent<WeakPoint_KSM>();
                if (wp != null)
                {
                    detectedThisFrame.Add(wp);
                }
            }
        }
        foreach (WeakPoint_KSM wp in activeWeakPoints)
        {
            if (!detectedThisFrame.Contains(wp))
            {
                if (wp != null) wp.SetDetectedByUV(false);
            }
        }
        foreach (WeakPoint_KSM wp in detectedThisFrame)
        {
            if (wp != null) wp.SetDetectedByUV(true);
        }
        activeWeakPoints = detectedThisFrame;
    }

    private void ClearAllWeakPoints()
    {
        foreach (WeakPoint_KSM wp in activeWeakPoints)
        {
            if (wp != null) wp.SetDetectedByUV(false);
        }
        activeWeakPoints.Clear();
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

        ClearAllWeakPoints();
    }
}