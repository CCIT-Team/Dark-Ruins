using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class trap_spike : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    private float damage = 10f; //이후 플레이어 스탯 나오면 조정 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            //데미지 함수
            
            Debug.Log("아프다 아파");
        }
    }
}
