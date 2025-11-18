using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
public static class DataLoader
{
    private static Dictionary<string, Dictionary<string, string>> _dataCache = new Dictionary<string, Dictionary<string, string>>();

    [InitializeOnLoadMethod]
    private static void Load()
    {
        LoadAll();
    }

    [MenuItem("Tools/SetData")]
    public static void LoadAll()
    {
        _dataCache.Clear();
        string folderPath = "Assets/@JsonFiles";

        if (!Directory.Exists(folderPath))
            return;

        foreach (var file in Directory.GetFiles(folderPath, "*.json", SearchOption.AllDirectories))
        {
            string jsonText = File.ReadAllText(file);

            try
            {
                var list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonText);
                foreach (var entry in list)
                {
                    if (entry.TryGetValue("Name", out string name))
                    {
                        _dataCache[name] = entry;
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
    public static Dictionary<string, string> FindByName(string name) //DataLoader.FindByName(name) ÇÏ¸é °ª ³ª¿È
    {
        if (_dataCache.TryGetValue(name, out var value))
            return value;

        return null;
    }
}