using System.Collections;
using TMPro;
using UnityEngine;

public class ObtainItem : MonoBehaviour, IInteract,IPickUp
{
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerActionManager playerActionManager;

    [SerializeField] private GameObject itemDialogueContainer;
    [SerializeField] private TextMeshProUGUI itemDialogueText;

    [SerializeField] private ItemData itemData;
    [SerializeField] private int quantity = 1;

    private Collider objectCollider;
    public string InteractionPrompt => InteractionPrompt;

    public string ItemObtainedPrompt => itemData.PickupMessage;

    public ItemData Data => itemData;

    public int Quantity => quantity;

    private void Awake()
    {
        objectCollider = GetComponent<Collider>();

        if (playerInteraction == null)
        {
            playerInteraction = FindFirstObjectByType<PlayerInteraction>();
        }

        if (playerInventory == null)
        {
            playerInventory = FindFirstObjectByType<PlayerInventory>();
        }
    }

    public void Interact()
    {
        PickUpItem();
    }

    private void PickUpItem()
    {
        playerInteraction.ClearInteraction();
        bool added = playerInventory.AddItem(itemData, quantity);

        if (!added)
        {
            Debug.Log("Inventory Full");
            ShowPickUpText("Cant add more items, my inventory is full");
            return;
        }

        ShowPickUpText(ItemObtainedPrompt);

        FindFirstObjectByType<InventoryUIController>().RefreshInventory();
        
    }

    private void ShowPickUpText(string message)
    {
        playerActionManager.DisableActions();
        itemDialogueContainer.SetActive(true);
        itemDialogueText.SetText(message);
        StartCoroutine(AllowPlayerMovement());
    }

    private IEnumerator AllowPlayerMovement()
    {
        Debug.Log("COROUTINE ENTERED");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("time has passed");
        itemDialogueContainer.SetActive(false);
        playerActionManager.EnableActions();
        objectCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
