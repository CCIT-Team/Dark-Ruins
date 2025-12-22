using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_slime : MonoBehaviour, ITrap
{
    private bool isSlowing = false;

    void ITrap.ActivateTrap()
    {
        isSlowing = true;
    }
    void ITrap.DeactivateTrap()
    {
        isSlowing = false;
    }

    void Slow()
    {
        //플레이어 이동속도 감소
    }
    void defaul()
    {
        //플레이어 이동속도 복구
    }

    void Update()
    {
        if (isSlowing)
        {
            Slow();
        }
        else
        {
            defaul();
        }
    }
}
