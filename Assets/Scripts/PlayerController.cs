using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{

    private InputSystem_Actions controls => InputManagerController.Instance.controls;
    public GameObject characterSubparent;
    private Rigidbody characterRb;
    public GameObject characterMainObject;
    private Vector2 inputVector;
    public int moveSpeed;
    public bool isWalking = false;

    private Animator animator;

    private float mouseX;
    public float rotationSpeed = 10f;

    private PlayerDetection detection;

    
    public void Awake()
    {
        
        controls.Player.SetCallbacks(this);
        characterRb = characterSubparent.GetComponent<Rigidbody>();
        animator = characterMainObject.GetComponent<Animator>();

        detection = GetComponent<PlayerDetection>();

        UpdateWalkingState();
    }

    public void OnEnable()
    {
       
    }

    public void OnDisable()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();



        Vector3 moveDir = camRight * inputVector.x + camForward * inputVector.y;
        moveDir.Normalize();

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = characterRb.linearVelocity.y;
        characterRb.linearVelocity = velocity;

        if (camForward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(camForward);
            characterMainObject.transform.rotation = Quaternion.Slerp(
                characterMainObject.transform.rotation,
                targetRot,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        UpdateWalkingState();
    }

    private void UpdateWalkingState()
    {
        // Check horizontal input magnitude to determine if walking
        isWalking = inputVector.sqrMagnitude > 0.01f;

        if (animator != null)
        {
            animator.SetBool("isWalking", isWalking);
        }
    }

    public void OnCombineTest(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log($"[DEBUG] CombineManager.Instance is: {CombineManager.Instance}");
        Debug.Log($"[DEBUG] inventory is: {Inventory.Instance}");
        Debug.Log($"[DEBUG] inventory.itemDatabase is: {Inventory.Instance?.ItemDatabase}");

        var itemA = Inventory.Instance.ItemDatabase.GetItemByName("Aerosol Can");
        var itemB = Inventory.Instance.ItemDatabase.GetItemByName("Warning Paper");

        Debug.Log($"[DEBUG] itemA is: {itemA?.itemName ?? "NULL"}");
        Debug.Log($"[DEBUG] itemB is: {itemB?.itemName ?? "NULL"}");

        bool success = CombineManager.Instance.TryCombine(itemA, itemB);
        Debug.Log("Combination successful: " + success);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {

    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            Debug.Log("Im pressing E");
            return;
        }

        if (detection.CurrentInteractable != null)
        {
            detection.CurrentInteractable.Interact();
            if (detection.CurrentInteractable is IObtainable obtainable)
            {
                Debug.Log($"Picked up {obtainable.Quantity}x {obtainable.Data.itemName}");
                detection.ClearInteractable();
            }
            
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        mouseX = mouseDelta.x;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }

    public void OnInventoryAccess(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        InventoryNavigation.Instance.ToggleInventory();

    }    
}
