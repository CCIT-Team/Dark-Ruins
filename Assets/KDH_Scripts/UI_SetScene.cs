#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
//���� �̿ϼ� ���
public class UISetScene : EditorWindow
{
    private Component uiScene;
    private MethodInfo[] sceneMethods;
    private Vector2 scrollPos;
    private Dictionary<GameObject, bool> originalActiveStates;

    [MenuItem("Tools/UI���� ��ư���� ���� �Ѻ���")]
    public static void ShowWindow()
    {
        GetWindow<UISetScene>("UISetScene");
    }

    private void OnGUI()
    {
        GUILayout.Label("�� UI ����", EditorStyles.boldLabel);

        // ���� �� ���� ���� / �ǵ����� ��ư
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("���� UI ���� ����"))
        {
            SaveUIActiveStates();
        }

        GUI.enabled = originalActiveStates != null;
        if (GUILayout.Button("����� ���·� �ǵ�����"))
        {
            RestoreUIActiveStates();
        }
        GUI.enabled = true;
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (GUILayout.Button("UIScene ã�� / ���ε�"))
        {
            FindUIScene();
        }

        GUILayout.Space(10);

        // UIScene protected �޼��� ��ư ǥ��
        if (sceneMethods != null)
        {
            foreach (var method in sceneMethods)
            {
                if (GUILayout.Button(method.Name))
                {
                    InvokeUISceneMethod(method);
                }
            }
        }
    }
    private void SaveUIActiveStates()
    {
        if (uiScene == null)
        {
            Debug.LogWarning("UIScene�� �������� �ʾҽ��ϴ�.");
            return;
        }

        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            Debug.LogWarning("@UI_Root ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        originalActiveStates = new Dictionary<GameObject, bool>();

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            originalActiveStates[child.gameObject] = child.gameObject.activeSelf;
        }

        Debug.Log($"UI Ȱ��ȭ ���� {originalActiveStates.Count}�� ���� �Ϸ�");
    }

    private void RestoreUIActiveStates()
    {
        if (originalActiveStates == null)
        {
            Debug.LogWarning("����� UI ���°� �����ϴ�.");
            return;
        }

        foreach (var kvp in originalActiveStates)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        Debug.Log("����� Ȱ��ȭ ���·� ���� �Ϸ�!");
    }

    // Init ȣ�� (�ʿ��ϸ�)
    private void FindUIScene()
    {
        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            Debug.LogWarning("@UI_Root ������Ʈ�� ã�� �� �����ϴ�.");
            uiScene = null;
            sceneMethods = null;
            return;
        }

        uiScene = root.GetComponent<UIScene>();
        if (uiScene == null)
        {
            Debug.LogWarning("@UI_Root�� UIScene ������Ʈ�� �����ϴ�.");
            sceneMethods = null;
            return;
        }

        // �ʿ��� ���ε��� ����
        var uiType = uiScene.GetType();

        // GameObjects Enum ���ε�
        var bindObjectMethod = uiType.GetMethod("BindObject", BindingFlags.NonPublic | BindingFlags.Instance);
        if (bindObjectMethod != null)
        {
            var gameObjectsEnum = uiType.GetNestedType("GameObjects", BindingFlags.Public | BindingFlags.NonPublic);
            if (gameObjectsEnum != null)
                bindObjectMethod.Invoke(uiScene, new object[] { gameObjectsEnum });
        }

        // Buttons Enum ���ε�
        var bindButtonMethod = uiType.GetMethod("BindButton", BindingFlags.NonPublic | BindingFlags.Instance);
        if (bindButtonMethod != null)
        {
            var buttonsEnum = uiType.GetNestedType("Buttons", BindingFlags.Public | BindingFlags.NonPublic);
            if (buttonsEnum != null)
                bindButtonMethod.Invoke(uiScene, new object[] { buttonsEnum });
        }

        // Texts Enum ���ε�
        var bindTextMethod = uiType.GetMethod("BindText", BindingFlags.NonPublic | BindingFlags.Instance);
        if (bindTextMethod != null)
        {
            var textsEnum = uiType.GetNestedType("Texts", BindingFlags.Public | BindingFlags.NonPublic);
            if (textsEnum != null)
                bindTextMethod.Invoke(uiScene, new object[] { textsEnum });
        }


        // ���⼭ protected �޼��常 �������� Finalize/MemberwiseClone ����
        sceneMethods = uiScene.GetType()
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => m.IsFamily                  // protected
                        && m.DeclaringType == uiScene.GetType()) // �θ� Ŭ������ System.Object ����
            .ToArray();



        Debug.Log($"UIScene [{uiScene.GetType().Name}] ã��. (protected �޼��� {sceneMethods.Length}��)");
    }



    private void InvokeUISceneMethod(MethodInfo method)
    {
        try
        {
            var parameters = method.GetParameters();
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                // �⺻�� Ÿ�Կ� ���� �ӽ� �� ����
                if (parameters[i].ParameterType == typeof(int)) args[i] = 0;
                else if (parameters[i].ParameterType == typeof(float)) args[i] = 0f;
                else if (parameters[i].ParameterType == typeof(string)) args[i] = "";
                else args[i] = null;
            }

            method.Invoke(uiScene, args);
            Debug.Log($"Protected �޼��� ȣ���: {method.Name}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"�޼��� ���� ����: {e.Message}");
        }
    }
}
#endif