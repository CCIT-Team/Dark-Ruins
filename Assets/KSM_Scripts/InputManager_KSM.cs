using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputManager_KSM
{
    public static InputManager_KSM Instance { get; private set; }

    void start()
    {
        Init();
    }

    Managers_KSM s_instance; // 싱글톤 인스턴스

    void Init()
    {
        if (s_instance == null) // 전역변수로 선언된 Manager가 있는지
        {
            GameObject go = GameObject.Find("@Managers"); // Manager 코드를 담을 객체를 찾는다
            if (go == null) // 없으면 Manager 스크립트를 붙인 오브젝트를 만든다.
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers_KSM>();
            }
            DontDestroyOnLoad(go); // 씬 바껴도 안사라짐
            s_instance = go.GetComponent<Managers_KSM>();
            // 생성한 게임 오브젝트의 컴포넌트로 붙은 Managers 를 가져온다.
        }
    }

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    // 구독자들에게 눌린 키 전달
    public static event Action<List<KeyCode>> OnKeysHeld;

    // 관심 있는 키만 등록
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

    private List<KeyCode> _heldKeys = new List<KeyCode>();

    void Update()
    {
        _heldKeys.Clear();

        foreach (KeyCode key in interestedKeys)
        {
            short state = GetAsyncKeyState((int)key);
            if ((state & 0x8000) != 0) // 눌린 상태 확인
            {
                _heldKeys.Add(key);
            }
        }

        OnKeysHeld?.Invoke(new List<KeyCode>(_heldKeys));
    }
}
