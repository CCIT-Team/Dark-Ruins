using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ArmSetGlock : MonoBehaviour
{
    [SerializeField]
    private Glock _gl;
    private void OnEnable()
    {
        _gl.GetComponent<ItemBase>().Init();
        _gl.Subscribe();
        _gl.GetComponent<ItemBase>().OnPickUp();
        GameObject.Find("InventoryView").GetChild<Transform>("UI_Root").gameObject.SetActive(true);
        GameObject.Find("InventoryView").GetChild<Transform>("UI_Root").GetComponent<LookPlayer>().On(_gl.GetComponent<ItemBase>());
    }
    private void OnDisable()
    {
        _gl.GetComponent<ItemBase>().Init();
        _gl.Unsubscribe();
    }
}
