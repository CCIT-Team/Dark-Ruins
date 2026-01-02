using UnityEngine;

public interface IDamageable_KSM
{
    void OnDamaged(int damage, Transform attacker, bool isWeakPoint);
}