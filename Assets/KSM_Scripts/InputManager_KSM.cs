using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputManager_KSM
{
    //Å°¾×¼Ç
    public Action KeyAction;
    public event Action<List<KeyCode>> OnKeysHeld;
    private List<KeyCode> _pressedKeys = new List<KeyCode>();

    private KeyCode[] interestedKeys = new KeyCode[]
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.Space,
        KeyCode.LeftShift,
        KeyCode.I,
        KeyCode.E,
        KeyCode.Mouse0,
    };


    public void OnUpdate()
    {
        if (Input.anyKey == false) return;

        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
        _pressedKeys.Clear();

        foreach (KeyCode key in interestedKeys)
        {
            if(Input.GetKey(key))
            {
                _pressedKeys.Add(key);
            }
        }

        if (_pressedKeys.Count > 0)
        {
            OnKeysHeld?.Invoke(new List<KeyCode>(_pressedKeys));
        }
    }
}