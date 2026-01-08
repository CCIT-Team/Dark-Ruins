using System.Collections;
using System.Collections.Generic;
using LYS_Work;
using UnityEngine;

public class UVPuzzleManager : MonoBehaviour
{
    [SerializeField]
    List<UVPuzzle> targets;
    [SerializeField]
    int cnt=0;
    bool isCompleted=false;
    [SerializeField]
    List<ItemPuzzleCompletedDetector> activationTargets;
    void Start()
    {
        foreach(var i in targets)
        {
            i.completedCallback+=Completed;
        }
    }

    public void Reset()
    {
        foreach(var target in targets)
        {
            target.Reset();
        }
        isCompleted=false;
        cnt=0;
    }

    private int Check(string name)
    {
        for(int i = 0; i < targets.Count; i++)
        {
            if(targets[i].gameObject.name == name)
            {
                return i;
            }
        }

        return -1;
    }

    private void Completed(string name)
    {
        if(isCompleted)
        {
            return;
        }   

        int idx = Check(name);

        if(idx < 0)
        {
            return;
        }

        if(idx > cnt )
        {
            Reset();
            return;
        }

        cnt++;
        
        if(cnt>=targets.Count)
        {
            foreach(var target in activationTargets)
            {
                target.DetectPuzzleComplete(true);
            }
        }
    }
}
