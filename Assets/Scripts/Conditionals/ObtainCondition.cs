using System.Collections;
using TMPro;
using UnityEngine;

public class ObtainCondition : MonoBehaviour, IPickupCondition, IInteract
{
    [Header("Pickup")]
    [SerializeField] private ObtainItem obtainItem;

    [Header("Blocked Interaction")]
    [SerializeField] private string blockedMessage = "";
    [SerializeField] private GameObject itemDialogueContainer;
    [SerializeField] private TextMeshProUGUI itemDialogueText;

    [Header("Player")]
    [SerializeField] private PlayerActionManager playerActionManager;

    

    public string InteractionPrompt => blockedMessage;

    private void Start()
    {
        obtainItem = GetComponent<ObtainItem>();
        obtainItem.enabled = false;
    }
    public void Unlock()
    {        
        obtainItem.enabled = true;
        enabled = false;
    }

    public string GetBlockedMessage()
    {
        return blockedMessage;
    }

    public void Interact()
    {
        ShowBlockedMessage();
    }

    private void ShowBlockedMessage()
    {
        playerActionManager.DisableActions();

        itemDialogueContainer.SetActive(true);
        itemDialogueText.SetText(GetBlockedMessage());

        StartCoroutine(AllowPlayerMovement());
    }

    private IEnumerator AllowPlayerMovement()
    {
        yield return new WaitForSecondsRealtime(2f);

        itemDialogueContainer.SetActive(false);
        playerActionManager.EnableActions();
    }
}