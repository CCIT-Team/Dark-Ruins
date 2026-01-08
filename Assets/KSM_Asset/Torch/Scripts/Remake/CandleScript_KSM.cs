using UnityEngine;

public class CandleScript_KSM : MonoBehaviour
{
    [Header("연결할 컴포넌트")]
    [SerializeField] private GameObject lightObject;
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private AudioSource audioSource;

    [Header("설정")]
    public bool isOn = true;

    private void Start()
    {
        UpdateCandleState();
    }

    public void Interact()
    {
        isOn = !isOn;
        UpdateCandleState();

        if (isOn && audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void UpdateCandleState()
    {
        if (lightObject != null) lightObject.SetActive(isOn);

        if (fireParticle != null)
        {
            if (isOn) fireParticle.Play();
            else fireParticle.Stop();
        }
    }
}