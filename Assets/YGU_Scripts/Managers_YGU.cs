using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers_YGU : MonoBehaviour
{
    static Managers_YGU s_instance;
    static Managers_YGU Instance { get { Init(); return s_instance; } }

    [Header("Resources")]
    public List<AudioClip> soundClips;          //사운드 파일
    public List<GameObject> effectPrefabs;      //이펙트 프리팹

    SoundManager _sound = new SoundManager();
    EffectManager _effect = new EffectManager();

    public static SoundManager Sound { get { return Instance?._sound; } }
    public static EffectManager Effect { get { return Instance?._effect; } }

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
            //s_instance._sound.Init(s_instance.soundClips);
            s_instance._effect.Init(s_instance.effectPrefabs);
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Effect.Clear();
    }
}
