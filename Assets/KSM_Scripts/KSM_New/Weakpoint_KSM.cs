using UnityEngine;
public class WeakPoint : MonoBehaviour
{
    [Header("활성화")]
    [SerializeField] private float requiredExposure = 3.0f;
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private Renderer WeakPointRenderer;
    [SerializeField] private float uvLightRadius = 0.5f;

    public BoxCollider weakcollider;

    private MonsterController_KSM monsterController;
    private Material normalMaterial;
    private Collider col;

    private float currentExposureTimer = 0f;
    private bool isExposed = false;

    void Start()
    {
        col = GetComponent<Collider>();
        monsterController = GetComponentInParent<MonsterController_KSM>();

        if (WeakPointRenderer == null)
        {
            WeakPointRenderer = GetComponent<Renderer>();
        }
        if (WeakPointRenderer != null)
        {
            normalMaterial = WeakPointRenderer.material;
        }
    }

    void Update()
    {
        if (isExposed || monsterController == null || !Flashlight2_KSM.isUVLightActive)
        {
            currentExposureTimer = 0f;
            return;
        }

        if (IsLightHittingThisWeakPoint())
        {
            currentExposureTimer += Time.deltaTime;

            if (currentExposureTimer >= requiredExposure)
            {
                ActivateWeakPoint();
            }
        }
        else
        {
            currentExposureTimer = 0f;
        }
    }

    private bool IsLightHittingThisWeakPoint()
    {
        Transform lightSource = Flashlight2_KSM.uvLightTransform;
        if (lightSource == null) return false;

        RaycastHit hit;

        int layerMask = 1 << LayerMask.NameToLayer("WeakPoint");

        if (Physics.SphereCast(lightSource.position, uvLightRadius, lightSource.forward, out hit, 100f, layerMask))
        {
            if (hit.collider == col)
            {
                return true;
            }
        }
        return false;
    }

    private void ActivateWeakPoint()
    {
        isExposed = true;

        if (WeakPointRenderer != null && highlightedMaterial != null)
        {
            WeakPointRenderer.material = highlightedMaterial;
        }

        monsterController.NotifyWeakPointExposed();

        Debug.Log(gameObject.name + " 약점 활성화!");
    }
}