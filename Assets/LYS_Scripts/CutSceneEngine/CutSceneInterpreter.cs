using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

        public void InitInterpreter()
        {
            _loadedObjects.Clear();
            _sequances.Clear();
        }

        public bool LoadCutSceneScript(string path)
        {
            if(string.IsNullOrEmpty(path) || File.Exists(path)==false)
            {
                return false;
            }

            string rawString="";
            try
            {
                rawString = File.ReadAllText(path);
            }
            catch(Exception e)
            {
                Debug.LogError($"{e.GetType()} : 파일 로드 실패");
                return false;
            }

            CutScene loadedScript = Newtonsoft.Json.JsonConvert.DeserializeObject<CutScene>(rawString);

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

                _loadedObjects.Add(item,go);
            }

            _sequances = loadedScript.Sequances;

            return true;
        } 

        public bool StartCutScene()
        {
            if(_sequances is null || _sequances.Count <= 0)
            {
                return false;
            }

            StartCoroutine(InternalStart());
            return true;
        }

        private IEnumerator InternalStart()
        {
            foreach(CutSceneSequanceBlock seq in _sequances)
            {
                foreach(CutSceneTaskBlock task in seq.Tasks)
                {
                    switch(task.type)
                    {
                        case 0:
                            StartCoroutine(TestFun(task.time,_loadedObjects[task.target]));
                            break;
                    }
                }

                yield return new WaitForSeconds(seq.HoldFor);
            }
        }

        private IEnumerator TestFun(float waitTime,GameObject go)
        {
            yield return new WaitForSeconds(waitTime);
            go.transform.position *=2;
        }
    }
}