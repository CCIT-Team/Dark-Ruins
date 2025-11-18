using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trap_trigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [Tooltip("직접적으로 튀어나올 함정입니다.")]public GameObject spike;

    private float moveDistance = 0.5f; //함정이 올라올 거리

    private bool shouldmoving = false;

    //업데이트 if 쓰는 코드

    private float smoothTime = 0.3f; //부드럽게 올라오는 시간
    private Vector3 targetPosition;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        targetPosition = spike.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetPosition = spike.transform.position + new Vector3(0, moveDistance, 0);
            shouldmoving = true;
            Debug.Log("함정 카드 발동!");
        }
    }

    private void Update()
    {
        if(shouldmoving)
        {
            spike.transform.position = Vector3.SmoothDamp(spike.transform.position, targetPosition, ref currentVelocity, smoothTime);

            if (Vector3.Distance(spike.transform.position, targetPosition) < 0.01f)
            {
                spike.transform.position = targetPosition;
                shouldmoving = false;
            }
        }
    }

    //코루틴쓰는 코드

    //private float moveDurationUp = 0.2f; //함정이 올라오는 시간
    //private float moveDurationDown = 0.7f; //함정이 내려가는 시간

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(targetTag) && !ismoving)
    //    {
    //        //spike.SetActive(true);
    //        MoveUp();
    //        Debug.Log("함정 카드 발동!");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(targetTag) && !ismoving)
    //    {
    //        MoveDown();
    //        //spike.SetActive(false);
    //        Debug.Log("함정 카드 취소!");
    //    }
    //}


    //private void MoveUp()
    //{
    //    ismoving = true;

    //    Vector3 startPos = spike.transform.position;
    //    Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);

    //    float movingTime = 0f;

    //    while (movingTime < moveDurationUp)
    //    {
    //        movingTime += Time.deltaTime;
    //        spike.transform.position = Vector3.Lerp(startPos, endPos, (movingTime / moveDurationUp));
    //    }
    //    spike.transform.position = endPos;
    //    ismoving = false;
    //}

    //private void MoveDown()
    //{
    //    ismoving = true;

    //    Vector3 startPos = spike.transform.position;
    //    Vector3 endPos = startPos - new Vector3(0, moveDistance, 0);

    //    float movingTime = 0f;

    //    while (movingTime < moveDurationDown)
    //    {
    //        movingTime += Time.deltaTime;

    //        float t = movingTime / moveDurationDown;
    //        t = Mathf.SmoothStep(0, 1, t); 

    //        spike.transform.position = Vector3.Lerp(startPos, endPos, t);
    //    }
    //    spike.transform.position = endPos;
    //    ismoving = false;
    //}
}