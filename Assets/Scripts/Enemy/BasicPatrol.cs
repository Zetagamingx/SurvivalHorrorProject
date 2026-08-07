using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicPatrol : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<Transform> patrolPoints;

    private NavMeshAgent agent;
    private int currentIndex;

    private void Awake()
    {
        
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (spawnPoint != null)
            agent.Warp(spawnPoint.position);

        PickNewTarget();
        StartCoroutine(PatrolRoutine());
    }

    void PickNewTarget()
    {
        currentIndex = Random.Range(0, patrolPoints.Count);
        agent.SetDestination(patrolPoints[currentIndex].position);
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!agent.pathPending &&
                agent.remainingDistance <= agent.stoppingDistance)
            {
                yield return new WaitForSeconds(1f);
                PickNewTarget();
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}