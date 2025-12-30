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
    private void Completed()
    {
        if(isCompleted)
        {
            return;
        }
        cnt++;
        
        if(cnt>=targets.Count)
        {
            foreach(var target in activationTargets)
            {
                target.DetectPuzzleComplete();
            }
        }
    }
}
