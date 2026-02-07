using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform characterEmptyObject; // Usually the LookAt target (child of player)
    public Vector3 offset = new Vector3(0, 3, -5); // Customize as needed

    public float rotationSpeed = 3.0f;
    private float currentYaw = 0f;

    private Vector2 mouseInput;

    public void Update()
    {
        // Mouse rotation around the target (horizontal)
        currentYaw += mouseInput.x * rotationSpeed;

        // Calculate rotated offset
        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);
        Vector3 rotatedOffset = rotation * offset;

        // Apply position and look
        transform.position = characterEmptyObject.position + rotatedOffset;
        transform.LookAt(characterEmptyObject);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}