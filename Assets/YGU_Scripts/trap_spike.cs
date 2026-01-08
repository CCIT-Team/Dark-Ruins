using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class trap_spike : MonoBehaviour, ITrap
{
    private float moveDistance = 1f; //함정이 움직일 거리
    private float moveDurationUp = 0.1f; //함정이 올라오는 시간
    private float moveDurationDown = 0.5f; //함정이 내려가는 시간

    private bool ismoving = false;

    void ITrap.ActivateTrap()
    {
        if (ismoving == false)
        {
            StartCoroutine(MoveSpike());
        }
    }

    void ITrap.DeactivateTrap()
    {
        //StartCoroutine(MoveDown());
    }

    //Up Down 각각
    //private IEnumerator MoveUp()
    //{
    //    ismoving = true;

    //    Vector3 startPos = this.transform.position;
    //    Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);

    //    float movingTime = 0f;

    //    while (movingTime < moveDurationUp)
    //    {
    //        movingTime += Time.deltaTime;

    //        this.transform.position = Vector3.Lerp(startPos, endPos, (movingTime / moveDurationUp));

    //        yield return null;
    //    }

    //    this.transform.position = endPos;
    //    ismoving = false;
    //}

    //private IEnumerator MoveDown()
    //{
    //    ismoving = true;

    //    Vector3 startPos = this.transform.position;
    //    Vector3 endPos = startPos - new Vector3(0, moveDistance, 0);

    //    float movingTime = 0f;

    //    while (movingTime < moveDurationDown)
    //    {
    //        movingTime += Time.deltaTime;

    //        this.transform.position = Vector3.Lerp(startPos, endPos, movingTime / moveDurationDown);

    //        yield return null;
    //    }

    //    this.transform.position = endPos;
    //    ismoving = false;
    //}

    private IEnumerator MoveSpike()
    {
        ismoving = true;

        Vector3 startPos = this.transform.position;
        Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);

        StartCoroutine(Moving(startPos, endPos, moveDurationUp));

        yield return new WaitForSeconds(moveDurationUp + 2f);

        StartCoroutine(Moving(endPos, startPos, moveDurationDown));

        yield return new WaitForSeconds(moveDurationDown);

        ismoving = false;
    }

    private IEnumerator Moving(Vector3 startPos, Vector3 endPos, float moveDurationTime)
    {
        float movingTime = 0f;

        while (movingTime < moveDurationTime)
        {
            movingTime += Time.deltaTime;

            this.transform.position = Vector3.Lerp(startPos, endPos, (movingTime / moveDurationTime));

            yield return null;
        }
    }
}
