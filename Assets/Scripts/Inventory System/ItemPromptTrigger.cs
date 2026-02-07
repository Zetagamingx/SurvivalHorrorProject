using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class ItemPromptTrigger : MonoBehaviour, IInteractable
{
    public bool hasExplosiveCan;
    public bool hasInteractedOnce;

    [Header("Dialogue UI")]
    public GameObject dialogueObject;
    public GameObject dialogueBox;
    private TextMeshProUGUI textMeshPro;

    private InteractDialogueController dialogueController;

    public string InteractionPrompt => "I got an Aerosol Can";


    public void Start()
    {
        textMeshPro = dialogueBox.GetComponent<TextMeshProUGUI>();
    }

    public void Interact()
    {
        Debug.Log("ItemPromptTrigger.Interact() called");

        if (dialogueController == null && dialogueObject != null)
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();

        
        if (dialogueController != null && dialogueController.isShowingDialogue)
        {
            dialogueController.TryClose();
            return;
        }

        ShowDialogue("I got an Aerosol Can");
        
       
    }

    private void ShowDialogue(string message)
    {
        if (dialogueObject == null || textMeshPro == null)
            return;

        if (dialogueController == null)
        {
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();

        }


        textMeshPro.text = message;
        dialogueController.Show();
    }
}

// Start is called once before the first execution of Update after the MonoBehaviour is created


