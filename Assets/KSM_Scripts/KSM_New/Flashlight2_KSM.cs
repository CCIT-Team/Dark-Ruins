using UnityEngine;

public class Flashlight2_KSM : MonoBehaviour, IEquipment_KSM
{
    [Header("Lights")]
    public Light regularLight;
    public Light uvLight;

    public static bool isUVLightActive = false;

    public static event System.Action<bool> OnUVLightToggled;

    void Start()
    {
        if (regularLight == null || uvLight == null) return;

        regularLight.enabled = true;
        uvLight.enabled = false;
    }

    public void Toggle()
    {
        if (regularLight == null || uvLight == null) return;

        isUVLightActive = !isUVLightActive;
        regularLight.enabled = !isUVLightActive;
        uvLight.enabled = isUVLightActive;

        OnUVLightToggled?.Invoke(isUVLightActive);
    }
}