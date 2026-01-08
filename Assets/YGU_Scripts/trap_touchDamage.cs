using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_touchDamage : MonoBehaviour
{
    private enum eTrap { spike, arrow };

    [Header("함정 설정")]
    [SerializeField] eTrap trapType;
    [SerializeField] private int trapDamage;
    [SerializeField] private string trapSound;
    [SerializeField] private string targetTag = "Player";

    private void Start()
    {
        if (trapType == eTrap.spike)
        {
            trapSound = "Trap_Thorn_Attack";
            trapDamage = 10;
        }
        else if (trapType == eTrap.arrow)
        {
            trapSound = "Trap_Arrow_Attack";
            trapDamage = 15;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
        {
            return;
        }

        IDamageable_KSM DM = other.transform.GetComponent<IDamageable_KSM>();

        if (DM == null)
        {
            return;
        }

        Managers_YGU.Sound.Play("User_Hit_Trap", eSound.UI);
        Managers_YGU.Sound.Play(trapSound, eSound.UI);

        DM.OnDamaged(trapDamage, this.transform, false);
    }
}
