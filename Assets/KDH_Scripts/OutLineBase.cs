using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineBase : MonoBehaviour,IDrawOutLine
{
    public Outline OutLine;
    private void Start()
    {
        OutLine = GetComponent<Outline>();

        if (OutLine == null)
        {
            OutLine = gameObject.AddComponent<Outline>();
            OutLine.OutlineMode = Outline.Mode.OutlineAll;
            OutLine.OutlineColor = new Color(1.5f, 0.2f, 1.5f, 1.0f);
            OutLine.OutlineWidth = 4f;
        }
        OutLine.enabled = false;
    }
    public void OnFocused()
    {
#if UNITY_EDITOR
        Debug.Log($"{name} ���̷� ���õ�");
#endif
        OutLine.enabled = true;
    }
    public void OutFocused()
    {
        OutLine.enabled = false;
    }
}
