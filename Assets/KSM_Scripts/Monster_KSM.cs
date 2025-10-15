using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster_KSM : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public float lostDistance = 5f;
    public Transform target;

    public NavMeshAgent nmAgent;
    public SphereCollider sphereCollider;

    private Animator anim;
    private Rigidbody rb;
    
    
    enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DIE,
    }

    State state;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        nmAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Start()
    { 
        MaxHealth = 10;
        state = State.IDLE;

        StartCoroutine(StateMachine());
    }

    void Update()
    {
        if (target == null) return;

        nmAgent.SetDestination(target.position);
    }

    IEnumerator StateMachine()
    {
        while (currentHealth > 0)
        {
            yield return StartCoroutine(state.ToString());
        }
    }

    public IEnumerator IDLE()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("IdleNormal") == false)
        {
            anim.Play("IdleNormal", 0, 0);

            yield return null;
        }

        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
        float lookSpeed = Random.Range(25f, 40f);

        for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);

            yield return null;
        }

        if (currentHealth <= 0)
        {
            ChangeState(State.DIE);
        }
    }

    public IEnumerator PATROL()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("PATROL") == false)
        {
            anim.Play("PATROL", 0, 0);

            yield return null;
        }

        if (currentHealth <= 0)
        {
            ChangeState(State.DIE);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player") return;

        target = other.transform;
        nmAgent.SetDestination(target.position);

        ChangeState(State.CHASE);

        if (other.tag == "Knife")
        {
            Knife_KSM knife = other.GetComponent<Knife_KSM>();
            currentHealth -= knife.damage;

            Debug.Log("Monster Health: " + currentHealth);
        }
    }

    IEnumerator OnDamage()
    {
        yield return null;
    }
    public IEnumerator CHASE()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("WalkFWD") == false)
        {
            anim.Play("WalkFWD", 0, 0);

            yield return null;
        }

        if (nmAgent.remainingDistance > lostDistance)
        {
            target = null;
            nmAgent.SetDestination(transform.position);

            yield return null;

            ChangeState(State.IDLE);
        }
        else if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
        {
            ChangeState(State.ATTACK);
        }
        else if (currentHealth <= 0)
        {
            ChangeState(State.DIE);
        }
        else
        {
            yield return new WaitForSeconds(curAnimStateInfo.length);
        }
    }

    public IEnumerator ATTACK()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("Attack01") == false)
        {
            anim.Play("Attack01", 0, 0);

            yield return null;
        }

        if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
        {
            ChangeState(State.CHASE);
        }
        else if (currentHealth <=0)
        {
            ChangeState(State.DIE);
        }
        else
            yield return new WaitForSeconds(curAnimStateInfo.length * 2f);
    }

    public IEnumerator DIE()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("DIE") == false)
        {
            anim.Play("Die", 0, 0);
            
            yield return null;
        }
    }

    void ChangeState(State newState)
    {
        state = newState;
    }
}