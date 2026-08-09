using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("UI")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMPro.TextMeshProUGUI quantityText;

    private PlayerInventory playerInventory;
    private InventoryUIController inventoryUIController;

    public int SlotIndex { get; set; }

    private void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        inventoryUIController = FindFirstObjectByType<InventoryUIController>();
    }

    public void Refresh(InventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.gameObject.SetActive(false);
            return;
        }

        iconImage.enabled = true;
        iconImage.sprite = slot.item.Icon;

        if (slot.quantity > 1)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = slot.quantity.ToString();
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }

    }

    public void Clear()
    {
        iconImage.enabled = false;
        quantityText.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"Dropped on slot {SlotIndex}");

        UIDragItem draggedItem = eventData.pointerDrag.GetComponent<UIDragItem>();

        if (draggedItem == null)
            return;

        playerInventory.SwapSlots(draggedItem.SlotIndex, SlotIndex);

        inventoryUIController.RefreshInventory();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked slot {SlotIndex}");

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            playerInventory.SelectSlotForCombination(SlotIndex);
        }
    }
}