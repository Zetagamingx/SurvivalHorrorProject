using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private SimpleStateMachine stateMachine;
    [SerializeField] public string openingSection;

    [Header("Input")]
    [SerializeField] private InputActionReference inventoryAction;
    [SerializeField] private PlayerActionManager playerActionManager;
    

    private bool isOpen;

    private void OnEnable()
    {
        inventoryAction.action.Enable();
        inventoryAction.action.performed += ToggleInventory;
    }

    private void OnDisable()
    {
        inventoryAction.action.performed -= ToggleInventory;
        inventoryAction.action.Disable();
    }

    private void Start()
    {
        inventoryContainer.SetActive(false);
        isOpen = false;
    }

    private void OpenInventory()
    {
        playerActionManager.DisableActions();

        isOpen = true;
        inventoryContainer.SetActive(true);
        stateMachine.SetState(openingSection);
    }

    private void CloseInventory()
    {
        playerActionManager.EnableActions();

        isOpen = false;
        stateMachine.SetState(openingSection);
        inventoryContainer.SetActive(false);
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (isOpen)
            CloseInventory();
        else
            OpenInventory();
    }
}