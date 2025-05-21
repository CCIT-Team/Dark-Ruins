using UnityEngine;
using Cinemachine;

public class PlayerDeathCamera : MonoBehaviour
{
    public CinemachineVirtualCamera CM_Death_Camera;
    public CinemachineVirtualCamera defaultCam;
    public Transform playerFaceTarget; // 얼굴 앞에 빈 오브젝트 만들어 연결

    public float switchDelay = 0.1f;

    public void SwitchToCaptureCam()
    {
        StartCoroutine(SwitchRoutine());
    }

    private System.Collections.IEnumerator SwitchRoutine()
    {
        yield return new WaitForSeconds(switchDelay);
        CM_Death_Camera.Priority = 20; // 더 높은 Priority 값으로 전환
    }

    public void SwitchToMainCam()
    {
        Debug.Log("SwitchToMainCam called");
        CM_Death_Camera.Priority = 2;
    }
}
