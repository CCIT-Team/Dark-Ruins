using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController_KSM : CreatureController_KSM
{
    public enum State { IDLE, PATROL, CHASE, ATTACK, DIE, CHARGE }
    public State currentState;

    [Header("거리 설정")]
    protected float patrolRadius = 7f;
    protected float attackDistance = 3f;
    protected float lostDistance = 20f;
    protected float proximityRadius = 5f;
    protected float viewAngle = 90f;

    [Header("콜라이더 및 레이어")]
    public SphereCollider detectionCollider;
    public LayerMask obstacleMask;
    public List<Collider> hitColliders = new List<Collider>();
    public List<Collider> weakPointColliders = new List<Collider>();

    [Header("약점 데미지 배수")]
    [SerializeField] private float weakPointMultiplier = 2f;

    protected Rigidbody rb;
    protected Transform target;
    protected Vector3 patrolOrigin;

    private bool isCurrentlyHighlighted = false;

    protected override void Awake()
    {
        base.Awake();
        nmAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (nmAgent != null)
        {
            nmAgent.updatePosition = true;
            nmAgent.updateRotation = true;
        }

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    protected override void Start()
    {
        base.Start();
        patrolOrigin = transform.position;

        if (nmAgent != null) nmAgent.stoppingDistance = attackDistance;

        ChangeState(State.IDLE);
    }

    public override void OnDamaged(int damage, Transform attacker, bool isWeakPoint)
    {
        if (currentState == State.DIE) return;

        int finalDamage = damage;

        if (isWeakPoint)
        {
            if (isCurrentlyHighlighted)
            {
                finalDamage = (int)(damage * weakPointMultiplier);
                Debug.Log("약점 타격! (치명타)");
            }
            else
            {
                Debug.Log("약점 타격 (UV 비활성)");
            }
        }
        else
        {
            Debug.Log("일반 타격");
        }

        base.OnDamaged(finalDamage, attacker, isWeakPoint);

        if (currentHealth > 0)
        {
            if (target == null && attacker != null) target = attacker;

            if (currentState != State.CHASE && currentState != State.ATTACK && currentState != State.CHARGE)
            {
                ChangeState(State.CHASE);
            }
        }
    }

    public void NotifyWeakPointExposed() { isCurrentlyHighlighted = true; }
    public void NotifyWeakPointHidden() { isCurrentlyHighlighted = false; }

    public override void OnDead()
    {
        if (TryGetComponent<ItemDrop>(out ItemDrop ID) == true)
        {
            //ID.DeathDrop();
        }
        ChangeState(State.DIE);
    }

    protected void ChangeState(State newState)
    {
        if (currentState == State.DIE) return;

        StopAllCoroutines();
        currentState = newState;
        StartCoroutine(currentState.ToString());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || target != null || currentState == State.DIE) return;

        float distance = Vector3.Distance(transform.position, other.transform.position);

        if (distance <= proximityRadius)
        {
            target = other.transform;
            ChangeState(State.CHASE);
            Debug.Log("근접 감지");
            return;
        }

        Vector3 direction = (other.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle < viewAngle / 2f)
        {
            if (!Physics.Raycast(transform.position + Vector3.up, direction, distance, obstacleMask))
            {
                target = other.transform;
                ChangeState(State.CHASE);
                Debug.Log("시야 감지");
            }
        }
    }

    public virtual IEnumerator IDLE()
    {
        if (nmAgent) nmAgent.isStopped = true;
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        ChangeState(State.PATROL);
    }

    public virtual IEnumerator PATROL()
    {
        if (nmAgent) nmAgent.isStopped = false;

        Vector3 randomPos = Random.insideUnitSphere * patrolRadius + patrolOrigin;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPos, out hit, patrolRadius, 1))
        {
            nmAgent.SetDestination(hit.position);
        }
        else
        {
            ChangeState(State.IDLE);
            yield break;
        }

        while (nmAgent.pathPending || (nmAgent.remainingDistance > nmAgent.stoppingDistance))
        {
            yield return null;
        }
        ChangeState(State.IDLE);
    }

    public virtual IEnumerator CHASE()
    {
        if (nmAgent) nmAgent.isStopped = false;

        while (target != null)
        {
            nmAgent.SetDestination(target.position);
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackDistance)
            {
                ChangeState(State.ATTACK);
                yield break;
            }
            else if (distance > lostDistance)
            {
                target = null;
                ChangeState(State.PATROL);
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }

        if (target == null) ChangeState(State.PATROL);
    }

    public virtual IEnumerator ATTACK()
    {
        if (nmAgent) nmAgent.isStopped = true;
        if (target != null) transform.LookAt(target.position);

        yield return new WaitForSeconds(0.5f);

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackDistance) ChangeState(State.ATTACK);
            else if (distance <= lostDistance) ChangeState(State.CHASE);
            else ChangeState(State.PATROL);
        }
        else
        {
            ChangeState(State.PATROL);
        }
    }

    public virtual IEnumerator DIE()
    {
        if (nmAgent) nmAgent.isStopped = true;
        foreach (Collider col in hitColliders)
        {
            if (col != null) col.enabled = false;
        }

        foreach (Collider col in weakPointColliders)
        {
            if (col != null) col.enabled = false;
        }

        if (detectionCollider != null) detectionCollider.enabled = false;

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}