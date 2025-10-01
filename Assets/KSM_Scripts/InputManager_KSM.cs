using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputManager_KSM
{
<<<<<<< Updated upstream
    // Delegate 
    public Action keyaction = null;
=======
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


>>>>>>> Stashed changes

    public void OnUpdate()
    {
        _pressedKeys.Clear();

<<<<<<< Updated upstream
        // 어떤 키가 들어왔다면, keyaction에서 이벤트가 발생했음을 전파. 
        if (keyaction != null)
        {
            keyaction.Invoke();
=======
        foreach (KeyCode key in interestedKeys)
        {
            if (Input.GetKey(key))
            {
                _pressedKeys.Add(key);
            }
        }
>>>>>>> Stashed changes

        if (_pressedKeys.Count > 0)
        {
            OnKeysHeld?.Invoke(new List<KeyCode>(_pressedKeys));
        }
    }
}