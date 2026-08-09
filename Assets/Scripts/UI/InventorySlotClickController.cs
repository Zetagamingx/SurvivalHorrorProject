using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotClickController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UIInventorySlot inventorySlot;
    [SerializeField] private PlayerInventory playerInventory;

    private void Awake()
    {
        if (inventorySlot == null)
            inventorySlot = GetComponentInParent<UIInventorySlot>();

        if (playerInventory == null)
            playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (!playerInventory.IsCombineMode)
            return;

        playerInventory.SelectSlotForCombination(inventorySlot.SlotIndex);
    }
}