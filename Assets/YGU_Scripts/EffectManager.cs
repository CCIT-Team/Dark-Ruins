using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    private Dictionary<string, GameObject> _effects = new Dictionary<string, GameObject>();

    public void Init(List<GameObject> prefabs)
    {
        _effects.Clear();
        foreach(GameObject prefab in prefabs)
        {
            if (prefab != null && !_effects.ContainsKey(prefab.name))
                _effects.Add(prefab.name, prefab);
        }
    }

    public void Play(string key, Vector3 worldPos, float duration = 2.0f)
    {
        if (_effects.TryGetValue(key, out GameObject prefab))
        {
            GameObject go = Object.Instantiate(prefab, worldPos, Quaternion.identity);
            Object.Destroy(go, duration);
        }
        //else Debug.Log($"{key}없음 오류")
    }

    public void Clear()
    {
        _effects.Clear();
    }
}
