using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    public GameObject _dropItem;
    private void OnDisable()
    {
        if (gameObject.scene == null || !gameObject.scene.isLoaded)
            return;
        GameObject item=Instantiate(_dropItem,transform.position,transform.rotation);
#if UNITY_EDITOR
        Debug.Log("소환");
#endif
        if (item.TryGetComponent<ItemBase>(out ItemBase iB)==false)
        {
#if UNITY_EDITOR
            Debug.Log("삭제");
#endif
            Destroy(item);
        }
        item.GetComponent<Collider>().enabled = true;
        item.transform.localScale /= 0.3f;
        iB.SetData();
        iB._dropped = true;
        iB.Drop();
        
    }
}
