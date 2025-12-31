using System.Collections.Generic;
using UnityEngine;

public class BloodPoolManager_KSM : MonoBehaviour
{
    public static BloodPoolManager_KSM Instance;

    [Header("설정")]
    public GameObject bloodPrefab;
    public int poolSize = 10;

    private List<GameObject> poolList;

    private void Awake()
    {
        Instance = this;
        poolList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewBlood();
        }
    }

    private GameObject CreateNewBlood()
    {
        GameObject obj = Instantiate(bloodPrefab, transform);
        obj.SetActive(false);
        poolList.Add(obj);
        return obj;
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
                return;
            }
        }

        GameObject newBlood = CreateNewBlood();
        newBlood.transform.position = position;
        newBlood.transform.rotation = rotation;
        newBlood.SetActive(true);
    }
}