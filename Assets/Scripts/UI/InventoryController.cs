using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventoryPanel;

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
        inventoryPanel.SetActive(false);
        isOpen = false;
    }

    private void OpenInventory()
    {
        playerActionManager.DisableActions();

        isOpen = true;
        inventoryPanel.SetActive(true);
    }

    private void CloseInventory()
    {
        playerActionManager.EnableActions();

        isOpen = false;
        inventoryPanel.SetActive(false);
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (isOpen)
            CloseInventory();
        else
            OpenInventory();
    }
}