using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers_YGU : MonoBehaviour
{
    static Managers_YGU s_instance;
    static Managers_YGU Instance { get { Init(); return s_instance; } }

    SoundManager _sound = new SoundManager();
    ResourceManager _resource = new ResourceManager();

    public static SoundManager Sound { get { return Instance?._sound; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }

    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@YGU_Manager");
            if (go == null)
            {
                go = new GameObject { name = "@YGU_Manager" };
                go.AddComponent<Managers_YGU>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers_YGU>();
            s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
    }

    private void Start()
    {
        Init();
    }
}
