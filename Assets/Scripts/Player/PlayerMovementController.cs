using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    [SerializeField] private PlayerAnimatorController animController;

    public InputActionReference moveAction;

    private Rigidbody playerRb;
    private Vector2 input;
    private bool wasWalking;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        //AudioManager.Instance.PlayMusic("Cemetery");
        moveAction.action.Enable();
    }

    public void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        input = moveAction.action.ReadValue<Vector2>();

        bool isWalking = input != Vector2.zero;

        if (isWalking != wasWalking)
        {
            //AudioManager.Instance.PlaySfx("Step");
            animController.SetWalkingState(isWalking);
            wasWalking = isWalking;
        }
    }

    private void FixedUpdate()
    {
        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;

        Vector3 moveDirection = forward * input.y + right * input.x;

        Vector3 velocity = moveDirection * movementSpeed;

        // In case a basic jump is implemented velocity.y = playerRb.linearVelocity.y;

        playerRb.linearVelocity = velocity;

    }
}
