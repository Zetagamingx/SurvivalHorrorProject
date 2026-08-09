using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteract
{
    [SerializeField] private NPCAnimationController npcAnimationController;
    [SerializeField] private NPCRewardSystem npcRewardSystem;

    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private PlayerCameraController playerCameraController;
    [SerializeField] private PlayerInteraction playerInteraction;

    [SerializeField] private DialogueButtonController dialogueButtonController;
    [SerializeField] private GameObject dialogueConversation;
    [SerializeField] private AnswerOneButtonController answerOneButtonController;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private string firstDialogue;

    private CapsuleCollider capsuleCollider;
    public string InteractionPrompt => throw new System.NotImplementedException();

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Interact()
    {
        playerMovementController.moveAction.action.Disable();
        playerCameraController.lookAction.action.Disable();

        dialogueButtonController.npcInteraction = this;
        dialogueButtonController.StartConversation();
        answerOneButtonController.npcRewardSystem = GetComponent<NPCRewardSystem>();
        npcAnimationController.Talk();
        dialogueConversation.SetActive(true);
        textMeshPro.SetText(firstDialogue);
        

        
    }

    public void EndConversation()
    {
        dialogueConversation.SetActive(false);
        npcAnimationController.StopTalk();
        npcRewardSystem.GiveReward();
        npcRewardSystem.correctAnswers = 0;
        playerMovementController.moveAction.action.Enable();
        playerCameraController.lookAction.action.Enable();
        playerInteraction.ClearInteraction();

        capsuleCollider.enabled = false;
    }
}
