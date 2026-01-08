using System.Collections;
using System.Collections.Generic;
using CutSceneEngine;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Ending : MonoBehaviour
{
    CutSceneInterpreter interp;
    // Start is called before the first frame update
    [SerializeField]
    GameObject background;
    Image img;
    void Start()
    {
        interp = gameObject.GetOrAddComponent<CutSceneInterpreter>();
        img = background.GetComponent<Image>();
        var col = img.color;
        col.a = 0;
        img.color = col;
    }

    [ContextMenu("end")]
    public void ShowEnding()
    {
        var col = img.color;
        col.a = 255;
        img.color = col;
        Managers_YGU.Resource.LoadAsync<TextAsset>("Escape",(TextAsset asset) =>
        {
            interp.LoadCutSceneScript(asset.text);
            StartCoroutine(CoEnding());
        });
    }

    private IEnumerator CoEnding()
    {
        yield return new WaitForSeconds(1);

        var routine = StartCoroutine(interp.CoStartCutScene(null));

        yield return routine;


        var col = img.color;
        col.a = 0;
        img.color = col;
    }
}
