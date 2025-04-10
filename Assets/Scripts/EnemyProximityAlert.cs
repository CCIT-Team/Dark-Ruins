using UnityEngine;

public class EnemyProximityAlert : MonoBehaviour
{
    public float alertRadius = 10f;              // 반경 설정
    public string enemyTag = "Enemy";            // 적 오브젝트의 태그
    public AudioSource alertSound;               // 경고 사운드

    private bool isPlaying = false;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        bool enemyNearby = false;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < alertRadius)
            {
                enemyNearby = true;
                break;
            }
        }

        if (enemyNearby && !isPlaying)
        {
            alertSound.Play();
            isPlaying = true;
        }
        else if (!enemyNearby && isPlaying)
        {
            alertSound.Stop();
            isPlaying = false;
        }
    }
}
