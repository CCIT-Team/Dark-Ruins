using System.Collections;
using System.Collections.Generic;
using CutSceneEngine;
using LYS_Work.Controller;
using LYS_Work.Manager;
using LYS_Work.Token;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class testscript : MonoBehaviour
{
    

    public string addr;

    public TextMeshProUGUI tmpp;

    [ContextMenu("f")]
    public void fun()
    {
        Managers_YGU.Resource.LoadAsync<TextAsset>(addr,startScene);



    }

    public void startScene(TextAsset text)
    {
        CutSceneEngine.CutSceneInterpreter inter = gameObject.GetOrAddComponent<CutSceneInterpreter>();

    }


    private IEnumerator TypeWriting(float waitTime, TextMeshProUGUI tmp, string text)
        {
            float secPerChar = waitTime / text.Length;

            foreach(var c in text)
            {
                tmp.text+=c;
                yield return new WaitForSeconds(secPerChar);
            }
        }

}
