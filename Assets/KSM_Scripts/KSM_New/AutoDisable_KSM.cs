using UnityEngine;

public class AutoDisable_KSM : MonoBehaviour
{
    public float disableTime = 2.0f;
    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(DisableSelf), disableTime);
    }

    void DisableSelf()
    {
        gameObject.SetActive(false); 
    }
}