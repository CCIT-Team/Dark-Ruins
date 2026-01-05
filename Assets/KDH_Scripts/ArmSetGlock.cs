using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSetGlock : MonoBehaviour
{
    [SerializeField]
    private Glock _gl;
    private void OnEnable()
    {
        _gl.GetComponent<ItemBase>().Init();
        _gl.Subscribe();
    }
    private void OnDisable()
    {
        _gl.GetComponent<ItemBase>().Init();
        _gl.Unsubscribe();
    }
}
