using UnityEngine;

public class ObtainItem : MonoBehaviour, IInteract,IPickUp
{
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] PlayerInteraction playerInteraction;

    [SerializeField] private ItemData itemData;
    [SerializeField] private int quantity = 1;

    private Collider objectCollider;
    public string InteractionPrompt => "Pick up";

    public string ItemObtainedPrompt => $"Obtained {itemData.ItemName}";

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
            return;
        }

        objectCollider.enabled = false;
        gameObject.SetActive(false);

        FindFirstObjectByType<InventoryUIController>().RefreshInventory();
        
    }
}
