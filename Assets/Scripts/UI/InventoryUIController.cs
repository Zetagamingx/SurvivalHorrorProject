using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;

    [SerializeField] private List<UIInventorySlot> inventorySlots = new();

    private void Awake()
    {
        if (playerInventory == null)
            playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    private void Start()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        Debug.Log("Refreshing Inventory UI");
        List<InventorySlot> inventory = playerInventory.Inventory;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].SlotIndex = i;

            UIDragItem dragItem = inventorySlots[i].GetComponentInChildren<UIDragItem>();

            if (dragItem != null)
                dragItem.SlotIndex = i;
            if (i < inventory.Count)
                inventorySlots[i].Refresh(inventory[i]);
            else
                inventorySlots[i].Clear();
        }

    }
}