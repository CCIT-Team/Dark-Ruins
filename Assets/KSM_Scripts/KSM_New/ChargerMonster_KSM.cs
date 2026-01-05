using System.Collections;
using UnityEngine;

public class ChargerMonster_KSM : MonsterController_KSM
{
    [Header("돌진 설정")]
    [SerializeField] private float chargeDistance = 10f;
    [SerializeField] private float chargeSpeed = 20f;
    [SerializeField] private float chargePreparationTime = 1.0f;
    [SerializeField] private float chargeCooldown = 7.0f;

    [Header("넉백 설정")]
    [SerializeField] private float knockbackForce = 15f;
    [SerializeField] private float knockbackDuration = 0.5f;

    [Header("추적 설정")]
    [SerializeField] private float customLostDistance = 20f;

    private float defaultSpeed;
    private float lastChargeTime;
    private float originalAngularSpeed;

    protected override void Start()
    {
        base.Start();
        defaultSpeed = nmAgent.speed;
        originalAngularSpeed = nmAgent.angularSpeed;
        lastChargeTime = -chargeCooldown;

        lostDistance = customLostDistance;

        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.freezeRotation = true;
        }
    }

    public override IEnumerator CHASE()
    {
        nmAgent.isStopped = false;
        nmAgent.speed = defaultSpeed;
        nmAgent.stoppingDistance = attackDistance;

        float keepChasingTimer = 0f;
        float chasePersistenceTime = 2.0f;

        while (target != null)
        {
            nmAgent.SetDestination(target.position);
            float distance = Vector3.Distance(transform.position, target.position);
            bool isChargeReady = (Time.time >= lastChargeTime + chargeCooldown);

            if (distance <= chargeDistance && isChargeReady)
            {
                if (HasLineOfSightToTarget())
                {
                    ChangeState(State.CHARGE);
                    yield break;
                }
            }

            if (distance <= attackDistance)
            {
                ChangeState(State.ATTACK);
                yield break;
            }

            else if (distance > lostDistance)
            {
                keepChasingTimer += Time.deltaTime;
                if (keepChasingTimer > chasePersistenceTime)
                {
                    target = null;
                    ChangeState(State.PATROL);
                    yield break;
                }
            }
            else
            {
                keepChasingTimer = 0f;
            }

            yield return new WaitForSeconds(0.2f);
        }
        ChangeState(State.PATROL);
    }

    private bool HasLineOfSightToTarget()
    {
        if (target == null) return false;
        Vector3 direction = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);
        if (Physics.Raycast(transform.position + Vector3.up, direction, distance, obstacleMask)) return false;
        return true;
    }

    public IEnumerator CHARGE()
    {
        lastChargeTime = Time.time;
        nmAgent.isStopped = true;
        nmAgent.velocity = Vector3.zero;
        nmAgent.updateRotation = false;
        if (anim != null) anim.SetTrigger("rush attack");

        float elapsed = 0f;
        while (elapsed < chargePreparationTime)
        {
            if (target != null)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                dir.y = 0;
                if (dir != Vector3.zero)
                {
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 15f);
                }
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        nmAgent.updatePosition = false;
        nmAgent.updateRotation = false;
        rb.isKinematic = false;

        float distToTarget = 5f;
        if (target != null) distToTarget = Vector3.Distance(transform.position, target.position);
        float actualChargeDistance = Mathf.Min(distToTarget + 3.0f, chargeDistance);
        float chargeDuration = Mathf.Max(actualChargeDistance / chargeSpeed, 0.5f);

        float timer = 0f;
        bool hasHit = false;
        Vector3 dashDirection = transform.forward;

        while (timer < chargeDuration)
        {
            float step = chargeSpeed * Time.fixedDeltaTime;

            RaycastHit hit;
            if (rb.SweepTest(dashDirection, out hit, step + 0.5f))
            {
                if (((1 << hit.collider.gameObject.layer) & obstacleMask) != 0)
                {
                    rb.MovePosition(hit.point - dashDirection * 1.0f);
                    break;
                }
            }

            Vector3 nextPosition = rb.position + dashDirection * step;
            rb.MovePosition(nextPosition);
            nmAgent.nextPosition = rb.position;

            if (!hasHit && target != null)
            {
                if (Vector3.Distance(transform.position, target.position) < 2.0f)
                {
                    IDamageable_KSM damageable = target.GetComponent<IDamageable_KSM>();
                    if (damageable != null) damageable.OnDamaged(attackDamage, transform, false);

                    PlayerController_KSM player = target.GetComponent<PlayerController_KSM>();
                    if (player != null)
                    {
                        Vector3 knockDir = dashDirection; knockDir.y = 0.5f;
                        player.ApplyKnockback(knockDir * knockbackForce, knockbackDuration);
                    }
                    hasHit = true;
                }
            }

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        nmAgent.nextPosition = rb.position;
        nmAgent.updatePosition = true;
        nmAgent.updateRotation = true;
        nmAgent.angularSpeed = originalAngularSpeed;

        yield return new WaitForSeconds(1.0f);

        if (target != null)
        {
            float currentDist = Vector3.Distance(transform.position, target.position);

            if (currentDist > lostDistance)
            {
                target = null;
                ChangeState(State.PATROL);
            }
            else
            {
                ChangeState(State.CHASE);
            }
        }
        else
        {
            ChangeState(State.PATROL);
        }
    }

    public override IEnumerator ATTACK()
    {
        if (nmAgent)
        {
            nmAgent.isStopped = true;
            nmAgent.velocity = Vector3.zero;
            nmAgent.updateRotation = false;
        }

        isAttackAnimationFinished = false;
        if (anim != null)
        {
            anim.ResetTrigger("attack");
            anim.SetTrigger("attack");
            anim.SetFloat("speed", 0f);
        }

        float lookTimer = 0f;
        while (lookTimer < 0.5f && !isAttackAnimationFinished)
        {
            if (target != null) SmoothLookAt(target.position);
            lookTimer += Time.deltaTime;
            yield return null;
        }

        while (!isAttackAnimationFinished)
        {
            yield return null;
        }

        if (attackCooldown > 0)
            yield return new WaitForSeconds(attackCooldown);

        if (nmAgent) nmAgent.updateRotation = true;

        if (target != null)
        {
            ChangeState(State.CHASE);
        }
        else
        {
            ChangeState(State.PATROL);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (Application.isPlaying && currentState == State.CHARGE)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * 3.0f);
    //    }
    //}
}