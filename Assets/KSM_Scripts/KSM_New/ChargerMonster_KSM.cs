using System.Collections;
using UnityEngine;

public class ChargerMonster_KSM : MonsterController_KSM
{
    [Header("돌진 설정")]
    [SerializeField] private float chargeDistance = 10f;
    [SerializeField] private float chargeSpeed = 20f;
    [SerializeField] private float chargePreparationTime = 1.0f;
    [SerializeField] private float chargeCooldown = 7.0f; // 쿨타임 7초

    [Header("넉백 설정")]
    [SerializeField] private float knockbackForce = 15f;
    [SerializeField] private float knockbackDuration = 0.5f;

    private float defaultSpeed;
    private float lastChargeTime;
    private float originalAngularSpeed;

    protected override void Start()
    {
        base.Start();
        defaultSpeed = nmAgent.speed;
        originalAngularSpeed = nmAgent.angularSpeed;

        // 게임 시작 시 바로 돌진 가능하도록 초기화
        lastChargeTime = -chargeCooldown;
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

            if (distance <= attackDistance)
            {
                ChangeState(State.ATTACK);
                yield break;
            }
            // 쿨타임 체크 (Time.time이 마지막 돌진시간 + 7초보다 커야 함)
            else if (distance <= chargeDistance && Time.time >= lastChargeTime + chargeCooldown)
            {
                if (HasLineOfSightToTarget())
                {
                    ChangeState(State.CHARGE);
                    yield break;
                }
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
        // [수정 1] 쿨타임 즉시 적용 (중복 실행 방지)
        lastChargeTime = Time.time;

        // 1. 돌진 준비
        nmAgent.isStopped = true;
        nmAgent.velocity = Vector3.zero;
        nmAgent.updateRotation = false;

        Debug.Log("돌진 준비...");

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

        // 2. 돌진 시간 계산
        Debug.Log("돌진!");

        float distToTarget = 5f;
        if (target != null) distToTarget = Vector3.Distance(transform.position, target.position);

        // 플레이어 뒤 3m까지 지나가도록 계산
        float actualChargeDistance = distToTarget + 3.0f;
        actualChargeDistance = Mathf.Min(actualChargeDistance, chargeDistance);

        // 시간 = 거리 / 속도
        float chargeDuration = actualChargeDistance / chargeSpeed;
        chargeDuration = Mathf.Max(chargeDuration, 0.5f);

        // 3. 돌진 루프
        float timer = 0f;
        bool hasHit = false;
        Vector3 dashDirection = transform.forward; // 방향 고정

        while (timer < chargeDuration)
        {
            // [수정 2] Rigidbody 직접 이동 (위치 동기화 문제 해결)
            // NavMeshAgent.Move 대신 Rigidbody를 밀어버립니다.
            Vector3 nextPosition = rb.position + dashDirection * chargeSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPosition);

            // [중요] 실린더(Agent)가 몸체를 따라오도록 강제 동기화
            nmAgent.nextPosition = rb.position;

            // 벽 충돌 감지 (SphereCast)
            if (Physics.SphereCast(transform.position + Vector3.up, 1.0f, dashDirection, out RaycastHit hit, 1.0f, obstacleMask))
            {
                Debug.Log("벽 충돌! 돌진 중단");
                break;
            }

            // 플레이어 충돌 감지
            if (!hasHit && target != null)
            {
                float distToPlayer = Vector3.Distance(transform.position, target.position);
                if (distToPlayer < 2.0f)
                {
                    Debug.Log("돌진 공격 적중!");

                    IDamageable_KSM damageable = target.GetComponent<IDamageable_KSM>();
                    if (damageable != null) damageable.OnDamaged(attackDamage, transform, false);

                    PlayerController_KSM player = target.GetComponent<PlayerController_KSM>();
                    if (player != null)
                    {
                        Vector3 knockDir = dashDirection;
                        knockDir.y = 0.5f;
                        player.ApplyKnockback(knockDir * knockbackForce, knockbackDuration);
                    }
                    hasHit = true;
                }
            }

            timer += Time.fixedDeltaTime; // Rigidbody 이동이므로 fixedDeltaTime 사용 권장
            yield return new WaitForFixedUpdate(); // 물리 이동과 싱크를 맞춤
        }

        // 4. 돌진 종료 및 정리
        nmAgent.velocity = Vector3.zero;
        nmAgent.updateRotation = true;
        nmAgent.angularSpeed = originalAngularSpeed;

        // 후딜레이
        yield return new WaitForSeconds(1.0f);

        ChangeState(State.CHASE);
    }
}