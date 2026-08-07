using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    private enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

    [Header("References")]
    [SerializeField] private EnemyData data;
    [SerializeField] private BasicPatrol patrol;
    [SerializeField] private EnemyDetector detector;

    private NavMeshAgent agent;
    private EnemyState currentState;

    private float attackCooldownTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = data.moveSpeed;
        ChangeState(EnemyState.Patrol);
    }

    private void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Patrol:
                UpdatePatrol();
                break;

            case EnemyState.Chase:
                UpdateChase();
                break;

            case EnemyState.Attack:
                UpdateAttack();
                break;

            case EnemyState.Dead:
                UpdateDead();
                break;
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
            return;

        ExitState(currentState);

        currentState = newState;

        EnterState(currentState);
    }

    private void EnterState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Patrol:
                patrol.enabled = true;
                agent.isStopped = false;
                break;

            case EnemyState.Chase:
                patrol.enabled = false;
                agent.isStopped = false;
                break;

            case EnemyState.Attack:
                patrol.enabled = false;
                agent.isStopped = true;
                break;

            case EnemyState.Dead:
                patrol.enabled = false;
                agent.isStopped = true;
                break;
        }
    }

    private void ExitState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Attack:
                agent.isStopped = false;
                break;
        }
    }

    private void UpdatePatrol()
    {
        if (detector.CanSeePlayer())
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void UpdateChase()
    {
        if (!detector.CanSeePlayer())
        {
            ChangeState(EnemyState.Patrol);
            return;
        }

        agent.SetDestination(detector.CurrentTarget.position);

        float distance = Vector3.Distance(
            transform.position,
            detector.CurrentTarget.position
        );

        if (distance <= data.attackRange)
        {
            ChangeState(EnemyState.Attack);
        }
    }

    private void UpdateAttack()
    {
        if (!detector.CanSeePlayer())
        {
            ChangeState(EnemyState.Patrol);
            return;
        }

        transform.LookAt(detector.CurrentTarget.position);

        float distance = Vector3.Distance(
            transform.position,
            detector.CurrentTarget.position
        );

        if (distance > data.attackRange)
        {
            ChangeState(EnemyState.Chase);
            return;
        }

        if (attackCooldownTimer <= 0f)
        {
            Debug.Log(name + " attacks player for " + data.damage + " damage");
            attackCooldownTimer = data.attackCooldown;
        }
    }

    private void UpdateDead()
    {
    }

    public void Die()
    {
        ChangeState(EnemyState.Dead);
    }
}