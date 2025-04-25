using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifeTime = 9f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // 자동 삭제
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("플레이어 맞음!" + other.name);

        Destroy(gameObject); // 충돌 시 제거
    }
}
