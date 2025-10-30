//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Flashlight_KSM : MonoBehaviour
//{
//    [Header("Lights")]
//    public Light regularLight;
//    public Light uvLight;

//    private bool isFlashlightOn = false;

//    void Start()
//    {
//        regularLight.enabled = true;
//        uvLight.enabled = false;
//    }

//    void Update()
//    {
//        if (isFlashlightOn)
//        {
//            UpdateShaderFlashlightData();
//        }
//    }

//    public void ToggleFlashlight()
//    {
//        if (regularLight == null || uvLight == null)
//        {
//            return;
//        }

//        regularLight.enabled = true;
//        uvLight.enabled = false;
//        isUVLightActive = false;
//    }

//    void UpdateShaderFlashlightData()
//    {
//        if (isFlashlightOn && flashlight != null)
//        {
//            Shader.SetGlobalVector("_FlashlightPos", flashlight.transform.position);
//            Shader.SetGlobalVector("_FlashlightDir", -flashlight.transform.forward);
//            Shader.SetGlobalFloat("_FlashlightRange", flashlight.range);
//            Shader.SetGlobalFloat("_FlashlightAngle", flashlight.spotAngle);
//        }
//        else
//        {
//            Shader.SetGlobalFloat("_FlashlightRange", 0.0f);
//        }
//    }
//}