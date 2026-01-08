using UnityEngine;

public class AutoDisable_KSM : MonoBehaviour
{
    public float lifeTime = 1.5f;
    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(DisableSelf), lifeTime);
    }

    void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}