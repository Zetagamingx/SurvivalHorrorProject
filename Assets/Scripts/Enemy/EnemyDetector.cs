using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private Transform eyePoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    public Transform CurrentTarget { get; private set; }

    public bool CanSeePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectRange,
            playerLayer
        );

        if (hits.Length == 0)
        {
            CurrentTarget = null;
            return false;
        }

        Transform player = hits[0].transform;

        Vector3 dir = (player.position - eyePoint.position).normalized;
        float dist = Vector3.Distance(eyePoint.position, player.position);

        if (!Physics.Raycast(eyePoint.position, dir, dist, obstacleLayer))
        {
            CurrentTarget = player;
            return true;
        }

        CurrentTarget = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);

        if (eyePoint != null)
        {
            Gizmos.DrawLine(
                eyePoint.position,
                eyePoint.position + transform.forward * 2f
            );
        }
    }
}