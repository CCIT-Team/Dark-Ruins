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
        KeyCode.R,
        KeyCode.Mouse0,
        KeyCode.Escape,
        KeyCode.Q
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

            if (Input.GetKeyDown(key))
            {
                OnKeysPressed?.Invoke(key);
            }
        }

        OnKeysHeld?.Invoke(new List<KeyCode>(_pressedKeys));
    }
}