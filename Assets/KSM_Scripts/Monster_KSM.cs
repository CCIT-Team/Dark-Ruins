using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_KSM : MonoBehaviour
{
    [Header("몬스터 능력치")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("범위 설정")]
    public Transform target;
    public float patrolRadius = 10f;
    public float chaseDistance = 15f;
    public float attackDistance = 2f;
    public float lostDistance = 20f;

    [Header("콜라이더 설정")]
    [Tooltip("감지 콜라이더")]
    public SphereCollider detectionCollider;
    [Tooltip("피격 콜라이더")]
    public Collider hitCollider;
    
    private NavMeshAgent nmAgent;
    private Animator anim;
    private Vector3 patrolOrigin;

    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DIE,
    }
    public State currentState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        patrolOrigin = transform.position;

        nmAgent.stoppingDistance = attackDistance;

        ChangeState(State.IDLE);
    }

    private void ChangeState(State newState)
    {
        if (currentState == State.DIE) return;

        StopAllCoroutines();
        currentState = newState;

        StartCoroutine(currentState.ToString());
    }

    public void TakeDamage(int damage, Transform attacker)
    {
        if (currentState == State.DIE) return;

        currentHealth -= damage;
        Debug.Log("Monster Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            ChangeState(State.DIE);
        }
        else
        {
            if (target == null && attacker != null)
            {
                target = attacker;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != State.DIE && other.CompareTag("Player") && target == null)
        {
            target = other.transform;
            ChangeState(State.CHASE);
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
        nmAgent.isStopped = true;

        if (target != null)
        {
            transform.LookAt(target.position);
        }

        //anim.SetTrigger("Attack");

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