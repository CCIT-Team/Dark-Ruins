using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers_KSM : MonoBehaviour
{
    static Managers_KSM s_instance;
    static Managers_KSM Instance { get { return s_instance; } }
    InputManager_KSM _input = new InputManager_KSM();
    public static InputManager_KSM Input { get { Init(); return Instance._input; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    static void Init()
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
}
