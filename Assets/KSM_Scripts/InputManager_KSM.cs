using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputManager_KSM
{
    public Action KeyAction;
    public event Action<List<KeyCode>> OnKeysHeld;
    public event Action<KeyCode> OnKeysPressed;

    private List<KeyCode> _pressedKeys = new List<KeyCode>();

    #region KeyCodes
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
        KeyCode.F,
        KeyCode.Mouse0,
    };
    #endregion

    public void OnUpdate()
    {
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

        OnKeysHeld?.Invoke(new List<KeyCode>(_pressedKeys));
    }
}