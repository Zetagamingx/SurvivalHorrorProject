using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CarLightController : MonoBehaviour, IInteract
{
    [SerializeField] private ObtainCondition obtainCondition;
    [SerializeField] private Light carLight1;
    [SerializeField] private Light carLight2;

    [SerializeField] private GameObject interactionDialogueContainer;
    [SerializeField] private TextMeshProUGUI interactionText;

    [SerializeField] private PlayerActionManager playerActionManager;
    
    private bool lightsOn = false;

    public string InteractionPrompt => "You turned the car lights on";
      
    public void Interact()
    {
      
        if (!lightsOn)
        {
            playerActionManager.DisableActions();
            interactionDialogueContainer.SetActive(true);
            interactionText.SetText(InteractionPrompt);

            StartCoroutine(ReturnMovement());

            carLight1.enabled = true;
            carLight2.enabled = true;

            lightsOn = true;

            obtainCondition.Unlock();
        }

    }

    private IEnumerator ReturnMovement()
    {
        yield return new WaitForSecondsRealtime(2);
        interactionDialogueContainer.SetActive(false);
        playerActionManager.EnableActions();
        yield break;
    }
}
