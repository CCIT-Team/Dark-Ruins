using System.Collections.Generic;
using UnityEngine;

public class BloodPoolManager_KSM : MonoBehaviour
{
    public static BloodPoolManager_KSM Instance;

    [Header("설정")]
    public Transform effectPoolParent;

    private List<GameObject> poolList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        if (effectPoolParent == null) effectPoolParent = transform;

        foreach (Transform child in effectPoolParent)
        {
            poolList.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    public void PlayBloodEffect(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject blood in poolList)
        {
            if (!blood.activeInHierarchy)
            {
                blood.transform.position = position;
                blood.transform.rotation = rotation;
                blood.SetActive(true);
                Managers_YGU.Sound.Play("Machete_Attack", Sound.UI);
                return;
            }
        }
    }
}