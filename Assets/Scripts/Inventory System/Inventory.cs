using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 10;
    public InventorySlot[] slots;
    public static event Action OnInventoryChanged;
    public static Inventory Instance;

    [SerializeField] private ItemDatabase itemDatabase;
    public ItemDatabase ItemDatabase => itemDatabase;

    void Awake()
    {
        Instance = this;
        // Initialize inventory slots
        slots = new InventorySlot[maxSlots];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public void Start()
    {
        //Debug.Log("=== ItemDatabase Contents ===");
        //foreach (var item in itemDatabase.allItems)
        //{
         //   if (item != null)
           //     Debug.Log($"Database has: '{item.itemName}'");
            //else
              //  Debug.Log("Null item in database!");
        //}

        //Debug.Log("=== Starting Inventory Test ===");
        //AddItem("Warning Paper", 1);    // Should place in first empty slot
        //AddItem("Warning Paper", 20);    // Should stack
       // AddItem("Aerosol Can", 1);     // Should take next free slot
        //RemoveItem("Warning Paper", 1);
    }

    public bool AddItem(string itemName, int amount = 1)
    {
        ItemData itemData = itemDatabase.GetItemByName(itemName);
        if (itemData == null)
        {
            Debug.LogWarning("Item not found in database: " + itemName);
            return false;
        }

        // If stackable and already exists, stack it
        if (itemData.isStackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == itemData)
                {
                    slots[i].quantity += amount;
                    PrintInventory();
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        // Find first empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].item = itemData;
                slots[i].quantity = amount;
                PrintInventory();
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        Debug.LogWarning("Inventory full, could not add item: " + itemName);
        return false;



    }

    public bool RemoveItem(string itemName, int amount = 1)
    {
        ItemData itemData = itemDatabase.GetItemByName(itemName);
        if (itemData == null)
        {
            Debug.LogWarning("Item not found in database: " + itemName);
            return false;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == itemData)
            {
                slots[i].quantity -= amount;
                if (slots[i].quantity <= 0)
                {
                    slots[i].item = null;
                    slots[i].quantity = 0;
                }
                PrintInventory();
                return true;
            }
        }

        Debug.LogWarning("Tried to remove item not in inventory: " + itemName);
        return false;
    }

    public bool HasItem(string itemName, int quantity = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null &&
                slots[i].item.itemName == itemName &&
                slots[i].quantity >= quantity)
            {
                return true;
            }
        }
        return false;
    }

    private void PrintInventory()
    {
        Debug.Log("Inventory updated:");
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty)
            {
                Debug.Log($"Slot {i}: {slots[i].item.itemName} x{slots[i].quantity}");
            }
            else
            {
                Debug.Log($"Slot {i}: (empty)");
            }
        }
    }

   
}
