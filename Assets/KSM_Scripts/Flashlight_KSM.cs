using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_KSM : MonoBehaviour
{
    public Light flashlight;
    private bool isFlashlightOn = false;

    void Start()
    {
        flashlight = GetComponent<Light>();

        isFlashlightOn = flashlight.enabled;
        UpdateShaderFlashlightData();
    }
    void OnEnable()
    {
        Managers_KSM.Input.OnKeysHeld += HandleKeysHeld;
    }

    void OnDisable()
    {
        if (Managers_KSM.Instance != null)
        {
            Managers_KSM.Input.OnKeysHeld -= HandleKeysHeld;
        }
    }

    void Update()
    {
        if (isFlashlightOn)
        {
            UpdateShaderFlashlightData();
        }
    }

    private void HandleKeysHeld(List<KeyCode> heldKeys)
    {
        if (heldKeys.Contains(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }
    public void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            isFlashlightOn = !isFlashlightOn;
            flashlight.enabled = isFlashlightOn;
            UpdateShaderFlashlightData();
        }
    }

    void UpdateShaderFlashlightData()
    {
        if (isFlashlightOn && flashlight != null)
        {
            Shader.SetGlobalVector("_FlashlightPos", flashlight.transform.position);
            Shader.SetGlobalVector("_FlashlightDir", -flashlight.transform.forward);
            Shader.SetGlobalFloat("_FlashlightRange", flashlight.range);
            Shader.SetGlobalFloat("_FlashlightAngle", flashlight.spotAngle);
        }
        else
        {
            Shader.SetGlobalFloat("_FlashlightRange", 0.0f);
        }
    }
}