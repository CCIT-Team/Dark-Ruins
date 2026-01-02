using UnityEngine;
public class WeakPoint_KSM : MonoBehaviour
{
    [Header("이펙트 설정")]
    [SerializeField] private GameObject weakPointEffect;

    private bool isDetected = false;
    private ParticleSystem ps;

    private void Awake()
    {
        if (weakPointEffect != null)
        {
            ps = weakPointEffect.GetComponent<ParticleSystem>();
        }
    }

    private void Start()
    {
        if (weakPointEffect != null)
        {
            weakPointEffect.SetActive(false);
        }
    }

    public void SetDetectedByUV(bool detected)
    {
        if (isDetected != detected)
        {
            isDetected = detected;
            UpdateEffectState();
        }
    }

    private void UpdateEffectState()
    {
        if (weakPointEffect == null) return;

        if (isDetected)
        {
            weakPointEffect.SetActive(true);
            if (ps != null) ps.Play();
        }
        else
        {
            if (ps != null) ps.Stop();
            weakPointEffect.SetActive(false);
        }
    }
}