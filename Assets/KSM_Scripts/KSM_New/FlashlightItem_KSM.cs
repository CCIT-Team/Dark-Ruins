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
    public Collider det1;

    public static bool isUVLightActive = false;
    public static event System.Action<bool> OnUVLightToggled;
    public static Transform uvLightTransform { get; private set; }

    private Inventory playerInventory;
    private HashSet<WeakPoint_KSM> weakPointsInRange = new HashSet<WeakPoint_KSM>();


    void Start()
    {
        if (regularLight == null || uvLight == null) return;

        regularLight.enabled = true;
        uvLight.enabled = false;
        isUVLightActive = false;

        uvLightTransform = uvLight.transform;

        if (det1 != null) det1.isTrigger = true;

        if (playerInventory == null)
        {
            playerInventory = transform.root.GetComponent<Inventory>();
        }
    }

    void Update()
    {
        DrainBattery();
    }

    public void OnTriggerEnter(Collider other)
    {
        WeakPoint_KSM wp = other.GetComponent<WeakPoint_KSM>();

        if (wp != null)
        {
            weakPointsInRange.Add(wp);

            if (isUVLightActive && currentBattery > 0)
            {
                wp.SetDetectedByUV(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        WeakPoint_KSM wp = other.GetComponent<WeakPoint_KSM>();

        if (wp != null)
        {
            weakPointsInRange.Remove(wp);

            wp.SetDetectedByUV(false);
        }
    }

    private void UpdateWeakPointsState()
    {
        foreach (WeakPoint_KSM wp in weakPointsInRange)
        {
            if (wp != null)
            {
                bool shouldActive = isUVLightActive && currentBattery > 0;
                wp.SetDetectedByUV(shouldActive);
            }
        }
    }

    private void ClearAllWeakPoints()
    {
        foreach (WeakPoint_KSM wp in weakPointsInRange)
        {
            if (wp != null) wp.SetDetectedByUV(false);
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
        if (playerInventory == null) playerInventory = transform.root.GetComponent<Inventory>();
        if (playerInventory == null) return;

        BatteryItem_KSM battery = playerInventory.CheckItem<BatteryItem_KSM>() as BatteryItem_KSM;

        if (battery != null)
        {
            if (battery.Count > 0)
            {
                battery.Consume();
                currentBattery += batteryReloadAmount;
                if (currentBattery > maxBattery) currentBattery = maxBattery;

                if (isUVLightActive) UpdateWeakPointsState();

                Debug.Log($"배터리 교체 완료: {currentBattery}");
            }
            else
            {
                Debug.LogWarning("배터리 아이템은 있는데 수량이 0임");
            }
        }
        else
        {
            Debug.Log("배터리 없음");
        }
    }

    public void Toggle()
    {
        if (currentBattery <= 0 && !isUVLightActive)
        {
            Debug.Log("배터리 부족");
            return;
        }

        isUVLightActive = !isUVLightActive;
        regularLight.enabled = !isUVLightActive;
        uvLight.enabled = isUVLightActive;

        OnUVLightToggled?.Invoke(isUVLightActive);

        UpdateWeakPointsState();
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