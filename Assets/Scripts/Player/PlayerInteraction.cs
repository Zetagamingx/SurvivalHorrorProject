using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerDetection playerDetection;
    [SerializeField] public InputActionReference interactAction;

    private void Awake()
    {
        playerDetection = GetComponent<PlayerDetection>();
    }

    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }
    private void OnInteract(InputAction.CallbackContext context)
    {
        //Debug.Log("Interact button pressed");
        playerDetection.CurrentInteract?.Interact();
    }

    public void ClearInteraction()
    {
        playerDetection.ClearCurrentInteract();
    }
}
