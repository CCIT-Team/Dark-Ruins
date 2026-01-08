using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_touchDamage : MonoBehaviour
{
    [Header("함정 대미지")]
    [SerializeField] private int TrapDamage = 10;
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
        {
            Debug.Log("플레이어가 아닌데?");
            return;
        }

        IDamageable_KSM DM = other.transform.GetComponent<IDamageable_KSM>();

        if (DM == null)
        {
            Debug.Log("피해를 못받는데?");
            return;
        }
        Debug.Log("아파아파아파");
        DM.OnDamaged(TrapDamage, this.transform, false);
        //DM.Trapdamaged(TrapDamage);
    }
}
