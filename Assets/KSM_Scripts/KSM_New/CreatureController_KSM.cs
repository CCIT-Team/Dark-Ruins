using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public abstract class CreatureController_KSM : MonoBehaviour, IDamageable_KSM
{
    [Header("기본 능력치")]
    [SerializeField] protected int maxHealth = 100;
    public int currentHealth { get; protected set; }
    [SerializeField] protected int attackDamage = 10;

    public Animator anim { get; protected set; }
    public NavMeshAgent nmAgent { get; protected set; }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void OnDamaged(int damage, Transform attacker, bool isWeakPoint)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDead();
        }
    }

    public abstract void OnDead();
}
