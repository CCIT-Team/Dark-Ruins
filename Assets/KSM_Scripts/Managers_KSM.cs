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
        _input.OnUpdate();
    }

    void OnDisable()
    {
        _input = null;
    }

    static void Init()
    {
        if (s_instance == null) // ���������� ����� Manager�� �ִ���
        {
            GameObject go = GameObject.Find("@Managers"); // Manager �ڵ带 ���� ��ü�� ã�´�
            if (go == null) // ������ Manager ��ũ��Ʈ�� ���� ������Ʈ�� �����.
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers_KSM>();
            }
            DontDestroyOnLoad(go); // �� �ٲ��� �Ȼ����
            s_instance = go.GetComponent<Managers_KSM>();
            // ������ ���� ������Ʈ�� ������Ʈ�� ���� Managers �� �����´�.
        }
    }
}
