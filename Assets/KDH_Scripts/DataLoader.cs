using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
public static class DataLoader
{
    private static Dictionary<string, List<Dictionary<string, string>>> _dataCache
        = new Dictionary<string, List<Dictionary<string, string>>>();
    [InitializeOnLoadMethod]
    private static void Load()
    {
        DataLoader.LoadAll();
    }

    [MenuItem("Tools/SetData")]
    public static void LoadAll()
    {
        _dataCache.Clear();
        string folderPath = "Assets/@JsonFiles";

        if (!Directory.Exists(folderPath))
        {
            return;
        }

        foreach (var file in Directory.GetFiles(folderPath, "*.json", SearchOption.AllDirectories))
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string jsonText = File.ReadAllText(file);

            try
            {
                var list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonText);
                _dataCache[fileName] = list;
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError($"{fileName} ÆÄ½Ì ½ÇÆÐ: {e.Message}");
#endif
            }
        }
    }

    public static Dictionary<string, string> FindByName(string category, string name)
    {
        if (!_dataCache.ContainsKey(category))
        {
            return null;
        }

        var list = _dataCache[category];
        foreach (var entry in list)
        {
            if (entry.TryGetValue("Name", out string value) && value == name)
                return entry;
        }
        return null;
    }
}