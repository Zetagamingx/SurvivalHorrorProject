using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class EntranceHoleInteract : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject objectDialogueContainer;
    [SerializeField] private TextMeshProUGUI objectInteractText;
    
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerActionManager playerActionManager;

    [SerializeField] private GameObject explosionCutscene;
    [SerializeField] private PlayableDirector playableDirector;
    
    [SerializeField] private ItemData requiredItem;
    [SerializeField] public int requiredAmount;

    
    public string InteractionPrompt => "";

    

    public void Interact()
    {
        if (playerInventory.HasItem(requiredItem, requiredAmount))
        {
            playerActionManager.DisableActions();
            objectDialogueContainer.SetActive(true);
            objectInteractText.SetText("You placed the explosive can");
            StartCutScene();
        }

        else
        {
            objectInteractText.SetText("The damaged wall here smells like... kerosene?");
        }            
    }

    private void StartCutScene()
    {
        playableDirector.stopped += OnCutSceneFinished;
        StartCoroutine(RemoveText());
        playableDirector.Play();
    }

    private void OnCutSceneFinished(PlayableDirector director)
    {
        playableDirector.stopped -= OnCutSceneFinished;
        playerActionManager.EnableActions();
    }

    private IEnumerator RemoveText()
    {
        yield return new WaitForSecondsRealtime(1f);
        objectDialogueContainer.SetActive(false);
        yield break;
    }

    
}
