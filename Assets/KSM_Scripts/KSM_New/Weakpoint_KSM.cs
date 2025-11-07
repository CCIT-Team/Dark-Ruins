using UnityEngine;
public class WeakPoint_KSM : MonoBehaviour
{
    [Header("활성화")]
    [SerializeField] private float requiredExposure = 0.1f;
    [SerializeField] private Material highlightedMaterial;

    private Renderer weakPointRenderer;
    private Material normalMaterial;
    private MonsterController_KSM monsterController;

    private float currentExposureTimer = 0f;
    private bool isExposed = false;
    private bool isDetected = false;

    void Start()
    {
        monsterController = GetComponentInParent<MonsterController_KSM>();
        weakPointRenderer = GetComponent<Renderer>();

        if (weakPointRenderer != null)
        {
            normalMaterial = weakPointRenderer.material;
        }
    }

    void Update()
    {
        if (isDetected)
        {
            if (!isExposed)
            {
                currentExposureTimer += Time.deltaTime;
                if (currentExposureTimer >= requiredExposure)
                {
                    ActivateWeakPoint();
                }
            }
        }

        else
        {
            if (currentExposureTimer > 0f || isExposed)
            {
                DeactivateWeakPoint();
            }
        }
    }

    public void SetDetectedByUV(bool detected)
    {
        isDetected = detected;
    }

    private void ActivateWeakPoint()
    {
        isExposed = true;
        if (weakPointRenderer != null && highlightedMaterial != null)
        {
            weakPointRenderer.material = highlightedMaterial;
        }
        monsterController?.NotifyWeakPointExposed();
        Debug.Log($"{name} 약점 활성화됨!");
    }

    private void DeactivateWeakPoint()
    {
        isExposed = false;
        currentExposureTimer = 0f;
        if (weakPointRenderer != null && normalMaterial != null)
        {
            weakPointRenderer.material = normalMaterial;
        }
        monsterController?.NotifyWeakPointHidden();
        Debug.Log($"{name} 약점 비활성화됨.");
    }
}