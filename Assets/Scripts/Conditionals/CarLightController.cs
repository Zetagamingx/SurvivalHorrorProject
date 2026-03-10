using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class CarLightController : MonoBehaviour, IInteractable
{
    [SerializeField] private ObtainCondition obtainCondition;
    [SerializeField] private Light carLight1;
    [SerializeField] private Light carLight2;

    private InteractDialogueController dialogueController;
    private TextMeshProUGUI textMeshPro;
    private bool lightsOn = false;

    public string InteractionPrompt => "You turned the car lights on";

    private void Start()
    {
        dialogueController = InteractDialogueController.Instance;
        textMeshPro = dialogueController.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Interact()
    {
        if (dialogueController.isShowingDialogue)
        {
            dialogueController.TryClose();
            return;
        }

        textMeshPro.text = InteractionPrompt;
        dialogueController.Show();

        if (lightsOn)
            return;        

        carLight1.enabled = true;
        carLight2.enabled = true;

        lightsOn = true;

        obtainCondition.Unlock();


    }
}
