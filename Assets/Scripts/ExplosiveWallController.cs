using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class ExplosiveWallController : MonoBehaviour,IInteractable
{
    public bool hasExplosiveCan;
    public bool hasInteractedOnce;

    [Header("Dialogue UI")]
    public GameObject dialogueObject;
    public GameObject dialogueBox;
    private TextMeshProUGUI textMeshPro;

    private InteractDialogueController dialogueController;

    public string InteractionPrompt
    {
        get
        {
            if (!hasInteractedOnce && !hasExplosiveCan)
                return "There is a hole in the wall... \n it's too dark to see what’s on the other side.";
            else if (hasInteractedOnce && !hasExplosiveCan)
                return "It smells like... kerosene?";
            else if (hasExplosiveCan)
                return "You placed the hand-made explosive through the hole.";

            return string.Empty;
        }
    }

    public void Start()
    {
        textMeshPro = dialogueBox.GetComponent<TextMeshProUGUI>();
    }

    public void Interact()
    {
        if (Inventory.Instance.HasItem("Explosive Can"))
        {
            hasExplosiveCan = true;
            Inventory.Instance.RemoveItem("Explosive Can", 1);
        }
    
        Debug.Log("ExplosiveWallController.Interact() called");

        if (dialogueController == null && dialogueObject != null)
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();

        // If the dialogue is currently showing, try to close it
        if (dialogueController != null && dialogueController.isShowingDialogue)
        {
            dialogueController.TryClose();
            return;
        }

        ShowDialogue(InteractionPrompt);
        // Update interaction state
        if (!hasInteractedOnce && !hasExplosiveCan)
        {
            hasInteractedOnce = true;
            
        }

        else if (hasInteractedOnce)
        {
            ShowDialogue(InteractionPrompt);
        }

        else if(hasExplosiveCan)
        {
            ShowDialogue(InteractionPrompt);
        }
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


