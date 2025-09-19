using UnityEngine;

public class CheckpointZone : MonoBehaviour
{
    public Transform newCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.checkpoint = newCheckpoint;
            }
        }
    }
}