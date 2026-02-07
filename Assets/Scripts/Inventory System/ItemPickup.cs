using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour, IObtainable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int quantity = 1;

    [Header("Dialogue UI")]
    public GameObject dialogueObject;
    public GameObject dialogueBox;
    private TextMeshProUGUI textMeshPro;
    private InteractDialogueController dialogueController;
    
    public string InteractionPrompt => itemData != null ? itemData.pickupMessage : "";

    public ItemData Data => itemData;
    public int Quantity => quantity;

    private void Start()
    {
        textMeshPro = dialogueBox.GetComponent<TextMeshProUGUI>();
        if (dialogueController == null && dialogueObject != null)
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();
    }

    public void Interact()
    {
        StartCoroutine(ShowPromptThenWaitForInput());
    }

    private IEnumerator ShowPromptThenWaitForInput()
    {
        if (dialogueController == null)
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();

        textMeshPro.text = InteractionPrompt;
        dialogueController.Show();

        // Wait for graceDelay to expire (dialogueController handles this internally)
        yield return new WaitUntil(() => dialogueController.CanClose);

        // Wait for player to press Interact again
        yield return new WaitUntil(() => Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame);
        // Or replace 'eKey' with your actual mapped key in Input System (or custom InputManager)

        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(itemData.itemName, quantity);
            InventoryUI.Instance?.UpdateUI();
        }

        dialogueController.TryClose();
        Destroy(gameObject);
    }


    private void ShowDialogue(string message)
    {
        if (dialogueObject == null || textMeshPro == null) return;

        if (dialogueController == null)
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();

        if (dialogueController.isShowingDialogue)
        {
            dialogueController.TryClose();
            return;
        }

        textMeshPro.text = message;
        dialogueController.Show();
    }
}