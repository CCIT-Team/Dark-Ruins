using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputManager_KSM
{
    public event Action<List<KeyCode>> OnKeysHeld;

    private KeyCode[] interestedKeys = new KeyCode[]
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.Space,
        KeyCode.LeftShift,
        KeyCode.I,
        KeyCode.Mouse0,
    };

    private List<KeyCode> _pressedKeys = new List<KeyCode>();
    public void OnUpdate()
    {
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