using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSetRifle : MonoBehaviour
{
    [SerializeField]
    private Rifle _rf;
    private void OnEnable()
    {
        _rf.GetComponent<ItemBase>().Init();
        _rf.Subscribe();
    }
    private void OnDisable()
    {
        _rf.GetComponent<ItemBase>().Init();
        _rf.Unsubscribe();
    }
}
