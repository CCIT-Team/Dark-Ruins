using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum eSound
{
    Bgm,
    UI,
    Max,
}

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)eSound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private GameObject _soundRoot = null; //사운드 재생기

    public void Init() //생성자
    {
        if (_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            if (_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                UnityEngine.Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = System.Enum.GetNames(typeof(eSound));
              
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }
                _audioSources[(int)eSound.Bgm].loop = true;
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        _audioClips.Clear();
    }

    public void Play(string key, eSound type, float volume = 1.0f, float pitch = 1.0f) //사운드 재생
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == eSound.Bgm)
        {
            LoadAudioClip(key, (audioClip) =>
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.clip = audioClip;
                audioSource.pitch = pitch;
                audioSource.volume = volume;

                audioSource.Play();
            });
        }

        else if (type == eSound.UI)
        {
            LoadAudioClip(key, (audioClip) =>
            {
                audioSource.pitch = pitch;
                audioSource.volume = volume;
                audioSource.PlayOneShot(audioClip);
            });
        }
    }

    public void Play3D(string key, Vector3 position, float volume = 1.0f, float pitch = 1.0f) //맵에서 사운드 재생
    {
        LoadAudioClip(key, (audioClip) =>
        {
            if (audioClip == null) return;

            GameObject go = new GameObject($"3D_Sound_{key}");
            go.transform.position = position;

            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.volume = volume;

            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = 1f;
            audioSource.maxDistance = 20f;

            audioSource.Play();
            UnityEngine.Object.Destroy(go, audioClip.length);
        });
    }

    public void Stop(eSound type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

    private void LoadAudioClip(string key, Action<AudioClip> callback) //클립가져오기
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(key, out audioClip))
        {
            callback?.Invoke(audioClip);
            return;
        }

        Managers_YGU.Resource.LoadAsync<AudioClip>(key, (audioClip) =>
        {
            if (!_audioClips.ContainsKey(key))
                _audioClips.Add(key, audioClip);
            callback?.Invoke(audioClip);
        });
    }
}
