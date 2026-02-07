using UnityEngine;

public class ColliderVisualizer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            if (!col.enabled) continue;

            if (col is BoxCollider box)
            {
                Gizmos.matrix = box.transform.localToWorldMatrix;
                Gizmos.DrawWireCube(box.center, box.size);
            }
            else if (col is SphereCollider sphere)
            {
                Gizmos.matrix = sphere.transform.localToWorldMatrix;
                Gizmos.DrawWireSphere(sphere.center, sphere.radius);
            }
            else if (col is CapsuleCollider capsule)
            {
                Gizmos.matrix = capsule.transform.localToWorldMatrix;
                DrawWireCapsule(capsule);
            }
            // Add more collider types as needed
        }

        Gizmos.matrix = Matrix4x4.identity; // reset
    }

    void DrawWireCapsule(CapsuleCollider capsule)
    {
        // Simplified placeholder – capsule wire drawing is complex
        Gizmos.DrawWireSphere(capsule.center, capsule.radius); // Approx
    }
}