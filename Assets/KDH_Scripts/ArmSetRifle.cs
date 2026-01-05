using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ArmSetRifle : MonoBehaviour
{
    [SerializeField]
    private Rifle _rf;
    private void OnEnable()
    {
        _rf.GetComponent<ItemBase>().Init();
        _rf.Subscribe();
        _rf.GetComponent<ItemBase>().OnPickUp();
        GameObject.Find("InventoryView").GetChild<Transform>("UI_Root").gameObject.SetActive(true);
        GameObject.Find("InventoryView").GetChild<Transform>("UI_Root").GetComponent<LookPlayer>().On(_rf.GetComponent<ItemBase>());
    }
    private void OnDisable()
    {
        _rf.GetComponent<ItemBase>().Init();
        _rf.Unsubscribe();
    }
}
