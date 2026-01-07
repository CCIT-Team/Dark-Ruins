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
        GameObject g = GameObject.Find("InventoryView");
        if(g is null)
        {
            return;
        }
        g.GetChild<Transform>("UI_Root").gameObject.SetActive(true);
        g.GetChild<Transform>("UI_Root").GetComponent<LookPlayer>().On(_rf.GetComponent<ItemBase>());
    }
    private void OnDisable()
    {
        _rf.GetComponent<ItemBase>().Init();
        _rf.Unsubscribe();
    }
}
