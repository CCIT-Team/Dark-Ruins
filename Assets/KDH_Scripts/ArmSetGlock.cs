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
        _gl.Init();
        _gl.Subscribe();
        _gl.OnPickUp();
        GameObject g = GameObject.Find("InventoryView");
        if (g is null)
        {
            return;
        }
        g.GetChild<Transform>("UI_Root").gameObject.SetActive(true);
        g.GetChild<Transform>("UI_Root").GetComponent<ItemDescription>().On(_gl.GetComponent<ItemBase>());
    }
    private void OnDisable()
    {
        _gl.Init();
        _gl.Unsubscribe();
    }
}
