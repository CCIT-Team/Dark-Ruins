using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UVPuzzle : WeakPoint_KSM
{
    [SerializeField]
    TextMeshPro tmp,pair;
    public Action<string> completedCallback;


    private Color orgT, orgP;
    private void Start()
    {
        orgT = tmp.color;
        orgP =pair.color;
    }

    public void Reset()
    {
        isDetected = false;
        tmp.color = orgT;
        pair.color = orgP;
    }

    public override void SetDetectedByUV(bool detected)
    {
        if(isDetected || (detected == false))
        {
            
            return;
        }
        isDetected = true;
        tmp.color = Color.blue;
        pair.color = Color.blue;
        completedCallback?.Invoke(gameObject.name);
    }

}
