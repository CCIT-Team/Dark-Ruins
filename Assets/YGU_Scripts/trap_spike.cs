using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class trap_spike : MonoBehaviour, ITrap
{
    private float moveDistance = 0.5f; //함정이 움직일 거리
    private float moveDurationUp = 0.2f; //함정이 올라오는 시간
    private float moveDurationDown = 0.5f; //함정이 내려가는 시간

    private bool ismoving = false;

    private float damage = 10f; //이후 플레이어 스탯 나오면 조정 

    void ITrap.ActivateTrap()
    {
        StartCoroutine(MoveUp());
    }

    void ITrap.DeactivateTrap()
    {
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveUp()
    {
        ismoving = true;

        Vector3 startPos = this.transform.position;
        Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);

        float movingTime = 0f;

        while (movingTime < moveDurationUp)
        {
            movingTime += Time.deltaTime;

            this.transform.position = Vector3.Lerp(startPos, endPos, (movingTime / moveDurationUp));

            yield return null;
        }

        this.transform.position = endPos;
        ismoving = false;
    }

    private IEnumerator MoveDown()
    {
        ismoving = true;

        Vector3 startPos = this.transform.position;
        Vector3 endPos = startPos - new Vector3(0, moveDistance, 0);

        float movingTime = 0f;

        while (movingTime < moveDurationDown)
        {
            movingTime += Time.deltaTime;

            this.transform.position = Vector3.Lerp(startPos, endPos, movingTime / moveDurationDown);

            yield return null;
        }

        this.transform.position = endPos;
        ismoving = false;
    }
}
