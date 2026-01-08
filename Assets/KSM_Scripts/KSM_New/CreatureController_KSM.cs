using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public abstract class CreatureController_KSM : MonoBehaviour, IDamageable_KSM
{
    [Header("체력")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int attackDamage = 10;
    [SerializeField] public int currentHealth { get; protected set; }

    [Header("피격 이펙트 설정")]
    [SerializeField] protected bool useBloodEffect = true;

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

        if (useBloodEffect && attacker != null)
        {
            PlayBloodEffect(attacker.position);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDead();
        }
    }

    protected void PlayBloodEffect(Vector3 attackerPos)
    {
        if (BloodPoolManager_KSM.Instance == null) return;

        Vector3 hitPosition = transform.position + Vector3.up * 1.0f;
        Vector3 direction = (attackerPos - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        BloodPoolManager_KSM.Instance.PlayBloodEffect(hitPosition, rotation);
    }

    public abstract void OnDead();
}
