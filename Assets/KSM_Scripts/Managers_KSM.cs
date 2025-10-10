using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers_KSM : MonoBehaviour
{
    static Managers_KSM s_instance;
    public static Managers_KSM Instance { get { return s_instance; } }

    #region Contents
    InputManager_KSM _input = new InputManager_KSM();


    public static InputManager_KSM Input { get { Init(); return Instance._input; } }

    #endregion

    #region inputManager
    void Update()
    {
        _input.OnUpdate();
    }

    void OnDisable()
    {
        _input = null;
        s_instance = null;
    }

    #endregion

    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers_KSM>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers_KSM>();
        }
    }
}
