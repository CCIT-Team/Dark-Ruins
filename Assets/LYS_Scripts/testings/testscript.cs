using System.Collections;
using System.Collections.Generic;
using LYS_Work.Controller;
using LYS_Work.Manager;
using LYS_Work.Token;
using Unity.VisualScripting;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public Camera cam;
    RotatablePuzzleManager cont;
    Token token;
    public int test;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            cont = fun().GetComponent<RotatablePuzzleManager>();
            cont.DoPuzzle(transform, test, ref token);
        }
        
        if(Input.GetKeyDown(KeyCode.Q) && cont is not null)
        {
            cont.EndPuzzle(token);
        }
    }
    public GameObject fun()
    {
        // 2. Raycast의 시작 위치와 방향 설정
        // 시작 위치: 카메라의 위치 (transform.position)
        Vector3 rayOrigin = cam.transform.position;
        // 방향: 카메라의 정면 방향 (transform.forward)
        Vector3 rayDirection = cam.transform.forward;

        // 3. RaycastHit 변수 선언 (충돌 정보를 저장할 변수)
        RaycastHit hit;

        // 4. Raycast 실행
        // Physics.Raycast(시작 위치, 방향, 충돌 정보, 최대 거리, 레이어 마스크)
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, float.PositiveInfinity))
        {
            // Raycast가 무언가와 충돌했을 때 실행되는 코드
            Debug.Log($"Raycast가 {hit.collider.name} 오브젝트에 충돌했습니다.");
            return hit.collider.gameObject;

            // 충돌 지점에 시각적인 표시를 하고 싶다면:
            // Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
        }

        return null;
    }
}
