using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Playables;
public class ExplosiveWallController : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject explosionCutScene;
    [SerializeField] private PlayableDirector director;
    private TextMeshProUGUI textMeshPro;

    public bool hasExplosiveCan;
    public bool hasInteractedOnce;

    [Header("Dialogue UI")]
    public GameObject dialogueObject;
    public GameObject dialogueBox;

    private TimelineSubtitleBinder subtitleBinder;
    private InteractDialogueController dialogueController;

    public void Awake()
    {
        subtitleBinder = director.GetComponent<TimelineSubtitleBinder>();
    }

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
        /*if (PlayerInventory.Instance.HasItem("Explosive Can"))
        {
            hasExplosiveCan = true;
            Inventory.Instance.RemoveItem("Explosive Can", 1);           
        }

        if (hasExplosiveCan)
        {
            StartCutScene();
            return;
        }

        if (dialogueController == null && dialogueObject != null)
            dialogueController = dialogueObject.GetComponent<InteractDialogueController>();

        if (dialogueController != null && dialogueController.isShowingDialogue)
        {
            dialogueController.TryClose();
            return;
        }

        ShowDialogue(InteractionPrompt);

        if (!hasInteractedOnce)
            hasInteractedOnce = true;*/
    }

    private void StartCutScene()
    {

        InputManagerController.Instance.SetPlayerMovement(false);

        subtitleBinder.Rebind(); // <-- Important

        director.stopped += OnCutSceneFinished;
        director.Play();

    }

    private void OnCutSceneFinished(PlayableDirector director)
    {
        // Re-enable movement
        InputManagerController.Instance.SetPlayerMovement(true);

        

        // Unsubscribe (VERY IMPORTANT)
        director.stopped -= OnCutSceneFinished;

        explosionCutScene.SetActive(false);
        
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




