using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputManager_KSM : MonoBehaviour
{
    public static InputManager_KSM Instance { get; private set; }

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    public static event Action<List<KeyCode>> OnKeysHeld;

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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ¾ÀÀÌ ¹Ù²î¾îµµ ÆÄ±«µÇÁö ¾Ê°Ô ¼³Á¤
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        _pressedKeys.Clear();

        foreach (KeyCode key in interestedKeys)
        {
            short state = GetAsyncKeyState((int)key);
            if ((state & 0x8000) != 0)
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