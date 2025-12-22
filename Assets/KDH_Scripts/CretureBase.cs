using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CreatureBase : MonoBehaviour,IDamageable_KSM
{
    [Header("Ã¼·Â")]
    [SerializeField] protected int maxHealth = 1;
    public int currentHealth { get; protected set; }

    //public Animator anim { get; protected set; }

    protected virtual void Awake()
    {
        //anim = GetComponent<Animator>();
    }

    protected void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void OnDamaged(int damage, Transform attacker, bool isWeakPoint)
    {
#if UNITY_EDITOR
        Debug.Log("???");
#endif
        if (currentHealth <= 0) return;

        currentHealth -= damage;
#if UNITY_EDITOR
        Debug.Log(gameObject.name + " Health: " + currentHealth);
#endif
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDead();
        }
    }

    public abstract void OnDead();
}
