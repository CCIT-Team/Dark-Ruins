using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController_KSM : CreatureController_KSM
{
    private bool isCurrentlyHighlighted = false;

    [Header("거리")]
    private float patrolRadius = 7f;
    private float attackDistance = 2f;
    private float lostDistance = 15f;
    private float proximityRadius = 5f;
    private float viewAngle = 90f;
    //private float hearingRadius = 20f;

    [Header("콜라이더")]
    public SphereCollider detectionCollider;
    public LayerMask obstacleMask;
    public Collider weakPointCollider;
    public Collider hitCollider;

    [Header("약점 뎀지배수")]
    [SerializeField] private float weakPointMultiplier = 2f;

    private Rigidbody rb;
    private Transform target;
    private Vector3 patrolOrigin;

    public enum State { IDLE, PATROL, CHASE, ATTACK, DIE }
    public State currentState;

    protected override void Awake()
    {
        base.Awake();
        nmAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (nmAgent != null)
        {
            nmAgent.updatePosition = false;
            nmAgent.updateRotation = true;
        }
    }

    protected override void Start()
    {
        base.Start();

        patrolOrigin = transform.position;
        nmAgent.stoppingDistance = attackDistance;



        ChangeState(State.IDLE);
    }

    private void FixedUpdate()
    {
        if (nmAgent == null || rb == null || currentState == State.DIE)
        {
            return;
        }

        Vector3 desiredVelocity = nmAgent.velocity;

        if (!nmAgent.isStopped)
        {
            rb.MovePosition(rb.position + desiredVelocity * Time.fixedDeltaTime);
        }
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
                Debug.Log("UV활성화 약공" + finalDamage);
            }
            else
            {
                Debug.Log("UV비활성화 약공" + finalDamage);
            }
        }
        else
        {
            Debug.Log("일반 뎀지" + finalDamage);
        }

        base.OnDamaged(finalDamage, attacker, isWeakPoint);

        if (currentHealth > 0)
        {
            if (target == null && attacker != null)
            {
                target = attacker;
            }
            if (currentState != State.CHASE && currentState != State.ATTACK)
            {
                ChangeState(State.CHASE);
            }
        }
    }

    public void NotifyWeakPointExposed()
    {
        isCurrentlyHighlighted = true;
    }
    public void NotifyWeakPointHidden()
    {
        isCurrentlyHighlighted = false;
    }

    public override void OnDead()
    {
        ChangeState(State.DIE);
    }

    private void ChangeState(State newState)
    {
        if (currentState == State.DIE) return;

        StopAllCoroutines();
        currentState = newState;

        StartCoroutine(currentState.ToString());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || target != null || currentState == State.DIE)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, other.transform.position);

        if (distance <= proximityRadius)
        {
            target = other.transform;
            ChangeState(State.CHASE);
            Debug.Log("주변 탐지");
            return;
        }

        Vector3 selfPos = transform.position;
        Vector3 playerPos = other.transform.position;
        selfPos.y = 0;
        playerPos.y = 0;

        Vector3 directionToPlayer = (playerPos - selfPos).normalized;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < viewAngle / 2f)
        {
            Vector3 rayStartPoint = transform.position + Vector3.up * 1f;
            Vector3 directionToPlayerWithHeight = (other.transform.position - rayStartPoint).normalized;

            if (!Physics.Raycast(rayStartPoint, directionToPlayerWithHeight, distance, obstacleMask))
            {
                target = other.transform;
                ChangeState(State.CHASE);
                Debug.Log("시야각 탐지");
            }
        }
    }

    public IEnumerator IDLE()
    {
        //anim.SetTrigger("Idle");
        nmAgent.isStopped = true;

        yield return new WaitForSeconds(Random.Range(2f, 4f));

        ChangeState(State.PATROL);
    }

    public IEnumerator PATROL()
    {
        //anim.SetTrigger("Patrol");
        nmAgent.isStopped = false;

        Vector3 randomPos = Random.insideUnitSphere * patrolRadius;
        randomPos += patrolOrigin;

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

        while (nmAgent.pathPending || (nmAgent.remainingDistance > nmAgent.stoppingDistance && !nmAgent.isPathStale))
        {
            yield return null;
        }

        ChangeState(State.IDLE);
    }

    public IEnumerator CHASE()
    {
        //anim.SetTrigger("Chase");
        nmAgent.isStopped = false;

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

        if (target == null)
        {
            ChangeState(State.PATROL);
        }
    }

    public IEnumerator ATTACK()
    {
        //anim.SetTrigger("Attack");
        nmAgent.isStopped = true;

        if (target != null)
        {
            transform.LookAt(target.position);
        }

        yield return new WaitForSeconds(0.5f);

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackDistance)
            {
                ChangeState(State.ATTACK);
            }
            else if (distance <= lostDistance)
            {
                ChangeState(State.CHASE);
            }
            else
            {
                target = null;
                ChangeState(State.PATROL);
            }
        }
        else
        {
            ChangeState(State.PATROL);
        }
    }

    public IEnumerator DIE()
    {
        nmAgent.isStopped = true;

        //anim.SetTrigger("Die");

        if (hitCollider != null) hitCollider.enabled = false;
        if (detectionCollider != null) detectionCollider.enabled = false;

        yield return new WaitForSeconds(4f);

        Destroy(gameObject);
    }
}