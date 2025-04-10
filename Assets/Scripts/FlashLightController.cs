using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;

    private bool isOn = true;

    void Start()
    {
        flashlight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}