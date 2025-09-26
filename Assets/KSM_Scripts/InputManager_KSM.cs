using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_KSM
{
    // Delegate 
    public Action KeyAction = null;

    // InputMangers will detect inputs in OnUdate()
    public void OnUpdate()
    {
        if (Input.anyKey == false) return;

        if (KeyAction != null)
        {
            KeyAction.Invoke();
            
        }
    }
}
