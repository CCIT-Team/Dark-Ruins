using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackHitbox_KSM : MonoBehaviour
{
    private int damage;
    private Transform owner;

    private List<GameObject> hitHistory = new List<GameObject>();

    public void Initialize(int damageAmount, Transform ownerTransform)
    {
        this.damage = damageAmount;
        this.owner = ownerTransform;
    }

    private void OnEnable()
    {
        hitHistory.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hitHistory.Contains(other.gameObject))
            {
                IDamageable_KSM damageable = other.GetComponent<IDamageable_KSM>();

                if (damageable == null) damageable = other.GetComponentInParent<IDamageable_KSM>();

                if (damageable != null)
                {
                    damageable.OnDamaged(damage, owner, false);
                    hitHistory.Add(other.gameObject);
                    Managers_YGU.Sound.Play("User_Hit_Monster",eSound.UI);
                }
            }
        }
    }
}