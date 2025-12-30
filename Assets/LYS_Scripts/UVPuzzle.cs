using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UVPuzzle : WeakPoint_KSM
{
    [SerializeField]
    TextMeshPro tmp,pair;
    public Action completedCallback;
    private void Start()
    {
        
    }
    public override void SetDetectedByUV(bool detected)
    {
        if(isDetected)
        {
            return;
        }
        tmp.color = Color.blue;
        pair.color = Color.blue;
        isDetected = detected;
        completedCallback?.Invoke();
    }

}
