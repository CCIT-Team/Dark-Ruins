using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager
{
    //로드한 리소스들
    Dictionary<string, Object> resourceList = new Dictionary<string, Object>();

    //public T Load<T>(string key) where T : Object //로드했던 리소스인지 확인
    //{
    //    if (resourceList.TryGetValue(key, out Object resource))
    //        return resource as T;

    //    return null;
    //}

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
    {
        string loadKey = key;

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) =>
        {
            if (resourceList.TryGetValue(key, out Object resource))
            {
                callback?.Invoke(op.Result);
                return;
            }

            resourceList.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }

    //public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : Object
    //{
    //    var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
    //    opHandle.Completed += (op) =>
    //    {
    //        int loadCount = 0;

    //        int totalCount = op.Result.Count;

    //        foreach (var result in op.Result)
    //        {
    //            LoadAsync<T>(result.PrimaryKey, (obj) =>
    //            {
    //                loadCount++;
    //                callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
    //            });
    //        }
    //    };
    //}

    //private void start()
    //{
    //    LoadAllAsync<AudioClip>("Preload", null);
    //}
}
