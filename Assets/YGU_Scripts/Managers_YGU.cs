using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers_YGU : MonoBehaviour
{
    static Managers_YGU s_instance;
    static Managers_YGU Instance { get { Init(); return s_instance; } }

    [Header("Resources")]
    public List<GameObject> effectPrefabs;      //¿Ã∆Â∆Æ «¡∏Æ∆’

    SoundManager _sound = new SoundManager();
    EffectManager _effect = new EffectManager();
    ResourceManager _resource = new ResourceManager();

    public static SoundManager Sound { get { return Instance?._sound; } }
    public static EffectManager Effect { get { return Instance?._effect; } }
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
            s_instance._effect.Init(s_instance.effectPrefabs);
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Effect.Clear();
    }

    private void Start()
    {
        Init();
    }
}
