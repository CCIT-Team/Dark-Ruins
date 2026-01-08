using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
public class DataLoader
{
    private static DataLoader _instance=new();
    public static DataLoader Instance => _instance;
    private Dictionary<string, Dictionary<string, object>> _dataCache = new Dictionary<string, Dictionary<string, object>>();

    [InitializeOnLoadMethod]
    private static void Load()
    {
        LoadAll();
    }

    [MenuItem("Tools/SetData")]
    public static void LoadAll()
    {
        _instance._dataCache.Clear();
        string folderPath = "Assets/@JsonFiles";

        if (!Directory.Exists(folderPath))
            return;

        foreach (var file in Directory.GetFiles(folderPath, "*.json", SearchOption.AllDirectories))
        {
            string jsonText = File.ReadAllText(file);

            try
            {
                var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonText);
                foreach (var entry in list)
                {
                    if (entry.TryGetValue("Name", out object name))
                    {
                        _instance._dataCache[$"{name}"] = entry;
                    }
                }
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError($"{file} ÆÄ½Ì ½ÇÆÐ: {e.Message}");
#endif
            }
        }
    }
    public Dictionary<string, object> FindByName(string name) //DataLoader.FindByName(name) ÇÏ¸é °ª ³ª¿È
    {
        if (_instance._dataCache.TryGetValue(name, out var value))
            return value;

        return null;
    }
    public void SetByName(string set,string name, object value)
    {
        _instance._dataCache[set][name] = value;
    }
}