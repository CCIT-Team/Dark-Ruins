using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trap_trigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] [Tooltip("작동할 함정")] private GameObject trapObject;

    //private bool ismoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) /*&& !ismoving*/)
        {
            trapObject.GetComponent<ITrap>().ActivateTrap();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) /*&& !ismoving*/)
        {
            trapObject.GetComponent<ITrap>().DeactivateTrap();
        }
    }
}