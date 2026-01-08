using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class SFB_ExportMaterialEditor : EditorWindow
{
    public static SFB_ExportMaterial exporterObject;     // The scriptable object that holds data
    public Vector2 scrolling;                            // Holds scrolling data for window
    public static int materialCounter = 1;               // For keeping track of naming increments
    public static float progress = 0.0f;

    // ---------------------------
    // Window / Asset Creation
    // ---------------------------

    [MenuItem("Window/Substance Mass Exporter/Create Exporter Object")]
    public static void CreateExporterObject()
    {
        if (!Directory.Exists("Assets/SFBayStudios/Substance Mass Exporter/Export Texture Objects/"))
        {
            Directory.CreateDirectory("Assets/SFBayStudios/Substance Mass Exporter/Export Texture Objects/");
        }

        exporterObject = CreateObject("Assets/SFBayStudios/Substance Mass Exporter/Export Texture Objects/SFB Texture Exporter");
        AssetDatabase.SaveAssets();
        Init();
    }

    static SFB_ExportMaterial CreateObject(string newFile)
    {
        SFB_ExportMaterial asset = ScriptableObject.CreateInstance<SFB_ExportMaterial>();
        int x = 1;

        while (x < 999)
        {
            if (!File.Exists("Assets/SFBayStudios/Substance Mass Exporter/Export Texture Objects/SFB Texture Exporter " + x + ".asset"))
            {
                AssetDatabase.CreateAsset(asset, "Assets/SFBayStudios/Substance Mass Exporter/Export Texture Objects/SFB Texture Exporter " + x + ".asset");
                return asset;
            }
            x++;
        }

        Debug.LogError("Oops! You've made 999 objects with this name already...maybe you should rename some? Or is something else going on??");
        return null;
    }

    [MenuItem("Window/Substance Mass Exporter/Load Texture Exporter Window")]
    static void Init()
    {
        SFB_ExportMaterialEditor window = (SFB_ExportMaterialEditor)EditorWindow.GetWindow(typeof(SFB_ExportMaterialEditor));
        window.Show();
    }

    // ---------------------------
    // Unity Update
    // ---------------------------

    void Update()
    {
        if (exporterObject)
        {
            EditorUtility.SetDirty(exporterObject);
            EditorUtility.SetDirty(this);
        }

#if SFB_SUBSTANCE
        // Substance cache clear loop (only if Substance types exist)
        if (exporterObject)
        {
            for (int i = 0; i < exporterObject.materials.Count; i++)
            {
                if (!exporterObject.materials[i].loadingCacheClear)
                {
                    Debug.Log(exporterObject.materials[i].materialName + " isn't cleared");
                    ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[i].substancePath);
                    if (material == null)
                    {
                        exporterObject.materials[i].loadingCacheClear = true;
                        continue;
                    }

                    material.cacheSize = ProceduralCacheSize.None;
                    material.ClearCache();

                    if (!material.isProcessing)
                    {
                        Debug.Log(exporterObject.materials[i].materialName + " Done!");
                        exporterObject.materials[i].loadingCacheClear = true;
                    }
                    else
                    {
                        Debug.Log(exporterObject.materials[i].materialName + " processing...");
                    }
                }
            }
        }
#endif
    }

    // ---------------------------
    // GUI
    // ---------------------------

    void OnGUI()
    {
        GUIStyle saveStyle = new GUIStyle(GUI.skin.label);

#if !SFB_SUBSTANCE
        EditorGUILayout.HelpBox(
            "Unity의 내장 Substance(ProceduralMaterial) 지원이 제거되어 현재 환경에서는 Substance 전용 기능이 비활성화되어 있습니다.\n\n" +
            "Substance 기능을 계속 쓰려면:\n" +
            "1) Asset Store에서 'Substance 3D for Unity' 설치\n" +
            "2) Project Settings > Player > Scripting Define Symbols에 'SFB_SUBSTANCE' 추가",
            MessageType.Warning
        );
#endif

        scrolling = GUILayout.BeginScrollView(scrolling);

        exporterObject = EditorGUILayout.ObjectField(
            "Exporter Object:",
            exporterObject,
            typeof(SFB_ExportMaterial),
            false
        ) as SFB_ExportMaterial;

        if (!exporterObject)
        {
            EditorGUILayout.HelpBox(
                "Exporter Object를 선택하거나 메뉴에서 새로 생성하세요.\nWindow/Substance Mass Exporter/Create Exporter Object",
                MessageType.Info
            );

            GUILayout.EndScrollView();
            return;
        }

        if (string.IsNullOrEmpty(exporterObject.groupName))
            exporterObject.groupName = "Group Name";

        exporterObject.setNormalMapMode = EditorGUILayout.Toggle("Set Normal Map Mode?", exporterObject.setNormalMapMode);
        exporterObject.createMaterials = EditorGUILayout.Toggle("Create Materials?", exporterObject.createMaterials);
        exporterObject.convertToPNG = EditorGUILayout.Toggle("Convert to PNG?", exporterObject.convertToPNG);

        EditorGUILayout.Space();

        if (GUILayout.Button("EditorUtility.UnloadUnusedAssets()"))
        {
#if SFB_SUBSTANCE
            for (int i = 0; i < exporterObject.materials.Count; i++)
            {
                ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[i].substancePath);
                if (material == null) continue;

                Debug.Log("Before: " + material.cacheSize);
                material.cacheSize = ProceduralCacheSize.None;
                material.ClearCache();
                Debug.Log("After: " + material.cacheSize);
            }
#endif
            EditorUtility.UnloadUnusedAssets();
        }

        exporterObject.groupName = EditorGUILayout.TextField("Group Name:", exporterObject.groupName);
        EditorGUILayout.Space();

        // ---- Materials Foldout ----
        exporterObject.showMaterials = EditorGUILayout.Foldout(
            exporterObject.showMaterials,
            "Selected Materials (" + exporterObject.materials.Count + ")"
        );

        if (exporterObject.showMaterials)
        {
            EditorGUI.indentLevel++;

#if SFB_SUBSTANCE
            // Note: SFB_ExportMaterial.cs에서 newMaterial이 Material로 바뀌어 있어도,
            // ProceduralMaterial은 Material을 상속하므로(플러그인 제공 시) 캐스팅으로 처리 가능.
            Material picked = EditorGUILayout.ObjectField(
                "Add Procedural Material:",
                exporterObject.newMaterial,
                typeof(Material),
                false
            ) as Material;

            // 사용자가 뭔가 넣었으면 처리
            if (picked != null)
            {
                // ProceduralMaterial로 캐스팅(플러그인이 제공하는 타입일 때만 성공)
                ProceduralMaterial pm = picked as ProceduralMaterial;
                if (pm == null)
                {
                    EditorUtility.DisplayDialog("Invalid", "Substance ProceduralMaterial이 아닙니다. .sbsar에서 생성된 Material을 선택하세요.", "OK");
                }
                else
                {
                    string newPath = AssetDatabase.GetAssetPath(pm.GetInstanceID());
                    SubstanceImporter newImporter = AssetImporter.GetAtPath(newPath) as SubstanceImporter;

                    if (newImporter == null)
                    {
                        EditorUtility.DisplayDialog("Importer Missing", "SubstanceImporter를 찾을 수 없습니다. 플러그인/임포터 상태를 확인하세요.", "OK");
                    }
                    else
                    {
                        if (!IsInMaterialList(pm))
                        {
                            bool generateAllOutputs = newImporter.GetGenerateAllOutputs(pm);
                            SFB_MaterialExports newMaterial = new SFB_MaterialExports(pm.name, newPath, generateAllOutputs);
                            exporterObject.materials.Add(newMaterial);
                        }

                        LoadTextureNames_Substance();
                        pm.cacheSize = ProceduralCacheSize.None;
                        pm.ClearCache();
                    }
                }

                exporterObject.newMaterial = null;
            }
#else
            EditorGUILayout.HelpBox("Substance 플러그인이 없어 Procedural Material 추가 기능이 비활성화되어 있습니다.", MessageType.Info);
            // 안전하게 입력값은 비우기
            exporterObject.newMaterial = null;
#endif

            // List UI
            for (int i = 0; i < exporterObject.materials.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(exporterObject.materials[i].materialName);

#if SFB_SUBSTANCE
                if (!exporterObject.materials[i].generateAllOutputs)
                {
                    if (GUILayout.Button("Set Generate All Outputs"))
                    {
                        SubstanceImporter newImporter = AssetImporter.GetAtPath(exporterObject.materials[i].substancePath) as SubstanceImporter;
                        ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[i].substancePath);

                        if (newImporter != null && material != null)
                        {
                            newImporter.SetGenerateAllOutputs(material, true);
                            exporterObject.materials[i].generateAllOutputs = true;

                            material.cacheSize = ProceduralCacheSize.None;
                            material.ClearCache();

                            LoadTextureNames_Substance();
                        }
                    }
                }
#endif

                if (GUILayout.Button("Reload Outputs"))
                {
#if SFB_SUBSTANCE
                    LoadTextureNames_Substance();
#else
                    EditorUtility.DisplayDialog("Disabled", "Substance 플러그인이 없어 Outputs를 불러올 수 없습니다.", "OK");
#endif
                }

                if (GUILayout.Button("Remove"))
                {
                    exporterObject.materials.RemoveAt(i);
                }

                EditorGUILayout.EndHorizontal();
            }

#if SFB_SUBSTANCE
            if (!GenerateAllOutputs_Substance())
            {
                EditorGUILayout.HelpBox(
                    "Warning: Not all materials have \"Generate All Outputs\" selected. This may reduce texture options.",
                    MessageType.Warning
                );

                if (GUILayout.Button("Set Generate All Outputs on All Materials"))
                    SetGenerateAllOutputs_Substance();
            }
#endif

            EditorGUI.indentLevel--;
        }

        // ---- Texture Outputs Foldout ----
        if (exporterObject.materials.Count > 0)
        {
            exporterObject.showTextures = EditorGUILayout.Foldout(
                exporterObject.showTextures,
                "Texture Outputs (" + TextureOutputCount() + " of " + exporterObject.textureNames.Count + ")"
            );

            if (exporterObject.showTextures)
            {
                EditorGUI.indentLevel++;
                for (int t = 0; t < exporterObject.textureNames.Count; t++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorPrefs.SetBool(
                        exporterObject.textureNames[t] + "_SaveOnExport",
                        EditorGUILayout.Toggle(EditorPrefs.GetBool(exporterObject.textureNames[t] + "_SaveOnExport"), GUILayout.Width(100))
                    );
                    EditorGUILayout.LabelField(exporterObject.textureNames[t], saveStyle);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }

            exporterObject.showReuse = EditorGUILayout.Foldout(
                exporterObject.showReuse,
                "Reuse Textures (" + ReuseCount() + " of " + exporterObject.textureNames.Count + ")"
            );

            if (exporterObject.showReuse)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox(
                    "Any texture selected here will, if possible, reuse the first exported texture from this material.",
                    MessageType.None
                );

                for (int t = 0; t < exporterObject.textureNames.Count; t++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorPrefs.SetBool(
                        exporterObject.textureNames[t] + "_ReuseFirst",
                        EditorGUILayout.Toggle(EditorPrefs.GetBool(exporterObject.textureNames[t] + "_ReuseFirst"), GUILayout.Width(100))
                    );
                    EditorGUILayout.LabelField(exporterObject.textureNames[t], saveStyle);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }

            if (TextureOutputCount() > 0)
            {
                if (GUILayout.Button("Export Textures"))
                {
#if SFB_SUBSTANCE
                    ExportTextures_Substance();
#else
                    EditorUtility.DisplayDialog("Disabled", "Substance 플러그인이 없어 Export를 실행할 수 없습니다.", "OK");
#endif
                }
            }
        }

        GUILayout.EndScrollView();
    }

    // ---------------------------
    // Menu: Include Selected Materials
    // ---------------------------

    [MenuItem("Window/Substance Mass Exporter/Include Selected Material(s) %#e")]
    static void SelectProceduralMaterials()
    {
#if SFB_SUBSTANCE
        if (!exporterObject) return;

        foreach (Object selectedObject in Selection.objects)
        {
            bool generateAllOutputs = false;
            string newPath = "";
            SubstanceImporter newImporter;

            if (selectedObject.GetType() == typeof(SubstanceArchive))
            {
                newPath = AssetDatabase.GetAssetPath(selectedObject.GetInstanceID());
                newImporter = AssetImporter.GetAtPath(newPath) as SubstanceImporter;

                if (newImporter == null) continue;

                ProceduralMaterial[] newMaterials = newImporter.GetMaterials();
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    if (!IsInMaterialList(newMaterials[i]))
                    {
                        generateAllOutputs = newImporter.GetGenerateAllOutputs(newMaterials[i]);
                        SFB_MaterialExports newMaterial = new SFB_MaterialExports(newMaterials[i].name, newPath, generateAllOutputs);
                        exporterObject.materials.Add(newMaterial);
                        newMaterials[i].cacheSize = ProceduralCacheSize.None;
                    }
                    newMaterials[i].cacheSize = ProceduralCacheSize.None;
                    newMaterials[i].ClearCache();
                }
            }

            if (selectedObject.GetType() == typeof(ProceduralMaterial))
            {
                newPath = AssetDatabase.GetAssetPath(selectedObject.GetInstanceID());
                newImporter = AssetImporter.GetAtPath(newPath) as SubstanceImporter;

                if (newImporter == null) continue;

                ProceduralMaterial pm = selectedObject as ProceduralMaterial;
                if (!IsInMaterialList(pm))
                {
                    generateAllOutputs = newImporter.GetGenerateAllOutputs(pm);
                    SFB_MaterialExports newMaterial = new SFB_MaterialExports(selectedObject.name, newPath, generateAllOutputs);
                    exporterObject.materials.Add(newMaterial);

                    pm.cacheSize = ProceduralCacheSize.None;
                    pm.ClearCache();
                }
            }
        }

        LoadTextureNames_Substance();
        Init();
        EditorUtility.UnloadUnusedAssets();
#else
        EditorUtility.DisplayDialog("Disabled", "Substance 플러그인이 없어 이 기능이 비활성화되어 있습니다.", "OK");
#endif
    }

    // ==========================================================
    // ✅ ALWAYS-AVAILABLE WRAPPERS (CS0103 방지: 항상 존재)
    // ==========================================================

    static int TextureOutputCount()
    {
#if SFB_SUBSTANCE
        return TextureOutputCount_Substance();
#else
        return 0;
#endif
    }

    static int ReuseCount()
    {
#if SFB_SUBSTANCE
        return ReuseCount_Substance();
#else
        return 0;
#endif
    }

#if SFB_SUBSTANCE
    // ==========================================================
    // Substance-only implementations
    // ==========================================================

    static int TextureOutputCount_Substance()
    {
        int count = 0;
        for (int t = 0; t < exporterObject.textureNames.Count; t++)
        {
            if (EditorPrefs.GetBool(exporterObject.textureNames[t] + "_SaveOnExport"))
                count++;
        }
        return count;
    }

    static int ReuseCount_Substance()
    {
        int count = 0;
        for (int t = 0; t < exporterObject.textureNames.Count; t++)
        {
            if (EditorPrefs.GetBool(exporterObject.textureNames[t] + "_ReuseFirst"))
                count++;
        }
        return count;
    }

    static bool IsInMaterialList(ProceduralMaterial value)
    {
        for (int i = 0; i < exporterObject.materials.Count; i++)
        {
            if (exporterObject.materials[i].materialName == value.name)
                return true;
        }
        return false;
    }

    static bool IsInTextureList_Substance(string textureName)
    {
        for (int i = 0; i < exporterObject.textureNames.Count; i++)
        {
            if (exporterObject.textureNames[i] == textureName)
                return true;
        }
        return false;
    }

    static void LoadTextureNames_Substance()
    {
        exporterObject.textureNames.Clear();

        for (int i = 0; i < exporterObject.materials.Count; i++)
        {
            ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[i].substancePath);
            if (material == null) continue;

            Texture[] textures = material.GetGeneratedTextures();
            foreach (Texture texture in textures)
            {
                string[] nameArray = texture.name.Split("_"[0]);
                string typeName = nameArray[nameArray.Length - 1];

                if (!IsInTextureList_Substance(typeName))
                    exporterObject.textureNames.Add(typeName);
            }

            ProceduralMaterial.StopRebuilds();
            EditorUtility.UnloadUnusedAssets();
        }
    }

    static bool GenerateAllOutputs_Substance()
    {
        for (int i = 0; i < exporterObject.materials.Count; i++)
        {
            if (!exporterObject.materials[i].generateAllOutputs)
                return false;
        }
        return true;
    }

    static void SetGenerateAllOutputs_Substance()
    {
        for (int i = 0; i < exporterObject.materials.Count; i++)
        {
            progress = ((float)i / (float)exporterObject.materials.Count);
            EditorUtility.DisplayProgressBar(
                "Setting Generate All Outputs...",
                "Setting Value for " + exporterObject.materials[i].materialName,
                progress
            );

            SubstanceImporter newImporter = AssetImporter.GetAtPath(exporterObject.materials[i].substancePath) as SubstanceImporter;
            ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[i].substancePath);

            if (newImporter != null && material != null)
            {
                newImporter.SetGenerateAllOutputs(material, true);
                exporterObject.materials[i].generateAllOutputs = true;

                material.cacheSize = ProceduralCacheSize.None;
                material.ClearCache();
            }
        }

        LoadTextureNames_Substance();
        EditorUtility.ClearProgressBar();
    }

    static void ExportTextures_Substance()
    {
        string pBarString = "Starting Texture Exporting Process";
        progress = 0.0f;
        EditorUtility.DisplayProgressBar("Texture Exporter", pBarString, progress);

        for (int i = 0; i < exporterObject.materials.Count; i++)
        {
            pBarString = "(Material " + (i + 1) + " of " + exporterObject.materials.Count + ") Starting " + exporterObject.materials[i].materialName;
            if (i != 0)
                progress = (float)i / (float)exporterObject.materials.Count;

            EditorUtility.DisplayProgressBar("Texture Exporter", pBarString, progress);

            string materialName = GetMaterialName_Substance(i);
            ExportSubstance_Substance(i, materialName);
            RenameAndRemove_Substance(i, materialName, exporterObject.materials[i].materialName);

            if (exporterObject.createMaterials)
                CreateMaterials_Substance(i, materialName, exporterObject.materials[i].materialName);
        }

        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }

    static string GetMaterialName_Substance(int id)
    {
        if (!Directory.Exists("Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName))
        {
            Directory.CreateDirectory("Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName);
        }

        ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[id].substancePath);
        if (material == null) return "Material";

        string materialName = material.name;
        int x = 1;

        while (x < 999)
        {
            materialCounter = x;
            string checkName = materialName;
            if (x > 1) checkName = materialName + "_" + x;

            if (!Directory.Exists("Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + checkName))
            {
                Directory.CreateDirectory("Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + checkName);
                return checkName;
            }
            x++;
        }

        return materialName;
    }

    static void ExportSubstance_Substance(int id, string materialName)
    {
        SubstanceImporter newImporter = AssetImporter.GetAtPath(exporterObject.materials[id].substancePath) as SubstanceImporter;
        if (newImporter == null) return;

        string path = "Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + materialName + "/";
        ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[id].substancePath);
        if (material == null) return;

        material.cacheSize = ProceduralCacheSize.None;
        material.ClearCache();

        newImporter.ExportBitmaps(material, path, true);
    }

    static void SetTextureImporterFormat_Substance(Texture2D texture, bool isReadable)
    {
        if (texture == null) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Default;
            tImporter.isReadable = isReadable;
            AssetDatabase.ImportAsset(assetPath);
        }
    }

    static void ConvertToPNG_Substance(string filepath)
    {
        EditorUtility.DisplayProgressBar("Converting To PNG", filepath, progress);
        AssetDatabase.ImportAsset(filepath);

        Texture originalTexture = AssetDatabase.LoadAssetAtPath(filepath, typeof(Texture)) as Texture;
        if (originalTexture == null) return;

        string newFilePath = filepath.Replace(".tga", ".png");

        Texture2D savedTexture = originalTexture as Texture2D;
        if (savedTexture == null) return;

        SetTextureImporterFormat_Substance(savedTexture, true);

        Texture2D newTexture = new Texture2D(savedTexture.width, savedTexture.height, TextureFormat.ARGB32, false);
        newTexture.SetPixels(0, 0, savedTexture.width, savedTexture.height, savedTexture.GetPixels());
        newTexture.Apply();

        byte[] bytes = newTexture.EncodeToPNG();
        File.WriteAllBytes(newFilePath, bytes);

        AssetDatabase.ImportAsset(newFilePath);
        AssetDatabase.DeleteAsset(filepath);
    }

    static void RenameAndRemove_Substance(int id, string materialName, string originalName)
    {
        string path = "Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + materialName + "/";
        ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[id].substancePath);
        if (material == null) return;

        Texture[] textures = material.GetGeneratedTextures();
        foreach (Texture texture in textures)
        {
            string[] nameArray = texture.name.Split("_"[0]);
            string typeName = nameArray[nameArray.Length - 1];
            string newTexturePath = path + texture.name + ".tga";

            bool canRemove = false;
            if (EditorPrefs.GetBool(typeName + "_ReuseFirst"))
            {
                string originalPath = "Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + originalName + "/";
                if (File.Exists(originalPath + texture.name + ".tga") && materialCounter != 1)
                    canRemove = true;
            }

            bool deleted = false;
            if (!EditorPrefs.GetBool(typeName + "_SaveOnExport") || canRemove)
            {
                if (File.Exists(newTexturePath))
                    File.Delete(newTexturePath);
                deleted = true;
            }
            else if (materialCounter > 1)
            {
                string src = path + texture.name + ".tga";
                string dst = path + texture.name + "_" + materialCounter + ".tga";
                if (File.Exists(src))
                {
                    File.Move(src, dst);
                    newTexturePath = dst;
                }
            }

            if (!deleted && exporterObject.convertToPNG && File.Exists(newTexturePath))
                ConvertToPNG_Substance(newTexturePath);

            // Normal map mode (Substance procedural output type)
            if (exporterObject.setNormalMapMode && typeName == "normal")
            {
                string pngPath = newTexturePath.Replace(".tga", ".png");
                string applyPath = File.Exists(newTexturePath) ? newTexturePath : (File.Exists(pngPath) ? pngPath : null);
                if (applyPath == null) continue;

                EditorUtility.DisplayProgressBar("Texture Exporter", "Setting Normal Map Mode", progress);
                AssetDatabase.ImportAsset(applyPath);

                ProceduralTexture proceduralTexture = texture as ProceduralTexture;
                if (proceduralTexture != null && proceduralTexture.GetProceduralOutputType() == ProceduralOutputType.Normal)
                {
                    TextureImporter textureImporter = AssetImporter.GetAtPath(applyPath) as TextureImporter;
                    if (textureImporter != null)
                    {
                        textureImporter.textureType = TextureImporterType.NormalMap;
                        AssetDatabase.ImportAsset(applyPath);
                    }
                }
            }
        }

        material.cacheSize = ProceduralCacheSize.None;
        material.ClearCache();
    }

    static void CreateMaterials_Substance(int id, string materialName, string originalName)
    {
        string path = "Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/" + materialName + ".mat";
        ProceduralMaterial material = AssetDatabase.LoadAssetAtPath<ProceduralMaterial>(exporterObject.materials[id].substancePath);
        if (material == null) return;

        Material newMaterial = new Material(material.shader);
        newMaterial.CopyPropertiesFromMaterial(material);
        AssetDatabase.CreateAsset(newMaterial, path);
        AssetDatabase.Refresh();

        int propertyCount = ShaderUtil.GetPropertyCount(newMaterial.shader);
        for (int i = 0; i < propertyCount; i++)
        {
            if (ShaderUtil.GetPropertyType(newMaterial.shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
            {
                string propertyName = ShaderUtil.GetPropertyName(newMaterial.shader, i);

                if (newMaterial.GetTexture(propertyName) == null)
                    continue;

                string[] nameArray = newMaterial.GetTexture(propertyName).name.Split("_"[0]);
                string typeName = nameArray[nameArray.Length - 1];

                bool foundTexture = false;

                if (EditorPrefs.GetBool(typeName + "_ReuseFirst") && materialCounter != 1)
                {
                    string originalPath = "Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + originalName + "/";
                    string fileName = originalPath + originalName + "_" + typeName + ".tga";
                    if (!File.Exists(fileName))
                        fileName = originalPath + originalName + "_" + typeName + ".png";

                    if (File.Exists(fileName))
                    {
                        Texture originalTexture = AssetDatabase.LoadAssetAtPath(fileName, typeof(Texture)) as Texture;
                        newMaterial.SetTexture(propertyName, originalTexture);
                        foundTexture = true;
                    }
                }

                if (!foundTexture)
                {
                    string texturePath = "Assets/SFBayStudios/Exported Materials/" + exporterObject.groupName + "/tex_" + materialName + "/";

                    string filePath = texturePath + originalName + "_" + typeName + ".tga";
                    if (materialCounter > 1)
                    {
                        filePath = texturePath + originalName + "_" + typeName + "_" + materialCounter + ".tga";
                        if (!File.Exists(filePath))
                            filePath = texturePath + originalName + "_" + typeName + "_" + materialCounter + ".png";
                    }
                    else
                    {
                        if (!File.Exists(filePath))
                            filePath = texturePath + originalName + "_" + typeName + ".png";
                    }

                    if (File.Exists(filePath))
                    {
                        Texture newTexture = AssetDatabase.LoadAssetAtPath(filePath, typeof(Texture)) as Texture;
                        newMaterial.SetTexture(propertyName, newTexture);
                        foundTexture = true;
                    }
                }

                if (!foundTexture)
                {
                    newMaterial.SetTexture(propertyName, null);
                    if (propertyName == "_EmissionMap")
                        newMaterial.SetColor("_EmissionColor", Color.black);
                }
            }
        }

        material.cacheSize = ProceduralCacheSize.None;
        material.ClearCache();
    }
#endif // SFB_SUBSTANCE
}
