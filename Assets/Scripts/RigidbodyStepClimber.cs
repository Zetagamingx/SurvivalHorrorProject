using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class RigidbodyStepClimber : MonoBehaviour
{
    [Header("Step Settings")]
    public float maxStepHeight = 0.21f;       // Maximum height the player can climb
    public float forwardCheckDistance = 0.2f; // Distance to check in front

    private Rigidbody rb;
    private CapsuleCollider col;

    // Debug variables
    private Vector3 lastRayOrigin;
    private Vector3 lastRayDirection;
    private bool hitSomething;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        TryStep();
    }

    void TryStep()
    {
        // Foot level: true bottom of capsule + small offset
        float footY = col.bounds.min.y + 0.01f;
        Vector3 footOrigin = new Vector3(transform.position.x, footY, transform.position.z);

        float radius = col.radius * 0.95f;
        float height = col.height - (2 * col.radius);

        // Save debug info
        lastRayOrigin = footOrigin;
        lastRayDirection = transform.forward * forwardCheckDistance;
        hitSomething = false;

        // Forward capsule cast to detect obstacle
        if (Physics.CapsuleCast(
            footOrigin,
            footOrigin + Vector3.up * height,
            radius,
            transform.forward,
            out RaycastHit hit,
            forwardCheckDistance))
        {
            hitSomething = true;

            // Calculate the actual obstacle height relative to foot
            float obstacleHeight = hit.point.y - footY;

            // Only climb if within maxStepHeight
            if (obstacleHeight > 0f && obstacleHeight <= maxStepHeight)
            {
                // Move Rigidbody up exactly to the obstacle height
                rb.MovePosition(rb.position + Vector3.up * obstacleHeight);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Forward cast visualization
            Gizmos.color = hitSomething ? Color.green : Color.red;
            Gizmos.DrawRay(lastRayOrigin, lastRayDirection);

            // Max step height visualization
            Gizmos.color = Color.yellow;
            Vector3 stepTopPos = new Vector3(lastRayOrigin.x, lastRayOrigin.y + maxStepHeight, lastRayOrigin.z);
            Gizmos.DrawRay(stepTopPos, lastRayDirection);
        }
    }
}