using UnityEngine;

public class CheckpointZone_KSM : MonoBehaviour
{
    public Transform newCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn_KSM playerRespawn = other.GetComponent<PlayerRespawn_KSM>();
            if (playerRespawn != null)
            {
                playerRespawn.checkpoint = newCheckpoint;
            }
        }
    }
}