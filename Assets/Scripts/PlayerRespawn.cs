using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform checkpoint;
    public UIFadeInOutAnimation fadeUI;
    public bool isDead = false;
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;

            Debug.Log("Player has died.");

            FindObjectOfType<PlayerDeathCamera>().SwitchToCaptureCam();

            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        // 어두워지기
        yield return StartCoroutine(fadeUI.FadeOut());
        
        FindObjectOfType<PlayerDeathCamera>().SwitchToMainCam();

        // 플레이어 위치 이동
        transform.position = checkpoint.position;

        // 밝아지기
        yield return StartCoroutine(fadeUI.FadeIn());

        isDead = false;
    }
}