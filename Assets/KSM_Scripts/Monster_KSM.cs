using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster_KSM : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public Transform target;
    public float lostDistance;

    Animator anim;
    Rigidbody rb;
    NavMeshAgent nmAgent;
    SphereCollider sphereCollider;
    
    enum State
    {
        IDLE,
        CHASE,
        ATTACK,
    }

    State state;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player") return;

        target = other.transform;
        nmAgent.SetDestination(target.position);

        if(other.tag == "Knife")
        {
            Knife_KSM knife = other.GetComponent<Knife_KSM>();
            currentHealth -= knife.damage;

            Debug.Log("Monster Health: " + currentHealth);
        }

        ChangeState(State.CHASE);
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
            anim.Play("IdleNormal", 0, 0);

        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;

        float lookSpeed = Random.Range(25f, 40f);

        for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);

            yield return null;
        }
    }

    public IEnumerator CHASE()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("WalkFWD") == false)
        {
            anim.Play("WalkFWD", 0, 0);

            yield return null;
        }

        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
        {
            ChangeState(State.ATTACK);
        }
        else if (nmAgent.remainingDistance > lostDistance)
        {
            target = null;
            nmAgent.SetDestination(transform.position);
            yield return null;

            ChangeState(State.IDLE);
        }
        else
        {
            yield return new WaitForSeconds(curAnimStateInfo.length);
        }
    }

    public IEnumerator ATTACK()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        anim.Play("Attack01", 0, 0);

        if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
        {
            ChangeState(State.CHASE);
        }
        else
            yield return new WaitForSeconds(curAnimStateInfo.length * 2f);
    }

    void ChangeState(State newState)
    {
        state = newState;
    }
}