using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum Sound
{
    Bgm,
    Effect,
    Max,
}

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Sound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private GameObject _soundRoot = null; //사운드 재생기

    public void Init(List<AudioClip> clips) //생성자
    {
        if (_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            if (_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = System.Enum.GetNames(typeof(Sound));
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSources[(int)Sound.Bgm].loop = true;
            }

            _audioClips.Clear();
            if (clips ==null)
            {
                return;
            }
            foreach (AudioClip clip in clips)
            {
                if (!_audioClips.ContainsKey(clip.name))
                    _audioClips.Add(clip.name, clip);
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        _audioClips.Clear();
    }

    public void Play(string key, Sound type, float pitch = 1.0f) //사운드 재생
    {
        AudioClip audioClip = GetAudioClip(key);
        if (audioClip == null) return;

        AudioSource audioSource = _audioSources[(int)type];
        audioSource.pitch = pitch;

        if (type == Sound.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();

        }
        else if (type == Sound.Effect)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Stop(Sound type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

    private AudioClip GetAudioClip(string key) //클립가져오기
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(key, out audioClip))
            return audioClip;

        //Debug.Log($"{key}없음")
        return null;

    }
}
