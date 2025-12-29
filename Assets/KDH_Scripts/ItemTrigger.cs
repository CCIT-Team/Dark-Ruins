using UnityEngine;

public class TriggerRayInteractor : MonoBehaviour
{
    public Transform cameraTransform;
    public float rayDistance = 4f;
    public LayerMask interactableMask;

    private bool canRaycast,pendingRaycast = false;
    private void Start()
    {
        cameraTransform= GetComponentInParent<Camera>().transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & interactableMask) == 0)
            return;
#if UNITY_EDITOR
        Debug.Log("시선");
#endif
        canRaycast = true;
        pendingRaycast = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & interactableMask) == 0)
            return;
        canRaycast = false;
        if (Item is not default(ItemBase))
        {
            Item.OutFocused();
            Item=default(ItemBase);
        }
    }
    private void LateUpdate()
    {
        if (!pendingRaycast) return;

        FireRayOnce();
        pendingRaycast = false;
    }
    private ItemBase Item;
    private void FireRayOnce()
    {
        if (!canRaycast) return;
#if UNITY_EDITOR
        Debug.DrawRay(
            cameraTransform.position,
            cameraTransform.forward * rayDistance,
            Color.red,
            1f
        );
#endif
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayDistance, interactableMask))
        {
            if (hit.transform.TryGetComponent<ItemBase>(out ItemBase item))
            {
                Item = item;
                item.OnFocused();
            }
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log("레이 자체가 안 맞음");
        }
#endif
    }
}
