using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_arrow : MonoBehaviour, ITrap
{
    private bool once = true;

    GameObject ob;

    public void ActivateTrap()
    {
        if(once)
        {
            Managers_YGU.Sound.Play3D("Trap_Arrow_Activate", this.transform.position);
            for(int i=0; i<transform.childCount; i++)
            {
                ob = transform.GetChild(i).gameObject;
                ob.GetComponent<arrow_move>().ShootArrow();
            }
            once = false;
        }
    }

    public void DeactivateTrap()
    {
        //함정 해제 시 동작 없음
    }
}
