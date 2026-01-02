using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class CSVToJson
{
    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel()
    {
        string csvFolderPath = "Assets/@ExcelFiles";
        string jsonFolderPath = "Assets/@JsonFiles";
        if (!Directory.Exists(jsonFolderPath))
            Directory.CreateDirectory(jsonFolderPath);

        string[] files = Directory.GetFiles(csvFolderPath, "*.csv", SearchOption.TopDirectoryOnly);

        foreach (string file in files)
        {
            string[] lines = File.ReadAllLines(file);

            if (lines.Length < 2) continue;

            string[] headers = lines[0].Split(',');

            var rows = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(',');
                var row = new Dictionary<string, string>();
                for (int j = 0; j < headers.Length; j++)
                {
                    string value = j < cells.Length ? cells[j] : "";
                    row[headers[j]] = value;
                }
                rows.Add(row);
            }

            string json = JsonConvert.SerializeObject(rows, Formatting.Indented);

            string fileName = Path.GetFileNameWithoutExtension(file) + ".json";
            string outputPath = Path.Combine(jsonFolderPath, fileName);
            File.WriteAllText(outputPath, json);

            Debug.Log($"{fileName} 변환 완료");
        }

        AssetDatabase.Refresh();
        
        Debug.Log("모든 CSV 변환 완료");
    }
}
