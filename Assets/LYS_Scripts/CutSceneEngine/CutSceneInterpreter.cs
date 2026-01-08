using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CutSceneEngine
{
    public class CutSceneTaskBlock
    {
        public string target{get;set;}
        public int type{get;set;}
        public string[] arg{get;set;}
        public float time;
    }
    public class CutSceneSequanceBlock
    {
        public float HoldFor{get;set;}
        public List<CutSceneTaskBlock> Tasks{get;set;}
    }
    public class CutScene
    {
        public string[] ToUse{get;set;}
        public List<CutSceneSequanceBlock> Sequances{get;set;}
    }

    public class CutSceneInterpreter : MonoBehaviour
    {
        private Dictionary<string, GameObject> _loadedObjects = new Dictionary<string, GameObject>();
        private List<CutSceneSequanceBlock> _sequances = new List<CutSceneSequanceBlock>(64);

        public void InitInterpreter(bool initObjects)
        {
            if(initObjects)
            {
                _loadedObjects.Clear();
            }
            _sequances.Clear();
        }

        public bool LoadCutSceneScript(string text)
        {
            // if(string.IsNullOrEmpty(path) || File.Exists(path)==false)
            // {
            //     return false;
            // }

            // string rawString="";
            // try
            // {
            //     rawString = File.ReadAllText(path);
            // }
            // catch(Exception e)
            // {
            //     Debug.LogError($"{e.GetType()} : 파일 로드 실패");
            //     return false;
            // }

            CutScene loadedScript = Newtonsoft.Json.JsonConvert.DeserializeObject<CutScene>(text);

            foreach(var item in loadedScript.ToUse)
            {
                if(string.IsNullOrEmpty(item))
                {
                    continue;
                }
                GameObject go = GameObject.Find(item);

                if(go is null)
                {
                    Debug.LogError($"지정한 오브젝트를 못찾았습니다. : {item}");
                    _loadedObjects.Clear();
                    return false;
                }

                _loadedObjects.TryAdd(item,go);
            }

            _sequances = loadedScript.Sequances;

            return true;
        } 

        public bool StartCutScene(Action endCallback)
        {
            if(_sequances is null || _sequances.Count <= 0)
            {
                return false;
            }
            StartCoroutine(InternalStart(endCallback));
            return true;
        }

        public IEnumerator CoStartCutScene(Action endCallback)
        {
            if(_sequances is null || _sequances.Count <= 0)
            {
                yield break;
            }

            yield return StartCoroutine(InternalStart(endCallback));
        }

        private IEnumerator InternalStart(Action endcallback)
        {
            foreach(CutSceneSequanceBlock seq in _sequances)
            {
                foreach(CutSceneTaskBlock task in seq.Tasks)
                {
                    switch(task.type)
                    {
                        case 0:
                            StartCoroutine(TypeWriting(task.time, _loadedObjects[task.target].GetComponent<TextMeshProUGUI>(), task.arg[0]));
                            break;
                        case 1:
                            StartCoroutine(ClearText(task.time,_loadedObjects[task.target].GetComponent<TextMeshProUGUI>() ));
                            break;
                        case 2:
                            StartCoroutine(ChangeImg(task.time,_loadedObjects[task.target], _loadedObjects[task.arg[0]]));
                            break;
                        case 3:
                            StartCoroutine(SetAnchoredPos(task.time, _loadedObjects[task.target].GetComponent<RectTransform>(), float.Parse(task.arg[0]),float.Parse(task.arg[1]),float.Parse(task.arg[2])));
                            break;
                    }
                }

                yield return new WaitForSeconds(seq.HoldFor);
            }

            endcallback?.Invoke();
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

        private IEnumerator TestFun(float waitTime,GameObject go)
        {
            yield return new WaitForSeconds(waitTime);
            go.transform.position *=2;
        }
        private IEnumerator ClearText(float waitTime, TextMeshProUGUI tmp)
        {
            yield return new WaitForSeconds(waitTime);
            tmp.text = "";
        }

        private IEnumerator ChangeImg(float waitTime, GameObject newObj, GameObject oldObj)
        {
            newObj?.SetActive(true);
            oldObj?.SetActive(false);
            yield return new WaitForSeconds(waitTime);
        }

        private IEnumerator SetAnchoredPos(float waitTime, RectTransform rt, float x, float y, float z)
        {
            rt.anchoredPosition = new Vector3(x,y,z);
            yield return new WaitForSeconds(waitTime);
        }
        
    }
}